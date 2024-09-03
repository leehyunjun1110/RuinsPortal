using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("패럴랙스 설정")]
    [SerializeField] [Range(-1.0f, 1.0f)]
    private float moveSpeed = 0.1f; // 패럴랙스 이동 속도

    private Material material;
    private Vector2 currentOffset; // 현재 오프셋을 저장

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        currentOffset = material.GetTextureOffset("_MainTex");
    }

    public void Move(Vector2 direction)
    {
        currentOffset += direction * moveSpeed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", currentOffset);
    }

    public void MoveRight()
    {
        Move(Vector2.right);
    }

    public void MoveLeft()
    {
        Move(Vector2.left);
    }

    public void MoveUp()
    {
        Move(Vector2.up);
    }

    public void MoveDown()
    {
        Move(Vector2.down);
    }
}