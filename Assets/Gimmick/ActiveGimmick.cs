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

    public void MoveObjects(Vector2 vc2)
    {
        transform.Translate(vc2 * speed * Time.deltaTime);
        for(int ix = objs.Count - 1; ix >= 0; ix--)
        {
            objs[ix].transform.Translate(vc2 * speed * Time.deltaTime);
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
        GameObject[] allobjects = FindObjectsOfType<GameObject>();
        foreach(GameObject ob in allobjects)
        {
            bool none = true;
            for(int ix = objs.Count - 1; ix >= 0; ix--)
            {
                if(objs[ix] == ob.gameObject)
                {
                    none = false;
                }
            }
            if(ob.GetComponent<BoxCollider2D>() != null)
            {
                if(none == true && ob.transform.position.x + ob.transform.localScale.x * ob.GetComponent<BoxCollider2D>().size.x / 2 >= transform.position.x - transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2 && ob.transform.position.x - ob.transform.localScale.x * ob.GetComponent<BoxCollider2D>().size.x / 2 <= transform.position.x + transform.localScale.x * GetComponent<BoxCollider2D>().size.x && ob.transform.position.y - ob.transform.localScale.y * ob.GetComponent<BoxCollider2D>().size.y / 2 + 0.1f >= transform.position.y + transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2)
                {
                    if(ob != gameObject)
                    {
                        objs.Add(ob);
                    }
                }
                else if(none == false)
                {
                    if(ob.transform.position.x + ob.transform.localScale.x * ob.GetComponent<BoxCollider2D>().size.x / 2 < transform.position.x - transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2 || ob.transform.position.x - ob.transform.localScale.x * ob.GetComponent<BoxCollider2D>().size.x / 2 > transform.position.x + transform.localScale.x * GetComponent<BoxCollider2D>().size.x || ob.transform.position.y - ob.transform.localScale.y * ob.GetComponent<BoxCollider2D>().size.y / 2 + 0.1f < transform.position.y + transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2)
                    {
                        for(int ix = objs.Count - 1; ix >= 0; ix--)
                        {
                            if(objs[ix] == ob.gameObject)
                            {
                                objs.RemoveAt(ix);
                            }
                        }
                    }
                }
            }
        }
        if(active == true)
        {
            if(trapnumber == 1)
            {
                if(posi > 0 && transform.position.y < oripos + posi)
                {
                    MoveObjects(new Vector2(0, 1));
                }
                else if(posi < 0 && transform.position.y > oripos + posi)
                {
                    MoveObjects(new Vector2(0, -1));
                }
            }
            else if(trapnumber == 1.5f)
            {
                if(posi > 0 && transform.position.x < oripos + posi)
                {
                    MoveObjects(new Vector2(1, 0));
                }
                else if(posi < 0 && transform.position.x > oripos + posi)
                {
                    MoveObjects(new Vector2(-1, 0));
                }
            }
            else if(trapnumber == 2)
            {
                if(posi > 0 && transform.position.y < oripos + posi)
                {
                    MoveObjects(new Vector2(0, 1));
                }
                else if(posi < 0 && transform.position.y > oripos + posi)
                {
                    MoveObjects(new Vector2(0, -1));
                }
            }
            else if(trapnumber == 2.5f)
            {
                if(posi > 0 && transform.position.x < oripos + posi)
                {
                    MoveObjects(new Vector2(1, 0));
                }
                else if(posi < 0 && transform.position.x > oripos + posi)
                {
                    MoveObjects(new Vector2(-1, 0));
                }
            }
        }
        else if(active == false)
        {
            if(trapnumber == 1)
            {
                if(transform.position.y < oripos)
                {
                    MoveObjects(new Vector2(0, 1));
                }
                else if(transform.position.y > oripos)
                {
                    MoveObjects(new Vector2(0, -1));
                }
            }
            else if(trapnumber == 1.5f)
            {
                if(transform.position.x < oripos)
                {
                    MoveObjects(new Vector2(1, 0));
                }
                else if(transform.position.x > oripos)
                {
                    MoveObjects(new Vector2(-1, 0));
                }
            }
        }
    }
}
