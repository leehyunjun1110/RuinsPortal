using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // Canvas에 있는 검정색 Image
    [SerializeField] private float fadeDuration = 1f; // 페이드 시간
    [SerializeField] private string nextSceneName; // 다음 씬의 이름
    [SerializeField] private Collider targetCollider; // 특정 오브젝트의 콜라이더

    private bool isFadingIn = true;
    private bool isFadingOut = false;
    private float fadeTimer = 0f;

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 1); // 시작할 때 화면이 검정색으로 가려지도록 설정
            StartCoroutine(FadeIn());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌된 오브젝트의 콜라이더가 targetCollider와 같은지 확인
        if (!isFadingIn && collision.collider == targetCollider)
        {
            StartCoroutine(FadeOut());
        }
        else 
        {
            Debug.Log("씨발");
        }
    }

    private IEnumerator FadeIn()
    {
        fadeTimer = 0f;
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, fadeTimer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0); // 확실히 완전 투명하게 설정
        isFadingIn = false;
    }

    private IEnumerator FadeOut()
    {
        Debug.Log("FadeOut coroutine started."); // 로그 추가
        isFadingOut = true;
        fadeTimer = 0f;
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, fadeTimer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1); // 확실히 완전 검정색으로 설정
        SceneManager.LoadScene(nextSceneName); // 다음 씬으로 전환
    }
}