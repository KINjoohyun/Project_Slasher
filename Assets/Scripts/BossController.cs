using UnityEngine;

public class BossController : MonoBehaviour
{
    public int Count { get; set; } = 0;
    public int SpawnCount = 50; // 소환에 필요한 처치 수
    public Boss boss; // 소환할 보스

    private float posY;

    private float minSpeed = 1.0f;
    private float maxSpeed = 1.0f;
    private int minPattern = 1;
    private int maxPattern = 1;
    private int score = 1;
    private int damage = 1;

    private bool isSpawn = false;

    private void Awake()
    {
        var table = CsvTableMgr.GetTable<MonsterTable>();

        minSpeed = table.dataTable[boss.monsterID].MinSPD;
        maxSpeed = table.dataTable[boss.monsterID].MaxSPD;
        minPattern = table.dataTable[boss.monsterID].MinPTN;
        maxPattern = table.dataTable[boss.monsterID].MaxPTN;
        score = table.dataTable[boss.monsterID].SCORE;
        damage = table.dataTable[boss.monsterID].DAMAGE;
    }

    private void Start()
    {
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

        Instantiate(boss, new Vector3(0, posY, 0), boss.transform.rotation);

        BossPatternSetUp();
        //boss.actionOnDeath += () => GameManager.instance.Gameover(); // test code
        GameManager.instance.AddMonster(boss);
        isSpawn = true;
    }

    private void BossPatternSetUp()
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
