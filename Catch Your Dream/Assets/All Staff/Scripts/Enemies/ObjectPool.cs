
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }


    [System.Serializable]
    public class PoolObject
    {
        public string tag;
        [HideInInspector] public List<GameObject> objectsPools;
        public GameObject[] objectPrefabs;
        [HideInInspector] public int amountToPool;
    }

    public List<PoolObject> pools;
    [HideInInspector] public int index;
    [HideInInspector] public int indexForSide;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        foreach (PoolObject pool in pools)
        {
            pool.objectsPools = new List<GameObject>();
            pool.amountToPool = pool.objectPrefabs.Length;

            for(int i = 0; i < pool.amountToPool; i++)
            {
                GameObject tmp = Instantiate(pool.objectPrefabs[i]);
                tmp.SetActive(false);
                pool.objectsPools.Add(tmp);
            }
        }
    }

    public GameObject PooledObject(string tag)
    {
        

        PoolObject pool = pools.Find(pool => pool.tag == tag);

        int i = Random.Range(0, pool.objectsPools.Count);

        switch (i)
        {
            case 0: index = 0; indexForSide = 0; break;
            case 1: index = 1; indexForSide = 1; break;
            case 2: index = 2; break;
        }

        if (!pool.objectsPools[i].activeSelf)
        {
            return pool.objectsPools[i];
        }
        else
        {
            return null;
        }
        

    }

}
