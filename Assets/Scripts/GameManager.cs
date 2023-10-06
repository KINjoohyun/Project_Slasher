using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public bool IsGameover { get; private set; }
    public int Score { get; private set; }
    public int maxHp = 5; // 최대 체력
    private int hp = 0;

    private List<Monster> monsterList;
    private List<Monster> removeList;

    private void Start()
    {
        IsGameover = false;

        monsterList = new List<Monster>();
        removeList = new List<Monster>();
        Score = 0;
        hp = maxHp;

        UIManager.instance.UpdateHP((float)hp / maxHp);
    }

    public void OnDamage(int damage)
    {
        hp -= damage;
        UIManager.instance.UpdateHP((float)hp / maxHp);

        if (hp <= 0)
        {
            Gameover();
        }
    }

    public void Gameover()
    {
        IsGameover = true;
    }

    public void AddMonster(Monster monster)
    {
        monsterList.Add(monster);
    }

    public void HitMonsters(Pattern c)
    {
        if (monsterList.Count <= 0) return;

        foreach (var monster in monsterList) 
        {
            monster.OnHit(c);
        }

        foreach (var monster in removeList)
        {
            monsterList.Remove(monster);
        }
        removeList.Clear();
    }

    public void AddScore(int increase)
    {
        Score += increase;
    }

    public void RemoveMonster(Monster monster)
    {
        removeList.Add(monster);
    }
}
