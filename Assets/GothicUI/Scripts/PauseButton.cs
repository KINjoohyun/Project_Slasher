using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    public Sprite playSprite; // Play ������ ���� ��������Ʈ
    public Sprite pauseSprite; // Pause ������ ���� ��������Ʈ

    private Button button;
    private Image buttonImage;
    private bool isPaused = false; // ������ �Ͻ����� �������� Ȯ���ϴ� �÷���

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // ��ư Ŭ�� �̺�Ʈ�� TogglePauseCoroutine �ڷ�ƾ ���� �߰�
        button.onClick.AddListener(() => StartCoroutine(TogglePauseCoroutine()));
    }

    private IEnumerator TogglePauseCoroutine()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Time.timeScale = 0;
            buttonImage.sprite = playSprite;
        }
        else
        {
            Time.timeScale = 1;
            yield return new WaitForSecondsRealtime(0.5f);
            buttonImage.sprite = pauseSprite;
        }
    }
}