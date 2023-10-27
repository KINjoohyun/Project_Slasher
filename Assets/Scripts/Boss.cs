using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Boss : MonoBehaviour, ISlashable, IDeathEvent
{
    public MonsterID monsterID = MonsterID.None; // 몬스터 ID

    public float moveSpeed = 1.0f; // 이동속도
    private float speed = 1.0f;
    public float stiffnessTime = 1.0f; // 경직 시간
    public float knockbackTime = 1.0f; // 넉백 시간
    public float knockbackDistance = 3.0f; // 넉백 거리

    public Queue<Pattern> queue = new Queue<Pattern>(); // 패턴 큐

    public int damage = 1; // 공격력
    public int score = 1; // 점수

    public event Action actionOnDeath;
    public event Action actionOnSlash; //추가 기능 구현 가능
    public bool IsAlive { get; private set; }
    public MonsterUiController monsterUi;
    private Animator anim;
    public SpriteRenderer sprite;

    [Header("밝아지는 정도")]
    public float fadeDark = 50.0f;

    private void Start()
    {
        IsAlive = true;
        speed = moveSpeed;
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
            GameManager.instance.OnDamage(damage);
            OnAttack();
        }
    }

    public void AddPattern(Pattern c)
    {
        queue.Enqueue(c);
    }

    public void UpdateBossUi()
    {
        if (queue.Count <= 0)
        {
            monsterUi.Clear();
            return;
        }

        int i = 0;
        foreach (var c in queue)
        {
            monsterUi.EnqueueImage(c);
            i++;

            if (i >= 5)
            {
                break;
            }
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

            if (monsterUi.IsEmpty())
            {
                Knockback(knockbackTime, knockbackDistance);
                UpdateBossUi();
            }

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
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnDie()
    {
        if (BossBGM.instance != null)
        {
            BossBGM.instance.Stop();
        }

        IsAlive = false;
        GameManager.instance.RemoveMonster(this);
        queue.Clear();
        monsterUi.Clear();
        anim.SetTrigger("Die");
    }

    public void OnAttack()
    {
        if (BossBGM.instance != null)
        {
            BossBGM.instance.Stop();
        }

        actionOnDeath -= () => GameManager.instance.Win();

        IsAlive = false;
        GameManager.instance.RemoveMonster(this);
        queue.Clear();
        monsterUi.Clear();

        gameObject.SetActive(false);
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
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }

    public void Knockback(float time, float distance)
    {
        StartCoroutine(KnockbackCoroutine(time, distance));
    }

    private IEnumerator KnockbackCoroutine(float duration, float distance)
    {
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            transform.position += Vector3.up * distance * Time.deltaTime;

            yield return null;
        }
    }

    public void OnDeath()
    {
        if (actionOnDeath != null)
        {
            actionOnDeath();
            actionOnDeath = null;
        }
        gameObject.SetActive(false);

    }
}
