using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject pooledObject;
    [SerializeField] private int pooledAmount = 10;

    private List<GameObject> pooledObjects;
    private GameObject poolContainer;

    // Creates the pool of objects
    private void Awake()
    {
        pooledObjects = new List<GameObject>();
        poolContainer = new GameObject($"Pool - {pooledObject.name}");

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.transform.parent = poolContainer.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Returns a pooled object, or creates a new one if none are available
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // If we get here, we need to create a new object
        GameObject obj = Instantiate(pooledObject);
        pooledObjects.Add(obj);
        return obj;
    }

    public static void ReturToPool(GameObject instance)
    {
        instance.SetActive(false);
    }
}
