using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefabs;
    [SerializeField] private Transform _cointainer;

    private Queue<T> _poolObject = new Queue<T>();

    public int TotalSpawnObjectCount { get; private set; }
    public int ActiveObjectsCount { get; private set; }

    public event Action TotalCountChanged;
    public event Action ActiveCountChanged;

    public virtual T GetObject()
    {
        if (_poolObject.Count == 0)
        {
            _poolObject.Enqueue(Instantiate(_prefabs, _cointainer));
        }

        TotalSpawnObjectCount++;
        TotalCountChanged?.Invoke();

        ActiveObjectsCount++;
        ActiveCountChanged?.Invoke();

        return _poolObject.Dequeue();
    }

    public virtual void ReturnObject(T objectToReturn)
    {
        _poolObject.Enqueue(objectToReturn);

        ActiveObjectsCount--;
        ActiveCountChanged?.Invoke();

        objectToReturn.gameObject.SetActive(false);
    }
}
