using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public string stageName = string.Empty;
    public int Stage { get; private set; } = 0;
    public TextMeshProUGUI stageText;
    private MoveScene moveScene;

    private void Awake()
    {
        moveScene = GetComponent<MoveScene>();

    }

    private void Start()
    {
        PlayDataManager.Init();

    }

    public void StageSelect(string stage)
    {
        if (stageName == stage && IsUnlock())
        {
            moveScene.Move(stageName);
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
}
