using System;
using UnityEngine;

[Serializable]
public class ComplexClass
{
    [SerializeField] private float _fVar1 = 0;
    [SerializeField] private float _fVar2 = 0;

    [SerializeField] private float _fVar3 = 0;
    public float FVar3 => _fVar3;

}
