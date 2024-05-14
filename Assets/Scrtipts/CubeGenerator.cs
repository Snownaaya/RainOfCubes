using System.Collections;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private PoolCubes _pool;
    [SerializeField] private Cube _cube;

    [SerializeField] private float _delay;
    [SerializeField] private int _horizontalBounds;
    [SerializeField] private int _verticalBounds;

    private Coroutine _coroutine;

    private void Start() => _coroutine = StartCoroutine(GeneratorCubes());

    private void Spawn()
    {
        Cube cubes = _pool.GetCube();
        cubes.gameObject.SetActive(true);
        cubes.transform.position = GetRandomPosition();
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

    private Vector3 GetRandomPosition()
    {
        float positionX = RandomGenerator.Range(_horizontalBounds, _verticalBounds);
        float positionZ = RandomGenerator.Range(_horizontalBounds, _verticalBounds);

        return new Vector3(positionX, transform.position.y, positionZ);
    }
}
