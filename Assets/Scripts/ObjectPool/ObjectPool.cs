using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T: MonoBehaviour
{
    private Queue<T> _objectQueue;
    private GameObject _prefab;

    private static ObjectPool<T> _instance = null;
    public static ObjectPool<T> Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new ObjectPool<T>();
            }
            return _instance;
        }
    }

    public int queueCount
    {
        get { return _objectQueue.Count; }
    }

    public void Initialize(GameObject prefab)
    {
        _prefab = prefab;
        _objectQueue = new Queue<T>();
    }

    //spawnTarget is used to seperate enemy type
    public T SpawnObject(GameObject spawnTarget, Vector3 position, Quaternion quaternion)
    {
        if(_prefab == null)
        {
            Debug.LogError(typeof(T).ToString() + " prefab not set");
            return default(T);
        }

        if(queueCount <= 0)
        {
            GameObject go = Object.Instantiate(spawnTarget, position, quaternion);
            T t = go.GetComponent<T>();
            if(t == null)
            {
                Debug.LogError(typeof(T).ToString() + " not found in prefab");
                return default(T);
            }
            _objectQueue.Enqueue(t);
        }
        T obj = _objectQueue.Dequeue();
        obj.gameObject.transform.position = position;
        obj.gameObject.transform.rotation = quaternion;
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void RecycleObject(T obj)
    {
        _objectQueue.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
}
