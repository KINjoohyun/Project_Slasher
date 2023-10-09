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
            yield return new WaitForSeconds(0.1f);
            buttonImage.sprite = playSprite;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            buttonImage.sprite = pauseSprite;
        }
    }
}