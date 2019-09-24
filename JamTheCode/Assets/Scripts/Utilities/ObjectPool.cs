using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject Prefab;
    public int InitialSize = 16;

    private List<GameObject> instances = new List<GameObject>();

    private void Start()
    {
        for (var i = 0; i < InitialSize; i++)
        {
            var go = CreateInstance();
            go.transform.parent = transform;
            go.SetActive(false);
        }
    }

    private GameObject CreateInstance()
    {
        var go = Instantiate(Prefab) as GameObject;
        go.transform.parent = transform;
        instances.Add(go);
        return go;
    }

    public GameObject GetObject()
    {
        foreach (var instance in instances)
        {
            if (instance.activeSelf != true)
            {
                instance.SetActive(true);
                return instance;
            }
        }
        return CreateInstance();
    }
}