using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class SourceObjects
{
    public string ID;

    public GameObject SourcePrefab;
    public int MinNumberOfObject = 0;
    public bool AllowGrow = true;
    public bool AutoDestroy = true;

    public List<GameObject> clones = new List<GameObject>();
}

public class PoolingSystem : Singleton<PoolingSystem>
{
    public List<SourceObjects> SourceObjects = new List<SourceObjects>();



    public int DefaultCount = 10;

    private void Start()
    {
        InitilizePool();
    }

    public void InitilizePool()
    {
        InitilizeGameObjects();
    }

    private void InitilizeGameObjects()
    {
        for (int i = 0; i < SourceObjects.Count; i++)
        {
            SourceObjects[i].clones = new List<GameObject>();

            int copyNumber = DefaultCount;
            if (SourceObjects[i].MinNumberOfObject != 0)
                copyNumber = SourceObjects[i].MinNumberOfObject;

            for (int j = 0; j < copyNumber; j++)
            {
                GameObject go = Instantiate(SourceObjects[i].SourcePrefab, transform);
                go.SetActive(false);
                if (SourceObjects[i].AutoDestroy)
                    go.AddComponent<PoolObject>();

                SourceObjects[i].clones.Add(go);
            }
        }
    }

    public GameObject InstantiateAPS(string Id)
    {
        for (int i = 0; i < SourceObjects.Count; i++)
        {
            if (string.Equals(SourceObjects[i].ID, Id))
            {
                var inactiveClones = SourceObjects[i].clones.Where(clone => !clone.activeSelf);
                foreach (GameObject inactiveClone in inactiveClones)
                {
                    inactiveClone.SetActive(true);
                    return inactiveClone;
                }

                if (SourceObjects[i].AllowGrow)
                {
                    GameObject go = Instantiate(SourceObjects[i].SourcePrefab, transform);
                    SourceObjects[i].clones.Add(go);
                    IPoolable poolable = go.GetComponent<IPoolable>();
                    if (poolable != null)
                        poolable.Initilize();

                    if (SourceObjects[i].AutoDestroy)
                        go.AddComponent<PoolObject>();
                    return go;
                }

            }
        }
        return null;
    }

    public GameObject InstantiateAPS(string iD, Vector3 position)
    {
        GameObject go = InstantiateAPS(iD);
        if (go)
        {
            go.transform.position = position;
            return go;
        }
        else
            return null;
    }

    public GameObject InstantiateAPS(string iD, Vector3 position, Quaternion rotation)
    {
        GameObject go = InstantiateAPS(iD);
        if (go)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }
        else
            return null;
    }
    public GameObject InstantiateAPS(string iD, Vector3 position, Quaternion rotation,Transform parent)
    {
        GameObject go = InstantiateAPS(iD);
        if (go)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.transform.parent = parent;
            return go;
        }
        else
            return null;
    }
    public GameObject InstantiateAPS(GameObject sourcePrefab)
    {
        for (int i = 0; i < SourceObjects.Count; i++)
        {
            if (ReferenceEquals(SourceObjects[i].SourcePrefab, sourcePrefab))
            {
                for (int j = 0; j < SourceObjects[i].clones.Count; j++)
                {
                    if (!SourceObjects[i].clones[j].activeInHierarchy)
                    {
                        SourceObjects[i].clones[j].SetActive(true);
                        return SourceObjects[i].clones[j];
                    }
                }
                if (SourceObjects[i].AllowGrow)
                {
                    GameObject go = Instantiate(SourceObjects[i].SourcePrefab, transform);
                    SourceObjects[i].clones.Add(go);
                    return go;
                }
            }
        }
        return null;
    }

    public GameObject InstantiateAPS(GameObject sourcePrefab, Vector3 position)
    {
        GameObject go = InstantiateAPS(sourcePrefab);
        if (go)
        {
            go.transform.position = position;
            return go;
        }
        else
            return null;
    }

    public void DestroyAPS(GameObject clone)
    {
        clone.transform.position = transform.position;
        clone.transform.rotation = transform.rotation;
        //clone.transform.localScale = Vector3.one;
        clone.transform.SetParent(transform);

        IPoolable poolable = clone.GetComponent<IPoolable>();
        if (poolable != null)
            poolable.Dispose();
        clone.SetActive(false);
    }

    public void DestroyAPS(GameObject clone, float waitTime)
    {
        StartCoroutine(DestroyAPSCo(clone, waitTime));
    }

    IEnumerator DestroyAPSCo(GameObject clone, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DestroyAPS(clone);
    }
}