using System.Collections;
using UnityEngine;

public class CaveCameraShake : MonoBehaviour
{
    public Camera cam; // 카메라 컴포넌트를 지정
    public float startFOV = 90f; // 시작 FOV
    public float targetFOV = 60f; // 목표 FOV
    public float duration = 2f; // FOV 변화 시간

    private void Start()
    {
        cam.fieldOfView = startFOV; // 시작 FOV 설정
        StartCoroutine(ChangeFOV());
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
}
