using System.Collections.Generic;
using UnityEngine;

public class PoolCubes : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Transform _cointainer;

    private Queue<Cube> _poolCube = new Queue<Cube>();

    public Cube GetCube()
    {
        if (_poolCube.Count == 0)
        {
            Cube cube = Instantiate(_cube, _cointainer);
            cube.Init(this);
            return cube;
        }

        Cube poolCube = _poolCube.Dequeue();
        poolCube.gameObject.SetActive(true);
        return poolCube;
    }

    public void ReturnCube(Cube cube)
    {
        _poolCube.Enqueue(cube);
        cube.gameObject.SetActive(false);
    }
}
