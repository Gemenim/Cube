using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fuse))]
[RequireComponent(typeof(Separator))]
public class Cube : MonoBehaviour
{
    private int _maxChanceDivision = 100;
    private int _chanceDivision = 100;
    private int _reducingChance = 20;

    private Fuse _fuse;
    private Separator _separator;

    public bool CanDivision => Random.Range(0, _maxChanceDivision) <= _chanceDivision;

    private void Awake()
    {
        _fuse = GetComponent<Fuse>();
        _separator = GetComponent<Separator>();
    }

    private void OnMouseUpAsButton()
    {
        List<Rigidbody> rigidbodies = _separator.Separate();

        if (rigidbodies.Count > 0)
        {
            _fuse.Share(rigidbodies);
        }
        else
        {
            TakeRigidbodyRaycast(out rigidbodies);
            _fuse.Share(rigidbodies);
        }
    }

    public void Reduce—hance()
    {
        _chanceDivision -= _reducingChance;
    }

    private void TakeRigidbodyRaycast(out List<Rigidbody> rigidbodies)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _fuse.ExplosionRadius);
        rigidbodies = new();

        foreach (Collider hit in hits) 
        { 
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }
    }
}
