using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class interact : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _intText;
    [SerializeField] private float _checkRate = 0.05f;
    [SerializeField] private float _maxDistance = 3.0f;
    [SerializeField] private LayerMask _layerMask;

    private float _lastCheckTime;
    private GameObject _curObj;

    private IInteractable _curInt;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        _layerMask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (Time.time - _lastCheckTime > _checkRate)
        {
            _lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxDistance, _layerMask))
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
        }
        
        if(_curInt != null && Input.GetKeyDown(KeyCode.E))
        {
            _curInt.Interaction();
        }
    }

    void SetPromptText()
    {
        _intText.gameObject.SetActive(true);
        _intText.text = _curInt.GetText();



    }



}

