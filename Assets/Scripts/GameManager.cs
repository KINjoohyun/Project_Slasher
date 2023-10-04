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
    private List<Monster> monsterList;

    private void Start()
    {
        IsGameover = false;

        monsterList = new List<Monster>();
        Score = 0;
    }

    public void OnDamage(int damage)
    {
        Debug.Log($"Player Hit! Damage : {damage}");
    }

    public void Gameover()
    {
        IsGameover = true;

    }

    public void AddMonster(Monster monster)
    {
        monsterList.Add(monster);
        //monster.onDeath += () => monsterList.Remove(monster);
    }

    public void HitMonsters(char c)
    {
        if (monsterList.Count <= 0) return;

        foreach (var monster in monsterList) 
        {
            monster.OnHit(c);
        }

        // ����ȭ �ʿ� (��ȸ �� ����Ʈ ���Ŵ� �����Ͽ� ���� SetActiveFalse ����)
    }

    public void AddScore(int increase)
    {
        Score += increase;
    }
}
