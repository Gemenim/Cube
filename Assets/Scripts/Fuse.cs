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
        _separator.Separate();
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObgects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObgects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
