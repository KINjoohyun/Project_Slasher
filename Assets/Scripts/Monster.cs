using System;
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
    public bool isAlive {  get; private set; }

    private void Start()
    {
        initY = transform.position.y;
        transform.localScale = Vector3.one;
        isAlive = true;
    }

    private void Update()
    {
        if (!isAlive) return;

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
        if (!isAlive) return;

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
        if (onDeath != null)
        {
            onDeath();
        }

        isAlive = false;
        GameManager.instance.AddScore(score);
        Destroy(gameObject, 1f);
    }
}
