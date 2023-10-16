using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening.Core.Enums;

public class StageManager : MonoBehaviour
{
    public string stageName = string.Empty;
    public int Stage { get; private set; } = 0;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI noticeText;
    private MoveScene moveScene;
    public GameObject[] locks;

    private void Awake()
    {
        moveScene = GetComponent<MoveScene>();

    }

    private void Start()
    {
        PlayDataManager.Init();

        Unlock();
    }

    public void StageSelect(string stage)
    {
        if (stageName == stage)
        {
            if (IsUnlock())
            {
                moveScene.Move(stageName);
            }
            else
            {
                Notice("해금되지 않은 스테이지입니다.");
            }
        }
        else
        {
            stageName = stage;
            stageText.text = stage;

            Stage = int.Parse(stageName.Substring(5));
        }
    }

    public bool IsUnlock()
    {
        return PlayDataManager.data.Stage >= Stage;
    }

    public void Notice(string str)
    {
        noticeText.text = str;
        noticeText.gameObject.SetActive(true);
    }

    public void Unlock()
    {
        for (int i = 0; i < PlayDataManager.data.Stage; i++)
        {
            locks[i].SetActive(false);
        }
    }
}
