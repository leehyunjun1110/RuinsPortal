using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGimmick : MonoBehaviour
{
    public float trapnumber = 1; // 1, 1.5 = 왕복하여 움직임(1: 위아래 / 1.5: 좌우) || 2, 2.5 = 작동 밖에 못함(2: 위아래 / 2.5: 좌우)
    public bool active = false;
    public List<GameObject> objs = new List<GameObject>();
    public float speed = 1;
    public float posi = 4;
    private float oripos;
    // Start is called before the first frame update
    void Start()
    {
        if(trapnumber == 1 || trapnumber == 2)
        {
            oripos = transform.position.y;
        }
        else if(trapnumber == 1.5f || trapnumber == 2.5f)
        {
            oripos = transform.position.x;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        bool none = true;
        for(int ix = objs.Count - 1; ix >= 0; ix--)
        {
            if(objs[ix] == other.gameObject)
            {
                none = false;
            }
        }
        if(none == true && other.gameObject.GetComponent<Rigidbody2D>() != null && other.gameObject.GetComponent<Rigidbody2D>().gravityScale != 0 && other.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            if(other.transform.position.y - other.transform.localScale.y * other.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.05f >= transform.position.y + transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2)
            {
                objs.Add(other.gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        for(int ix = objs.Count - 1; ix >= 0; ix--)
        {
            if(objs[ix] == other.gameObject)
            {
                objs.RemoveAt(ix);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true)
        {
            if(trapnumber == 1)
            {
                if(posi > 0 && transform.position.y < oripos + posi)
                {
                    transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
                    }
                }
                else if(posi < 0 && transform.position.y > oripos + posi)
                {
                    transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
                    }
                }
            }
            else if(trapnumber == 1.5f)
            {
                if(posi > 0 && transform.position.x < oripos + posi)
                {
                    transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
                    }
                }
                else if(posi < 0 && transform.position.x > oripos + posi)
                {
                    transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
                    }
                }
            }
            else if(trapnumber == 2)
            {
                if(posi > 0 && transform.position.y < oripos + posi)
                {
                    transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
                    }
                }
                else if(posi < 0 && transform.position.y > oripos + posi)
                {
                    transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
                    }
                }
            }
            else if(trapnumber == 2.5f)
            {
                if(posi > 0 && transform.position.x < oripos + posi)
                {
                    transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
                    }
                }
                else if(posi < 0 && transform.position.x > oripos + posi)
                {
                    transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
                    }
                }
            }
        }
        else if(active == false)
        {
            if(trapnumber == 1)
            {
                if(transform.position.y < oripos)
                {
                    transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
                    }
                }
                else if(transform.position.y > oripos)
                {
                    transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
                    }
                }
            }
            else if(trapnumber == 1.5f)
            {
                if(transform.position.x < oripos)
                {
                    transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
                    }
                }
                else if(transform.position.x > oripos)
                {
                    transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
                    for(int ix = objs.Count - 1; ix >= 0; ix--)
                    {
                        objs[ix].transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
