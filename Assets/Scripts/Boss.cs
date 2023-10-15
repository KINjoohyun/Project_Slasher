using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, ISlashable, IDeathEvent
{
    public MonsterID monsterID = MonsterID.None; // ���� ID

    public float moveSpeed = 1.0f; // �̵��ӵ�
    private float speed = 1.0f;

    public Queue<Pattern> queue = new Queue<Pattern>(); // ���� ť

    public int damage = 1; // ���ݷ�
    public int score = 1; // ����

    public event Action actionOnDeath;
    public event Action actionOnSlash; //�߰� ��� ���� ����
    public bool IsAlive { get; private set; }
    public MonsterUiController monsterUi;
    private Animator anim;

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

    public void OnSlashed(Pattern c)
    {
        if (!IsAlive) 
        {
            return; 
        }

        if (queue.Peek() == c)
        {
            queue.Dequeue();
            monsterUi.DequeueImage();
            anim.SetTrigger("Hit");

            if (monsterUi.IsEmpty())
            {
                Knockback();
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
            }
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

    public void Knockback()
    {
        StartCoroutine(KnockbackCoroutine(1.0f));
    }

    private IEnumerator KnockbackCoroutine(float duration)
    {
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime;

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
    }
}