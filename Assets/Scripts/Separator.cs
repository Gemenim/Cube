using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Cube))]
public class Separator : MonoBehaviour
{
    [SerializeField] private Color[] _colors = { Color.red, Color.green, Color.blue, };
    [SerializeField] private int _minNumberParts = 2;
    [SerializeField] private int _maxNumberParts = 6;
    [SerializeField] private Fuse _prefab;

    private Renderer _renderer;
    private Cube _cube;
    private int _multiplierReductionPart = 2;    

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
        _cube = GetComponent<Cube>();
    }

    public List<Rigidbody> Separate()
    {
        List<Rigidbody> cubes = new();

        if (_cube.CanDivision)
            cubes = CreateParts();

        return cubes;
    }

    public void SetParameters()
    {
        transform.localScale /= _multiplierReductionPart;
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
            Cube componentCube = cube.GetComponent<Cube>();
            cube.IncreaseFuse();
            componentCube.Reduce—hance();
            separator.SetParameters();
            cubes.Add(cube.GetComponent<Rigidbody>());
        }

        return cubes;
    }
}
