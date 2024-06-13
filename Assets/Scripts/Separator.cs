using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Separator : MonoBehaviour
{
    [SerializeField] private Color[] _colors = { Color.red, Color.green, Color.blue, };
    [SerializeField] private int _minNumberParts = 2;
    [SerializeField] private int _maxNumberParts = 6;
    [SerializeField] private Cube _prefab;

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

    public void Init()
    {
        transform.localScale /= _multiplierReductionPart;
        _renderer.material.color = _colors[Random.Range(0, _colors.Length)];
    }

    public List<Rigidbody> CreateParts()
    {
        int numberParts = Random.Range(_minNumberParts, _maxNumberParts + 1);
        List<Rigidbody> cubes = new();

        for (int i = 0; i < numberParts; i++)
        {
            Cube cube = Instantiate(_prefab, transform.position, Quaternion.identity);
            cube.Init(_cube);
            cubes.Add(cube.GetComponent<Rigidbody>());
        }

        return cubes;
    }
}
