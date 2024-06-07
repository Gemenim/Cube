using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private float _explosionForce = 5;

    private void OnValidate()
    {
        if (_explosionRadius < 0)
            _explosionRadius = 0;
    }    

    public void Share(List<Rigidbody> cubes)
    {
        Explode(cubes);
        Destroy(gameObject);
    }

    private void Explode(List<Rigidbody> cubes)
    {
        foreach (Rigidbody cube in cubes)
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
