using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IParallaxMover
{
    void Move(Vector2 direction, float speed);
    void Stop();
}

public class ParallaxManager : MonoBehaviour, IParallaxMover
{
    [Header("패럴랙스 설정")]
    private Material material;
    private Vector2 currentOffset;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        currentOffset = material.GetTextureOffset("_MainTex");
    }

    public void Move(Vector2 direction, float speed)
    {
        currentOffset += direction * speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", currentOffset);
    }

    public void Stop()
    {
        // 배경 이동을 멈춤
        Move(Vector2.zero, 0f);
    }
}