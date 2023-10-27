using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

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

public class Monster : MonoBehaviour, ISlashable, IDeathEvent
{
    public MonsterID monsterID = MonsterID.None; // 몬스터 ID

    public float moveSpeed = 1.0f; // 이동속도
    private float speed = 1.0f;

    public Queue<Pattern> queue = new Queue<Pattern>(); // 패턴 큐

    public int damage = 1; // 공격력
    public int score = 1; // 점수
    
    public event Action actionOnDeath;
    public event Action actionOnAttack;
    public event Action actionOnSlash; //추가 기능 구현 가능
    public bool IsAlive { get; private set; }
    public MonsterUiController monsterUi;
    private Animator anim;
    public SpriteRenderer sprite;

    [Header("밝아지는 정도")]
    public float fadeDark = 20.0f; // 밝아지는 정도

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
        if (!IsAlive)
        {
            return;
        }

        transform.position += Vector3.down * speed * Time.deltaTime;

        var y = transform.position.y / fadeDark; // color code
        sprite.color = Color.Lerp(Color.white, Color.black, y); // color code
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //OnAttack();
            GameManager.instance.OnDamage(damage);
            OnDie();
        }
    }

    public void AddPattern(Pattern c)
    {
        queue.Enqueue(c);
        monsterUi.EnqueueImage(c);
    }

    public bool OnSlashed(Pattern c)
    {
        if (!IsAlive)
        {
            return false;
        }

        if (queue.Peek() == c)
        {
            queue.Dequeue();
            monsterUi.DequeueImage();
            anim.SetTrigger("Hit");

            if (actionOnSlash != null)
            {
                actionOnSlash();
                actionOnSlash = null;
            }

            if (queue.Count <= 0)
            {
                GameManager.instance.AddScore(score);

                OnDie();
                return true;
            }

            Stiffness(0.2f); // test code
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnDie()
    {
        IsAlive = false;
        GameManager.instance.RemoveMonster(this);
        queue.Clear();
        monsterUi.Clear();
        anim.SetTrigger("Die");
    }

    public void OnAttack()
    {
        IsAlive = false;
        GameManager.instance.OnDamage(damage);
        GameManager.instance.RemoveMonster(this);
        queue.Clear();
        monsterUi.Clear();

        if (actionOnAttack != null)
        {
            actionOnAttack();
            actionOnAttack = null;
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

    public void OnDeath()
    {
        if (actionOnDeath != null)
        {
            actionOnDeath();
            actionOnDeath = null;
        }
    }

    public void Stiffness(float time)
    {
        StartCoroutine(StiffnessCoroutine(time));
    }

    private IEnumerator StiffnessCoroutine(float duration)
    {
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position += Vector3.up * speed * Time.deltaTime;

            yield return null;
        }
    }

    public void Knockback(float time)
    {
        StartCoroutine(KnockbackCoroutine(time));
    }

    private IEnumerator KnockbackCoroutine(float duration)
    {
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position += Vector3.up * speed * Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime;

            yield return null;
        }
    }
}
