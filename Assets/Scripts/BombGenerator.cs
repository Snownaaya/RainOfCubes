using UnityEngine;

public class BombGenerator : ObjectPool<Bomb>
{
    [SerializeField] private Cube _cube;
    [SerializeField] private CubeGenerator _cubeGenerator;

    public void Spawn(Vector3 position)
    {
        Bomb bomb = GetObject();
        bomb.gameObject.SetActive(true);
        bomb.Init(this, _cubeGenerator);
        bomb.transform.position = position;
    }
}
