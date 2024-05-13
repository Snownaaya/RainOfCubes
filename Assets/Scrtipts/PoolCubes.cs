using System.Collections.Generic;
using UnityEngine;

public class PoolCubes : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Transform _cointainer;

    private Queue<Cube> _pool = new Queue<Cube>();

    public Cube GetCube()
    {
        if (_pool.Count == 0)
        {
            Cube cube = Instantiate(_cube, _cointainer);
            cube.SetPool(this);
            return cube;
        }

        Cube poolCube = _pool.Dequeue();
        poolCube.gameObject.SetActive(true);
        return poolCube;
    }

    public void ReturnCube(Cube cube)
    {
        _pool.Enqueue(cube);
        cube.gameObject.SetActive(false);
    }
}