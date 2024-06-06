using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Separator))]
public class Fuse : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private float _explosionForce = 5;

    private Separator _separator;
    private Renderer _renderer;

    private void OnValidate()
    {
        if (_explosionRadius < 0)
            _explosionRadius = 0;
    }

    private void Start()
    {
        _separator = GetComponent<Separator>();
    }

    private void OnMouseUpAsButton()
    {
        Share();
    }

    private void Share()
    {
        Explode(_separator.Separate());
        Destroy(gameObject);
    }

    private void Explode(List<Rigidbody> cubes)
    {
        foreach (Rigidbody cube in cubes)
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
