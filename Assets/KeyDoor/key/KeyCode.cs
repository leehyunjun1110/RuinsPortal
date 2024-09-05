using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCode : MonoBehaviour
{
    public GameObject player = null;
    public GameObject door = null;
    public float speed = 1f;
    private bool updir = true;
    public float updown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && player == null)
        {
            player = other.gameObject;
        }
    }

    void Move(GameObject obj)
    {
        float x = obj.transform.position.x - transform.position.x;
        float y = obj.transform.position.y - transform.position.y;
        transform.Translate(new Vector2(x, y) * Vector2.Distance(new Vector2(obj.transform.position.x, obj.transform.position.y), new Vector2(transform.position.x, transform.position.y)) * speed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            if(updown < 5)
            {
                updown += 10f * Time.deltaTime;
                if(updir == false)
                {
                    transform.Translate(new Vector2(0, -0.5f) * Time.deltaTime);
                }
                else
                {
                    transform.Translate(new Vector2(0, 0.5f) * Time.deltaTime);
                }
            }
            else if(updown >= 5)
            {
                updown = 0;
                if(updir == false)
                {
                    updir = true;
                }
                else
                {
                    updir = false;
                }
            }
        }
        else if(player != null && door == null)
        {
            Move(player);

            GameObject[] allobjects = FindObjectsOfType<GameObject>();
            foreach(GameObject ob in allobjects)
            {
                if(ob.GetComponent<KeyDoor>() != null)
                {
                    if(Vector2.Distance(new Vector2(ob.transform.position.x, ob.transform.position.y), new Vector2(transform.position.x, transform.position.y)) <= 2.2f)
                    {
                        door = ob;
                        speed = speed * 2;
                    }
                }
            }
        }
        if(door != null)
        {
            Move(door);
            if(Vector2.Distance(new Vector2(door.transform.position.x, door.transform.position.y), new Vector2(transform.position.x, transform.position.y)) <= 0.3f)
            {
                door.GetComponent<KeyDoor>().open = true;
                Destroy(gameObject);
            }
        }
    }
}
