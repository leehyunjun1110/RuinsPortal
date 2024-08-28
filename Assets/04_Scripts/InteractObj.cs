using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObj : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isUp;
    [SerializeField] private bool isDown;

    public string GetText()
    {
        if (isUp)
        {
            return "이미 물건이 들려있습니다.";
        }
        else if (isDown)
        {
            return "상호작용이 가능합니다.";
        }
        else
        {
            return "상호작용을 중지합니다.";
        }
    }

    public void Interaction() // 메서드 이름 변경
    {
        if (isDown)
        {
            Debug.Log("듦");
            isDown = false; // 상태 변경
            isUp = true; // 상태 변경
        }
        else if (isUp)
        {
            Debug.Log("내려놓음");
            isUp = false; // 상태 변경
            isDown = true; // 상태 변경
        }
    }

    private void Awake()
    {
        isUp = false;
        isDown = true;
    }
}
