using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class MonsterSpawner : MonoBehaviour
{
    public Monster monsterPrefab; // 몬스터 프리팹
    
    private float minX;
    private float maxX;
    private float posY;

    private float minSpeed = 1.0f;
    private float maxSpeed = 1.0f;
    private int minPattern = 1;
    private int maxPattern = 1;
    private float minSpawnTime = 0.0f;
    private float maxSpawnTime = 1.0f;
    private int score = 1;
    private int damage = 1;

    private float nextSpawnTime = 0.0f;
    private float lastSpawnTime = 0.0f;

    private ObjectPool<Monster> monsterPool;

    private void Awake()
    {
        var table = CsvTableMgr.GetTable<MonsterTable>();

        minSpeed = table.dataTable[monsterPrefab.monsterID].MinSPD;
        maxSpeed = table.dataTable[monsterPrefab.monsterID].MaxSPD;
        minPattern = table.dataTable[monsterPrefab.monsterID].MinPTN;
        maxPattern = table.dataTable[monsterPrefab.monsterID].MaxPTN;
        minSpawnTime = table.dataTable[monsterPrefab.monsterID].MinSpwTime;
        maxSpawnTime = table.dataTable[monsterPrefab.monsterID].MaxSpwTime;
        score = table.dataTable[monsterPrefab.monsterID].SCORE;
        damage = table.dataTable[monsterPrefab.monsterID].DAMAGE;
    }

    private void Start()
    {
        float x = gameObject.GetComponentInParent<SpriteRenderer>().bounds.size.x;
        minX = -x / 2.0f;
        maxX = +x / 2.0f;
        posY = gameObject.GetComponentInParent<SpriteRenderer>().bounds.size.y / 2.0f + transform.position.y;

        lastSpawnTime = Time.time;
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        monsterPool = new ObjectPool<Monster>(() => { var monster = Instantiate(monsterPrefab); monster.gameObject.SetActive(false); return monster; }, 
            delegate (Monster monster) { monster.gameObject.SetActive(true); }, // actionOnGet
            delegate (Monster monster) { monster.gameObject.SetActive(false); }, // actionOnRelease
            null, true, 20); 
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
        monster.sprite.flipX = pos.x < 0.0f;
        monster.transform.position = pos;
        MonsterPatternSetUp(monster);
        monster.actionOnDeath += () => monsterPool.Release(monster);
        //monster.actionOnAttack += () => monsterPool.Release(monster);
        GameManager.instance.AddMonster(monster);
        UIManager.instance.UpdateGuide();
    }

    private void MonsterPatternSetUp(Monster monster)
    {
        var quantity = Random.Range(minPattern, maxPattern + 1);
        for (int i = 0; i < quantity; i++)
        {
            monster.AddPattern((Pattern)Random.Range(0, (int)Pattern.Count));
        }

        monster.moveSpeed = Random.Range(minSpeed, maxSpeed);
        monster.score = score;
        monster.damage = damage;
    }
}
