using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{

    public Camera cam; // 카메라 컴포넌트를 지정
    public float startFOV = 90f; // 시작 FOV
    public float targetFOV = 60f; // 목표 FOV
    public float duration = 2f; // FOV 변화 시간

    
    public void SceneChange()
    {
        SceneManager.LoadScene("MainScene"); // 현재 씬 다시 로드
    }

    private Renderer myRenderer;

    private void Start()
    {
        cam.fieldOfView = startFOV; // 시작 FOV 설정
        myRenderer = gameObject.GetComponent<Renderer>();
        myRenderer.material.color = new Color(1, 1, 1, 0); // 초기 색상 설정 (투명)
        StartCoroutine(FadeIn()); // 시작 시 페이드 인
        
    }

    private IEnumerator FadeIn()
    {
        float alpha = 0;
        while (alpha <= 1)
        {
            alpha += Time.deltaTime * 2; // 속도 조절
            Color color = myRenderer.material.color;
            color.a = alpha;
            myRenderer.material.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float alpha = 1;
        while (alpha >= 0)
        {
            alpha -= Time.deltaTime * 2; // 속도 조절
            Color color = myRenderer.material.color;
            color.a = alpha;
            myRenderer.material.color = color;
            yield return null;
        }

        // 페이드 아웃 후 씬 변경
        SceneChange();
    }

     private IEnumerator ChangeFOV()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsed / duration); // FOV 변화
            yield return null;
        }

        cam.fieldOfView = targetFOV; // 최종 FOV 설정
    }

    private void Update()
    {
        // 클릭 시 페이드 아웃
        if (Input.GetMouseButtonDown(0) && myRenderer.material.color.a >= 1)
        {
            StartCoroutine(ChangeFOV());
            StartCoroutine(FadeOut());
        }
    }
}
