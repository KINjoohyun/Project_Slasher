using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    public Sprite playSprite; // Play 상태일 때의 스프라이트
    public Sprite pauseSprite; // Pause 상태일 때의 스프라이트

    private Button button;
    private Image buttonImage;
    private bool isPaused = false; // 게임이 일시정지 상태인지 확인하는 플래그

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // 버튼 클릭 이벤트에 TogglePauseCoroutine 코루틴 실행 추가
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