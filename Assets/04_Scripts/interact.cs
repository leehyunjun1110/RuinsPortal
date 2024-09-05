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
    [SerializeField] private Transform targetParent;
    [SerializeField] private float itemRange = 1.5f;

    public GameObject rayPoint;

    private float _lastCheckTime;
    private GameObject _curObj;
    private GameObject _pickedObj;
    private IInteractable _curInt;

    private bool _interactable;
    private bool _picked = false;
    
    void Start()
    {
        StartCoroutine(CheckForInteractable()); 
    }

    void Update()
    {
        if (Input.GetKeyDown(UnityEngine.KeyCode.E))
        {
            if (_picked)
            {
                PlaceObject();
            }
            else if (_interactable)
            {
                PickupObject();
            }
        }

        if (_pickedObj != null)
        {
            UpdatePickedObjectPosition();
        }
    }

    private IEnumerator CheckForInteractable()
    {
        while (true)
        {
            if (Time.time - _lastCheckTime >= _checkRate)
            {
                _lastCheckTime = Time.time;
                RaycastHit2D hitRight = Physics2D.Raycast(rayPoint.transform.position, Vector2.right, _maxDistance, _layerMask);
                RaycastHit2D hitLeft = Physics2D.Raycast(rayPoint.transform.position, Vector2.left, _maxDistance, _layerMask);
                if (hitRight.collider != null && !_picked)
                {
                    _curObj = hitRight.collider.gameObject;
                    _curInt = _curObj.GetComponent<IInteractable>();

                    if (_curInt != null)
                    {
                        _interactable = true;
                        SetPromptText();
                    }
                }
                else if (hitLeft.collider != null && !_picked)
                {
                    _curObj = hitLeft.collider.gameObject;
                    _curInt = _curObj.GetComponent<IInteractable>();

                    if (_curInt != null)
                    {
                        _interactable = true;
                        SetPromptText();
                    }
                }
                else
                {
                    _interactable = false;
                    _curInt = null;
                    _intText.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

    void UpdatePickedObjectPosition()
    {
        if (_pickedObj != null)
        {
            _pickedObj.transform.position = transform.position + transform.right * itemRange; // 플레이어 오른쪽 1.5 유닛 거리에 위치
        }
    }

    void PickupObject()
    {
        if (_curObj != null)
        {
            _pickedObj = Instantiate(_curObj, transform.position + transform.right * itemRange, _curObj.transform.rotation);
            
            if (targetParent != null)
            {
                _pickedObj.transform.SetParent(targetParent);
            }
            else
            {
                _pickedObj.transform.SetParent(transform);
            }

            _pickedObj.GetComponent<CircleCollider2D>().isTrigger = true;
            _curObj.SetActive(false);
            _picked = true;
            _intText.gameObject.SetActive(false);
        }
    }

    void PlaceObject()
    {
        if (_pickedObj != null)
        {
                _curObj.transform.position = _pickedObj.transform.position;
                _curObj.SetActive(true);

                Destroy(_pickedObj);
                _pickedObj = null;
                _picked = false;
        }
    }

    void SetPromptText()
    {
        if (_curInt != null)
        {
            _intText.gameObject.SetActive(true);
            _intText.text = _curInt.GetText();
        }
        else
        {
            _intText.gameObject.SetActive(false);
        }
    }
}