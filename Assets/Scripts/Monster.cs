using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum MonsterID
{
    None = -1,

    Goblin = 1001,
    FlyingEye,
    Boss1,
    Mushroom,
    BigMushroom,
    Slime,
    Boss2,
    Skeleton,
    Bandit,
    Matial,
    Boss3_1,
    Boss3_2,

    Count
}

public class Monster : MonoBehaviour, ISlashable
{
    public MonsterID monsterID = MonsterID.None; // 몬스터 ID

    public float moveSpeed = 1.0f; // 이동속도
    private float speed = 1.0f;

    public Queue<Pattern> queue = new Queue<Pattern>(); // 패턴 큐

    public int damage = 1; // 공격력
    public int score = 1; // 점수
    
    public event Action actionOnDeath;
    public event Action actionOnSlash; //추가 기능 구현 가능
    public bool IsAlive { get; private set; }
    public MonsterUiController queueUi;
    private Animator anim;

    private void OnEnable()
    {
        IsAlive = true;
        if (queue == null)
        {
            queue = new Queue<Pattern>();
        }

        var table = CsvTableMgr.GetTable<UpgradeTable>();
        if (PlayDataManager.data.Upgrade_SpeedDown == 0)
        {
            speed = moveSpeed;
        }
        else
        {
            speed = moveSpeed - table.speedTable[PlayDataManager.data.Upgrade_SpeedDown].VALUE;
        }
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!IsAlive) return;
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.OnDamage(damage);
            OnDie();
        }
    }

    public void AddPattern(Pattern c)
    {
        queue.Enqueue(c);
        queueUi.EnqueueImage(c);
    }

    public void OnSlashed(Pattern c)
    {
        if (!IsAlive) return;

        if (queue.Peek() == c)
        {
            queue.Dequeue();
            queueUi.DequeueImage();
            anim.SetTrigger("Hit");

            if (actionOnSlash != null)
            {
                actionOnSlash();
            }

            if (queue.Count <= 0)
            {
                GameManager.instance.AddScore(score);

                OnDie();
            }
        }
        
    }

    public void OnDie()
    {
        IsAlive = false;
        GameManager.instance.RemoveMonster(this);
        queue.Clear();
        queueUi.Clear();
        //anim.SetTrigger("Die");

        if (actionOnDeath != null)
        {
            actionOnDeath();
            actionOnDeath = null;
        }
    }

    public Pattern GetPattern()
    {
        if (queue.Count <= 0)
        {
            return Pattern.Count;
        }
        return queue.Peek();
    }

    public float GetYPos()
    {
        return transform.position.y;
    }
}
