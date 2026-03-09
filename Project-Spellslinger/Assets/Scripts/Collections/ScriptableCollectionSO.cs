using UnityEngine;
using System.Collections.Generic;

public class ScriptableCollection<T> : ScriptableObject where T : CollectableSO
{
    public string Path;
    public T[] Collection;
    private Dictionary<string, T> _collectionItemById;

    public virtual void Setup()
    {
        _collectionItemById = new Dictionary<string, T>();
        if (Collection == null) return;

        for (int i = 0; i <= Collection.Length; i++)
        {
            var item = Collection[i];
            if (!_collectionItemById.ContainsKey(item.Id))
            {
                _collectionItemById.Add(item.Id, item);
                continue;
            }

            Debug.LogWarning($"Duplicate item Id detected! [{item.Id}]");
        }
    }

    public bool GetItemById(string id, out T item)
    {
        if (_collectionItemById == null) Setup();
        return _collectionItemById.TryGetValue(id, out item);
    }

    public bool GetItemByIdLinear(string id, out T item)
    {
        for (var i = 0; i < Collection.Length; i++)
        {
            var itemInCollection = Collection[i];
            if (itemInCollection.Id == id)
            {
                item = itemInCollection;
                return true;
            }
        }

        item = null;
        return false;
    }

    [NaughtyAttributes.Button]
    public virtual void Bake()
    {
        Collection = Resources.LoadAll<T>(Path);
    }

}