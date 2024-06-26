using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Rigidbody _exposionObject;
    private Material _material;
    private Color _originalColor;
    private BombGenerator _generator;

    private float _fadeTime = 5f;

    private void Awake()
    {
        _exposionObject = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
        _originalColor = _material.color;
    }

    private void OnEnable() =>
        StartCoroutine(FadeOut());

    public void Init(BombGenerator bombGenerator) =>
        _generator = bombGenerator;

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            _exposionObject = hit.attachedRigidbody;

            if (_exposionObject)
                _exposionObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        _generator.ReturnObject(this);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeTime);
            Color newColor = new Color(_originalColor.r, _originalColor.g, _originalColor.b, alpha);
            _material.color = newColor;
            yield return null;
        }

        Explode();
    }
}
