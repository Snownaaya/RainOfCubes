using TMPro;
using UnityEngine;

public class ObjectCounter<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalText;
    [SerializeField] private TMP_Text _activeText;
    [SerializeField] private ObjectPool<T> _pool;

    [SerializeField] private string _nameOfObject;

    private void OnEnable()
    {
        _pool.TotalCountChanged += UpdateText;
        _pool.ActiveCountChanged += UpdateText;
    }

    private void OnDisable()
    {
        _pool.TotalCountChanged -= UpdateText;
        _pool.ActiveCountChanged -= UpdateText;
    }

    private void UpdateText()
    {
        _totalText.text = $"Total: {_pool.TotalSpawnObjectCount} {_nameOfObject}";
        _activeText.text = $"Active: {_pool.ActiveObjectsCount} {_nameOfObject}";
    }
}
