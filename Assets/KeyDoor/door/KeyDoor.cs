using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public bool open = false;
    private SpriteRenderer sr;
    private BoxCollider2D cld;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cld = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(open == false)
        {
            sr.color = new Color32(255, 255, 255, 255);
            cld.enabled = true;
        }
        else
        {
            sr.color = new Color32(255, 255, 255, 105);
            cld.enabled = false;
        }
    }
}
