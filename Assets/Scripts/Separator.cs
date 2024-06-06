using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Separator : MonoBehaviour
{
    [SerializeField] private int _minNumberParts = 2;
    [SerializeField] private int _maxNumberParts = 6;
    [SerializeField] private Color[] _colors = { Color.red, Color.green, Color.blue, };
    [SerializeField] private Fuse _prefab;

    private Renderer _renderer;
    private int _multiplierReductionPart = 2;

    private int _maxChanceDivision = 100;
    private int _chanceDivision = 100;
    private int _reducingChance = 20;

    private void OnValidate()
    {

        if (_minNumberParts < 2)
            _minNumberParts = 2;

        if (_maxNumberParts < _minNumberParts)
            _maxNumberParts = _minNumberParts + 1;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public List<Rigidbody> Separate()
    {
        bool canDivision = Random.Range(0, _maxChanceDivision) <= _chanceDivision;
        List<Rigidbody> cubes = new();

        if (canDivision)
            cubes = CreateParts();

        return cubes;
    }

    public void SetParameters()
    {
        transform.localScale /= _multiplierReductionPart;
        _chanceDivision -= _reducingChance;
        _renderer.material.color = _colors[Random.Range(0, _colors.Length)];
    }

    private List<Rigidbody> CreateParts()
    {
        int numberParts = Random.Range(_minNumberParts, _maxNumberParts + 1);
        List<Rigidbody> cubes = new();

        for (int i = 0; i < numberParts; i++)
        {
            Fuse cube = Instantiate(_prefab, transform.position, Quaternion.identity);
            Separator separator = cube.GetComponent<Separator>();
            separator.SetParameters();
            cubes.Add(cube.GetComponent<Rigidbody>());
        }

        return cubes;
    }
}
