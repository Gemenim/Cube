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
        _fuse.Share(_separator.Separate());
    }

    public void Reduce—hance()
    {
        _chanceDivision -= _reducingChance;
    }
}
