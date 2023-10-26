using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public string stageName = string.Empty;
    public int Stage { get; private set; } = 0;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI noticeText;
    public FadeEffects moveScene;
    public GameObject[] locks;

    private void Start()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        Unlock();
    }

    public void StageSelect(string stage)
    {
        if (stageName == stage)
        {
            if (IsUnlock())
            {
                moveScene.FadeOut(stageName);
            }
            else
            {
                Notice("�رݵ��� ���� ���������Դϴ�.");
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
