using MildMania.SharedDataSystem;

[System.Serializable]
public class SharedVariableFloat : SharedVariable<float>
{
    public SharedVariableFloat(string name, bool isBinded) 
        : base(name, isBinded)
    {
    }
}
