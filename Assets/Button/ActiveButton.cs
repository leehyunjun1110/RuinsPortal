using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    public GameObject activeobject;
    public GameObject bublock;
    private bool pressing = false;
    private GameObject presobject;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(pressing == false)
        {
            pressing = true;
            presobject = other.gameObject;
            bublock.transform.localPosition = new Vector3(0, 0.2f, -0.01f);
            activeobject.GetComponent<ActiveGimmick>().active = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(pressing == true && presobject == other.gameObject)
        {
            pressing = false;
            presobject = null;
            bublock.transform.localPosition = new Vector3(0, 0.4f, -0.01f);
            activeobject.GetComponent<ActiveGimmick>().active = false;
        }
    }
}
