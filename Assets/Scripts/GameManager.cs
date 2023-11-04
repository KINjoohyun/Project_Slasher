using System;
using System.Collections.Generic;
using TMPro;
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
    public int hp { get; set; }
    public bool IsPause { get; private set; } = false;
    public BossController[] bossCon;
    public int StageNum = 0;

    private List<ISlashable> slashList;
    private List<ISlashable> removeList;

    public TextMeshProUGUI slashText;

    public event Action actionOnKill;

    private void Start()
    {
        if (BGMobject.instance != null)
        {
            BGMobject.instance.Stop();
        }

        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        IsGameover = false;
        IsPause = false;
        Time.timeScale = 1;

        var table = CsvTableMgr.GetTable<UpgradeTable>();

        slashList = new List<ISlashable>();
        removeList = new List<ISlashable>();
        Score = 0;
        HighScore = PlayDataManager.data.HighScore;
        if (PlayDataManager.data.Upgrade_HealthUP == 0)
        {
            maxHp = 5;
        }
        else
        {
            maxHp = 5 + table.healthTable[PlayDataManager.data.Upgrade_HealthUP].VALUE;
        }
        hp = maxHp;

        UIManager.instance.UpdateUI();
        foreach (var item in bossCon)
        {
            UIManager.instance.UpdateBoss(item.Count, item.SpawnCount);
        }

        WeaponHandler.instance.ActiveWeapon();
    }

    public void OnDamage(int damage)
    {
        if (IsGameover)
        {
            return;
        }

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
        SwipeManager.instance.DeleteLine();

        UpdateHighScore();
        PlayDataManager.Gameover();
        UIManager.instance.UpdateGameover();
    }

    public void Win()
    {
        var monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (var item in monsters) 
        {
            item.SetActive(false);
        }

        PlayDataManager.UnlockStage(StageNum);
        Gameover();
    }

    private void UpdateHighScore()
    {
        if (Score < HighScore)
        {
            return;
        }
        HighScore = Score;
    }

    public void AddMonster(ISlashable monster)
    {
        slashList.Add(monster);
    }

    public void SlashMonsters(Pattern c)
    {
        if (slashList.Count <= 0) 
        { 
            return;
        }

        int slachCount = 0;
        foreach (var monster in slashList)
        {
            if (monster.OnSlashed(c))
            {
                slachCount++;
            }
        }
        if (slachCount > 0)
        {
            MultiSlash(slachCount);
        }
        RemoveListAct();

        foreach (var item in bossCon)
        {
            item.Spawn();
        }

    }

    private void MultiSlash(int slashCount)
    {
        if (slashCount < 2)
        {
            return;
        }
        slashText.text = $"X {slashCount}";
        slashText.gameObject.SetActive(true);
    }

    public void RemoveListAct()
    {
        if (removeList.Count < 0)
        {
            return;
        }

        foreach (var monster in removeList)
        {
            if (actionOnKill != null && !monster.IsAlive)
            {
                actionOnKill();
            }
            slashList.Remove(monster);
        }
        removeList.Clear();
    }

    public void AddScore(int increase)
    {
        Score += increase;
        foreach (var item in bossCon)
        {
            item.Count++;
        }

        UIManager.instance.UpdateScore();
        foreach (var item in bossCon)
        {
            UIManager.instance.UpdateBoss(item.Count, item.SpawnCount);
        }
    }

    public void RemoveMonster(ISlashable monster)
    {
        removeList.Add(monster);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        IsPause = !IsPause;

        if (IsPause)
        {
            SwipeManager.instance.DeleteLine();
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public Pattern CloserPattern()
    {
        if (slashList.Count <= 0)
        {
            return Pattern.Count;
        }

        var min = slashList[0];
        foreach (var monster in slashList) 
        { 
            if (monster.GetYPos() <= min.GetYPos())
            {
                min = monster;
            }
        }
        return min.GetPattern();
    }

    public void GoLobby()
    {
        UpdateHighScore();
        PlayDataManager.Gameover();

        if (BGMobject.instance != null)
        {
            BGMobject.instance.Play();
        }
    }
}
