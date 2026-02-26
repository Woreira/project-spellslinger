using System;
using UnityEngine;
using System.Collections.Generic;

public static class PoolingSystem<T> where T : MonoBehaviour
{
    private static Dictionary<int, FastList<T>> _poolByPrefab = new Dictionary<int, FastList<T>>();
    private static Dictionary<int, int> _instanceByPrefab = new Dictionary<int, int>();

    public static void InitPool(T prefab, int capacity = 50)
    {
        var id = prefab.GetInstanceID();
        var list = new FastList<T>(capacity);

        for (var i = 0; i < capacity; i++)
        {
            var instance = UnityEngine.Object.Instantiate(prefab);
            instance.gameObject.SetActive(false);
            list.Add(instance);
        }

        _poolByPrefab.Add(id, list);
    }

    public static void ExtendPool(FastList<T> pool, T prefab)
    {
        var id = prefab.GetInstanceID();
        var oldCapacity = pool.Capacity;
        pool.Extend(downwards: true);

#if UNITY_EDITOR
        Debug.Log($"[PoolingSystem] Extending [{prefab}] pool, new capacity: [{pool.Capacity}]");
#endif

        for (var i = 0; i < oldCapacity; i++)
        {
            var instance = UnityEngine.Object.Instantiate(prefab);
            instance.gameObject.SetActive(false);
            pool.Add(instance);
        }
    }

    private static bool TryGetPooledPrefab(T prefab, out T instance)
    {
        instance = null;
        if (_poolByPrefab.TryGetValue(prefab.GetInstanceID(), out var pool))
        {
            if (pool.Count <= 0)
            {
                ExtendPool(pool, prefab);
            }

            instance = pool.Pop();
            return true;
        }
        return false;
    }

    public static T Get(T prefab)
    {
        T instance;
        if (!TryGetPooledPrefab(prefab, out instance))
        {
            InitPool(prefab);
            TryGetPooledPrefab(prefab, out instance);
        }

        _instanceByPrefab.Add(instance.GetInstanceID(), prefab.GetInstanceID());
        instance.gameObject.SetActive(true);
        return instance;
    }

    public static T Get(T prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        var instance = Get(prefab);
        instance.transform.parent = parent;
        instance.gameObject.transform.position = position;
        instance.gameObject.transform.rotation = rotation;
        return instance;
    }

    public static T Get(T prefab, Vector3 position, Quaternion rotation)
    {
        return Get(prefab, null, position, rotation);
    }

    public static void Store(T instance)
    {
        var id = instance.GetInstanceID();
        if (!_instanceByPrefab.TryGetValue(id, out var prefabId))
        {
            Debug.LogError($"[PoolingSystem] [{instance.gameObject.name}] pool for instance: [{instance.GetType().ToString()}] could not be found!");
            GameObject.Destroy(instance.gameObject);
            return;
        }

        _poolByPrefab[prefabId].Add(instance);
        _instanceByPrefab.Remove(id);
        instance.gameObject.SetActive(false);
    }

    public static void DrainAll()
    {
        _poolByPrefab = new Dictionary<int, FastList<T>>();
        _instanceByPrefab = new Dictionary<int, int>();
        GC.Collect();
    }

}