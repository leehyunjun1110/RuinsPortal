using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [Header("카메라 설정")]
    [SerializeField] private float speed = 1f; // 카메라 이동 속도
    private Vector2 lastCameraPosition; // 이전 프레임의 카메라 위치

    [SerializeField] private Transform player; // 플레이어 트랜스폼

    void Start()
    {
        lastCameraPosition = transform.position;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("플레이어 트랜스폼이 할당되지 않았습니다!");
            return;
        }

        Vector2 targetPosition = new Vector2(player.position.x, player.position.y);
        Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z); // z축 고정

        Vector2 deltaMovement = newPosition - lastCameraPosition;

        lastCameraPosition = newPosition;
    }
}