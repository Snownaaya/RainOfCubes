using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefabs;
    [SerializeField] private Transform _cointainer;

    private Queue<T> _poolObject = new Queue<T>();

    public virtual T GetObject()
    {
        if (_poolObject.Count == 0)
        {
            T gameObject = Instantiate(_prefabs, _cointainer);
            return gameObject;
        }

        T poolObject = _poolObject.Dequeue();
        return poolObject;
    }

    public virtual void ReturnObject(T gameObject)
    {
        _poolObject.Enqueue(gameObject);
        gameObject.gameObject.SetActive(false);
    }
}
