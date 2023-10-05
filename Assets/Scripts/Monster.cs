using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 이동속도
    public int damage = 1; // 공격력
    public Queue<Pattern> queue = new Queue<Pattern>(); // 패턴 큐
    public int score = 1; // 점수
    
    private float initY = 0.0f;
    public event Action onDeath;

    private void Start()
    {
        initY = transform.position.y;
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
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

    private void LerpScale()
    {
        float gap = (initY - transform.position.y) / initY * 2.0f;
        transform.localScale = new Vector3(gap, gap, 1);
    }

    public void SetUp(Pattern c)
    {
        queue.Enqueue(c);
    }

    public void OnHit(Pattern c)
    {
        if (queue.Peek() == c)
        {
            queue.Dequeue();

            if (queue.Count <= 0)
            {
                OnDie();
            }
        }
        
    }

    public void OnDie()
    {
        GameManager.instance.AddScore(score);

        if (onDeath != null)
        {
            onDeath();
        }
    }
}
