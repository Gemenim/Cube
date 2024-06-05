using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private int _minNumberParts = 2;
    [SerializeField] private int _maxNumberParts = 6;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private float _explosionForce = 5;
    [SerializeField] private Color[] _colors = { Color.red, Color.green, Color.blue, };
    [SerializeField] Fuse _prefab;

    private int _maxChanceDivision = 100;
    private int _chanceDivision = 100;
    private int _multiplierReductionPart = 2;
    private int _reducingChance = 20;

    private void OnValidate()
    {
        if (_minNumberParts < 2)
            _minNumberParts = 2;

        if (_maxNumberParts < _minNumberParts)
            _maxNumberParts = _minNumberParts + 1;

        if (_explosionRadius < 0)
            _explosionRadius = 0;
    }

    private void OnMouseUpAsButton()
    {
        Division();
    }

    private void Division()
    {
        bool canShare = Random.Range(0, _maxChanceDivision) <= _chanceDivision;

        if (canShare)
        {
            CreateParts();
            Explode();
            Destroy(gameObject);
        }
        else
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void CreateParts()
    {
        int numberParts = Random.Range(_minNumberParts, _maxNumberParts + 1);

        for (int i = 0; i < numberParts; i++)
        {
            Fuse cube = Instantiate(_prefab, transform.position, Quaternion.identity);
            cube.SetParameters();
        }
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

    public void SetParameters()
    {
        transform.localScale /= _multiplierReductionPart;
        _chanceDivision -= _reducingChance;
        gameObject.GetComponent<Renderer>().material.color = _colors[Random.Range(0, _colors.Length)];
    }
}
