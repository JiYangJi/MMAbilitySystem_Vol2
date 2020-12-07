using MildMania.SharedDataSystem;
using UnityEngine;

[System.Serializable]
public class SharedVariableVector3 : SharedVariable<Vector3>
{
    public SharedVariableVector3(string name, bool isBinded)
    : base(name, isBinded)
    {
    }
}
