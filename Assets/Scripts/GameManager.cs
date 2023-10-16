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
    public BossController bossCon;
    public string StageName = string.Empty;

    private List<ISlashable> slashList;
    private List<ISlashable> removeList;

    private void Start()
    {
        PlayDataManager.Init();

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

        foreach (var monster in slashList) 
        {
            monster.OnSlashed(c);
        }

        foreach (var monster in removeList)
        {
            slashList.Remove(monster);
        }
        removeList.Clear();

        bossCon.Spawn();

        UIManager.instance.UpdateGuide();
    }

    public void AddScore(int increase)
    {
        Score += increase;
        bossCon.Count++;

        UIManager.instance.UpdateScore();
    }

    public void RemoveMonster(ISlashable monster)
    {
        removeList.Add(monster);
        UIManager.instance.UpdateGuide();
    }

    public void Restart()
    {
        SceneManager.LoadScene(StageName);
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


}
