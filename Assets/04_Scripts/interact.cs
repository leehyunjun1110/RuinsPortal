using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class interact : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _intText;
    [SerializeField] private float _checkRate = 0.05f;
    [SerializeField] private float _maxDistance = 3.0f;
    public LayerMask _layerMask;

    private float _lastCheckTime;
    private GameObject _curObj;

    private IInteractable _curInt;
    

    void Start()
    {
        
        _layerMask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (Time.time - _lastCheckTime > _checkRate)
        {
            _lastCheckTime = Time.time;

           RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _maxDistance, _layerMask);
            Debug.DrawRay(transform.position, Vector2.right * _maxDistance, Color.red);




            if (hit.collider != null)
            {
                if (hit.collider.gameObject != _curObj)
                {
                    _curObj = hit.collider.gameObject;
                    _curInt = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                _curObj = null;
                _curInt = null;
                _intText.gameObject.SetActive(false);
            }
            Debug.Log(_curInt);
        }
        
        if(_curInt != null && Input.GetKeyDown(KeyCode.E))
        {
            _curInt.Interaction();
        }

        
    }

    void SetPromptText()
    {
        Debug.Log("맞음");
        _intText.gameObject.SetActive(true);
        _intText.text = _curInt.GetText();



    }



}

