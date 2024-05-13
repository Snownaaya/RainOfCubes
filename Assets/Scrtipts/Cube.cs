using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifeTime = 2;
    [SerializeField] private int _maxLifeTime = 5;
    [SerializeField] private bool _hasCollider;

    [SerializeField] private MeshRenderer _meshRenderer;

    private Coroutine _coroutine;
    private PoolCubes _pool;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _hasCollider == false)
        {
            _hasCollider = true;
            RandomColor();
            StartLifeCycle();
        }
    }

    public void SetPool(PoolCubes pool) => _pool = pool;

    private void StartLifeCycle()
    {
        float randomLifeTime = RandomGenerator.Range(_minLifeTime, _maxLifeTime);
        _coroutine = StartCoroutine(DestroyAfterDelay(randomLifeTime));
    }

    private IEnumerator DestroyAfterDelay(float randomLifeTime)
    {
        while (enabled)
        {
            yield return new WaitForSeconds(randomLifeTime);
            _pool.ReturnCube(this);
        }
    }

    private void RandomColor() => _meshRenderer.material.color = Random.ColorHSV();
}
