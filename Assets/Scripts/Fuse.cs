using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private float _explosionForce = 5;
    [SerializeField] private float _increaseRadius = 1;
    [SerializeField] private float _increaseForce = 1;

    public float ExplosionForce => _increaseForce;
    public float ExplosionRadius => _explosionRadius;

    private void OnValidate()
    {
        if (_explosionRadius < 0)
            _explosionRadius = 0;

        if (_increaseForce < 0)
            _increaseForce = 0;

        if ( _increaseRadius < 0)
            _increaseRadius = 0;
    }

    public void Init(float explosionForce, float explosionRadius)
    {
        _explosionForce = explosionForce + _increaseForce;
        _explosionRadius = explosionRadius + _increaseRadius;
    }

    public void Share(List<Rigidbody> cubes)
    {
        Explode(cubes);
        Destroy(gameObject);
    }

    private void Explode(List<Rigidbody> cubes)
    {
        foreach (Rigidbody cube in cubes)
        {
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
