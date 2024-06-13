using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fuse))]
[RequireComponent(typeof(Separator))]
public class Cube : MonoBehaviour
{
    private int _maxChanceDivision = 100;
    private int _chanceDivision = 100;
    private int _dividerChance = 2;

    private Fuse _fuse;
    private Separator _separator;

    public int ChanceDivision => _chanceDivision;
    public Fuse Fuse => _fuse;

    private bool _canDivision => Random.Range(0, _maxChanceDivision) <= _chanceDivision;

    private void Awake()
    {
        _fuse = GetComponent<Fuse>();
        _separator = GetComponent<Separator>();
    }

    private void OnMouseUpAsButton()
    {
        if (_canDivision)
            _fuse.Share(_separator.CreateParts());
        else        
            _fuse.Share(TakeRigidbodyRaycast());
    }

    public void Init(Cube cube)
    {
        _separator.Init();
        Reduce—hance(cube.ChanceDivision);
        _fuse.Init(cube.Fuse.ExplosionForce, cube.Fuse.ExplosionRadius);
    }

    private void Reduce—hance(int chanceDivision)
    {
        _chanceDivision = chanceDivision / _dividerChance;
    }

    private List<Rigidbody> TakeRigidbodyRaycast()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _fuse.ExplosionRadius);
        List<Rigidbody>  rigidbodies = new();

        foreach (Collider hit in hits) 
        { 
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }

        return rigidbodies;
    }
}
