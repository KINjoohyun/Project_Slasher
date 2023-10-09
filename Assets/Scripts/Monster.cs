using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 이동속도
    public int damage = 1; // 공격력
    public Queue<Pattern> queue = new Queue<Pattern>(); // 패턴 큐
    public int score = 1; // 점수
    
    public event Action onDeath;
    public bool IsAlive {  get; private set; }
    public MonsterUiController queueUi;

    private void OnEnable()
    {
        IsAlive = true;
        if (queue == null)
        {
            queue = new Queue<Pattern>();
        }
    }

    private void Update()
    {
        if (!IsAlive) return;
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        //LerpScale();
        if (Input.GetKeyDown(KeyCode.R)) // test code
        {
            foreach (char c in queue) 
            {
                Debug.Log(c);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.OnDamage(damage);
            OnDie();
        }
    }

    public void SetUp(Pattern c)
    {
        queue.Enqueue(c);
        queueUi.EnqueueImage(c);
    }

    public void OnHit(Pattern c)
    {
        if (!IsAlive) return;

        if (queue.Peek() == c)
        {
            queue.Dequeue();
            queueUi.DequeueImage();

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

        if (onDeath != null)
        {
            onDeath();
            onDeath = null;
        }
    }
}
