using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject pooledObject; // 풀링할 오브젝트
    [SerializeField] private int poolSize = 10;       // 초기 풀 사이즈

    private List<GameObject> pool;                    // 오브젝트 리스트

    void Awake()
    {
        pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        GameObject obj = Instantiate(pooledObject);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public void RepositionObject(Vector3 newPosition)
    {
        GameObject obj = GetPooledObject();
        if (obj != null)
        {
            obj.transform.position = newPosition;
            obj.SetActive(true);
        }
    }
}