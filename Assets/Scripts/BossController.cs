using UnityEngine;

public class BossController : MonoBehaviour
{
    public int Count { get; set; } = 0;
    public int SpawnCount = 50; // 소환에 필요한 처치 수
    public Boss bossPrefab; // 소환할 보스
    public MonsterSpawner[] disableSpwners; // 비활성화할 스포너들

    private float posY;

    private float minSpeed = 1.0f;
    private float maxSpeed = 1.0f;
    private int minPattern = 1;
    private int maxPattern = 1;
    private int score = 1;
    private int damage = 1;

    private bool isSpawn = false;

    private void Start()
    {
        var table = CsvTableMgr.GetTable<MonsterTable>();

        minSpeed = table.dataTable[bossPrefab.monsterID].MinSPD;
        maxSpeed = table.dataTable[bossPrefab.monsterID].MaxSPD;
        minPattern = table.dataTable[bossPrefab.monsterID].MinPTN;
        maxPattern = table.dataTable[bossPrefab.monsterID].MaxPTN;
        score = table.dataTable[bossPrefab.monsterID].SCORE;
        damage = table.dataTable[bossPrefab.monsterID].DAMAGE;

        Count = 0;
        isSpawn = false;

        posY = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2.0f + transform.position.y;
    }

    public void Spawn()
    {
        if (Count < SpawnCount || isSpawn)
        {
            return;
        }

        var boss = Instantiate(bossPrefab, new Vector3(0, posY, 0), bossPrefab.transform.rotation);

        BossPatternSetUp(boss);
        boss.actionOnDeath += () => GameManager.instance.Win();
        GameManager.instance.AddMonster(boss);
        isSpawn = true;

        foreach (var item in disableSpwners)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void BossPatternSetUp(Boss boss)
    {
        var quantity = Random.Range(minPattern, maxPattern + 1);
        for (int i = 0; i < quantity; i++)
        {
            boss.AddPattern((Pattern)Random.Range(0, (int)Pattern.Count));
        }
        boss.UpdateBossUi();

        boss.moveSpeed = Random.Range(minSpeed, maxSpeed);
        boss.score = score;
        boss.damage = damage;
    }
}
