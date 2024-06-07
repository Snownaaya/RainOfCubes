using System.Collections;
using UnityEngine;

public class CubeGenerator : ObjectSpawner<Cube>
{
    [SerializeField] private float _delay;
    [SerializeField] private int _horizontalBounds;
    [SerializeField] private int _verticalBounds;

    [SerializeField] private BombGenerator _generator;

    private void Start() =>
        StartCoroutine(GeneratorCubes());

    protected override void InitializeObject(Cube @object)
    {
        @object.Initialized(this, _generator);
    }

    private IEnumerator GeneratorCubes()
    {
        var waitForSecond = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn(SetRandomPosition());
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
