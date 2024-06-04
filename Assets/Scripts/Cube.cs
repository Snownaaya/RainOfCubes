using System.Collections;
using UnityEngine;
using System;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifeTime = 2;
    [SerializeField] private int _maxLifeTime = 5;

    [SerializeField] private MeshRenderer _meshRenderer;

    private BombGenerator _bombGenerator;
    private CubeGenerator _generator;
    private Color _initialColor;
    private Coroutine _coroutine;
    private ObjectPool<Cube> _pool;

    private bool _hasTouched = false;

    private void Awake() =>
        _initialColor = _meshRenderer.material.color;

    private void OnEnable() =>
        ResetCube();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _hasTouched == false)
        {
            _hasTouched = true;
            GetRandomColor();
            StartLifeCycle();
        }
    }

    public void Initialized(CubeGenerator cubeGenerator, BombGenerator bombGenerator)
    {
        _generator = cubeGenerator;
        _bombGenerator = bombGenerator;
    }

    private void StartLifeCycle()
    {
        float randomLifeTime = RandomGenerator.Range(_minLifeTime, _maxLifeTime);
        _coroutine = StartCoroutine(DestroyAfterDelay(randomLifeTime));
    }

    private void ResetCube()
    {
        _hasTouched = false;
        _meshRenderer.material.color = _initialColor;
    }

    private IEnumerator DestroyAfterDelay(float randomLifeTime)
    {
        yield return new WaitForSeconds(randomLifeTime);
        _bombGenerator.Spawn(transform.position);
        _generator.ReturnObject(this);
    }

    private void GetRandomColor() => _meshRenderer.material.color = UnityEngine.Random.ColorHSV();
}
