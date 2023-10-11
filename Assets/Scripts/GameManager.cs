using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int HighScore { get; private set; }
    public int maxHp = 5; // 최대 체력
    public int hp { get; private set; }
    public bool IsPause { get; private set; } = false;

    private List<Monster> monsterList;
    private List<Monster> removeList;

    private void Start()
    {
        IsGameover = false;
        IsPause = false;
        Time.timeScale = 1;

        monsterList = new List<Monster>();
        removeList = new List<Monster>();
        Score = 0;
        HighScore = PlayDataManager.data.HighScore;
        hp = maxHp;

        UIManager.instance.UpdateUI();
    }

    public void OnDamage(int damage)
    {
        hp -= damage;
        UIManager.instance.UpdateHP();

        if (hp <= 0)
        {
            Gameover();
        }
    }

    public void Gameover()
    {
        IsGameover = true;

        UpdateHighScore();
        PlayDataManager.Gameover();
        UIManager.instance.UpdateGameover();
    }

    private void UpdateHighScore()
    {
        if (Score < HighScore)
        {
            return;
        }
        HighScore = Score;
    }

    public void AddMonster(Monster monster)
    {
        monsterList.Add(monster);
    }

    public void HitMonsters(Pattern c)
    {
        if (monsterList.Count <= 0) 
        { 
            return;
        }

        foreach (var monster in monsterList) 
        {
            monster.OnHit(c);
        }

        foreach (var monster in removeList)
        {
            monsterList.Remove(monster);
        }
        removeList.Clear();

        UIManager.instance.UpdateGuide();
    }

    public void AddScore(int increase)
    {
        Score += increase;

        UIManager.instance.UpdateScore();
    }

    public void RemoveMonster(Monster monster)
    {
        removeList.Add(monster);
        UIManager.instance.UpdateGuide();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        IsPause = !IsPause;

        if (IsPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public Pattern CloserPattern()
    {
        if (monsterList.Count <= 0)
        {
            return Pattern.Count;
        }

        var min = monsterList[0];
        foreach (var monster in monsterList) 
        { 
            if (monster.transform.position.y <= min.transform.position.y)
            {
                min = monster;
            }
        }
        if (min.queue.Count <= 0) return Pattern.Count;
        return min.queue.Peek();
    }


}
