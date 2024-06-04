using System.Collections;
using UnityEngine;

public class CubeGenerator : ObjectPool<Cube>
{
    [SerializeField] private float _delay;
    [SerializeField] private int _horizontalBounds;
    [SerializeField] private int _verticalBounds;

    [SerializeField] private BombGenerator _generator;

    private Coroutine _coroutine;

    private void Start() =>
        _coroutine = StartCoroutine(GeneratorCubes());

    public void Spawn()
    {
        Cube cubes = GetObject();
        cubes.gameObject.SetActive(true);
        cubes.Initialized(this, _generator);
        cubes.transform.position = SetRandomPosition();
    }

    private IEnumerator GeneratorCubes()
    {
        var waitForSecond = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return waitForSecond;
        }
    }

    private Vector3 SetRandomPosition()
    {
        float positionX = RandomGenerator.Range(_horizontalBounds, _verticalBounds);
        float positionZ = RandomGenerator.Range(_horizontalBounds, _verticalBounds);

        return new Vector3(positionX, transform.position.y, positionZ);
    }
}
