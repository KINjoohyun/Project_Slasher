using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SwoopSpawner : MonoBehaviour
{
    public Monster prefab; // 소환할 몬스터들

    private float minX;
    private float maxX;
    private float posY;

    public float minSpawnTime = 0.0f; // 최소 스폰 시간
    public float maxSpawnTime = 30.0f; // 최대 스폰 시간
    private float nextSpawnTime = 0.0f;
    private float lastSpawnTime = 0.0f;
    public float moveSpeed = 5.0f; // 이동 속도

    private ObjectPool<Monster> monsterPool;

    private void Start()
    {
        float x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        minX = -x / 2.0f;
        maxX = +x / 2.0f;
        posY = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2.0f + transform.position.y;

        monsterPool = new ObjectPool<Monster>(() => {
            Vector3 pos = new Vector3(Mathf.Lerp(minX, maxX, Random.value), posY, 0);
            var monster = Instantiate(prefab, pos, prefab.transform.rotation);
            return monster;
        },
            delegate (Monster monster) { monster.gameObject.SetActive(true); }, // actionOnGet
            delegate (Monster monster) { monster.gameObject.SetActive(false); }); // actionOnRelease
    }

    private void Update()
    {
        if (GameManager.instance.IsGameover) return;


        if (lastSpawnTime + nextSpawnTime < Time.time)
        {
            lastSpawnTime = Time.time;
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector3 pos = new Vector3(Mathf.Lerp(minX, maxX, Random.value), posY, 0);

        var monster = monsterPool.Get();
        monster.transform.position = pos;
        MonsterSetUp(monster);
        monster.onDeath += () => monsterPool.Release(monster);
        GameManager.instance.AddMonster(monster);
        UIManager.instance.UpdateGuide();
    }

    private void MonsterSetUp(Monster monster)
    {
        monster.SetUp((Pattern)Random.Range(0, (int)Pattern.Count));
        monster.moveSpeed = moveSpeed;
    }
}
