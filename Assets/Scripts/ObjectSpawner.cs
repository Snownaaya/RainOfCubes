using UnityEngine;

public abstract class ObjectSpawner<T> : ObjectPool<T> where T : MonoBehaviour
{
    public void Spawn(Vector3 position)
    {
        T newObject = GetObject();
        newObject.transform.position = position;
        newObject.gameObject.SetActive(true);
        InitializeObject(newObject);
    }

    protected abstract void InitializeObject(T @object);
}
