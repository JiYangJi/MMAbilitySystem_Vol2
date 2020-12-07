using MildMania.SharedDataSystem;
using UnityEngine;

public class TestData : SharedData
{
    [SerializeField] private float _tempFloat = 0;
    public float TempFloat
    {
        get => _tempFloat;
        set => _tempFloat = value;
    }

    [SerializeField] private Vector3 _tempVector3 = Vector3.zero;
    public Vector3 TempVector3
    {
        get => _tempVector3;
        set => _tempVector3 = value;
    }

    [SerializeField] private ComplexClass _tempComplexClass = null;
    public ComplexClass TempComplexClass
    {
        get => _tempComplexClass;
        set => _tempComplexClass = value;
    }
}
