using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifeTime = 2;
    [SerializeField] private int _maxLifeTime = 5;

    [SerializeField] private MeshRenderer _meshRenderer;

    private Color _initialColor;
    private Coroutine _coroutine;
    private PoolCubes _pool;

    private bool _hasTouched;

    private void Awake() => _initialColor = _meshRenderer.material.color;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _hasTouched == false)
        {
            _hasTouched = true;
            GetRandomColor();
            StartLifeCycle();
        }
    }

    public void Init(PoolCubes pool) => _pool = pool;

    private void StartLifeCycle()
    {
        float randomLifeTime = RandomGenerator.Range(_minLifeTime, _maxLifeTime);
        _coroutine = StartCoroutine(DestroyAfterDelay(randomLifeTime));
    }

    private void ResetCube()
    {
        _hasTouched = false;
        _meshRenderer.material.color = _initialColor;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator DestroyAfterDelay(float randomLifeTime)
    {
        yield return new WaitForSeconds(randomLifeTime);
        ResetCube();
        _pool.ReturnCube(this);
    }

    private void GetRandomColor() => _meshRenderer.material.color = Random.ColorHSV();
}
