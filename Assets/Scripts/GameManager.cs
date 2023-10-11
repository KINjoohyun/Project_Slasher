using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveDataVC = SaveDataV2;

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
        HighScore = LoadHighScore();
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
        UIManager.instance.UpdateGameover();
    }

    private void UpdateHighScore()
    {
        if (Score < HighScore)
        {
            return;
        }
        HighScore = Score;
        var data = SaveLoadSystem.Load("savefile.json") as SaveDataVC;
        data.HighScore = HighScore;
        SaveLoadSystem.Save(data, "savefile.json");
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

    private int LoadHighScore()
    {
        var data = SaveLoadSystem.Load("savefile.json") as SaveDataVC;
        if (data == null)
        {
            data = new SaveDataVC();
            SaveLoadSystem.Save(data, "savefile.json");
        }
        return data.HighScore;
    }
}
