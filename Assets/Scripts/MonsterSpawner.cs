using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster[] monsterPrefabs; // 소환할 몬스터들
    
    private float minX;
    private float maxX;
    private float posY;

    private void Start()
    {
        float x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        minX = -x / 2.0f;
        maxX = +x / 2.0f;
        posY = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2.0f + transform.position.y;
    }

    private void Update()
    {
        if (GameManager.instance.IsGameover) return;

        if (Input.GetKeyDown(KeyCode.Space)) // test code
        {
            Vector3 pos = new Vector3(Mathf.Lerp(minX, maxX, Random.value), posY, 0);
            var prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            var monster = Instantiate(prefab, pos, prefab.transform.rotation);
            monster.SetUp('a');
            GameManager.instance.AddMonster(monster);
        }
    }
}
