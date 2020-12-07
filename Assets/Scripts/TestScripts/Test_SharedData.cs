using MildMania.SharedDataSystem;
using UnityEngine;


public class Test_SharedData : MonoBehaviour, ISharedDataBinder
{
    [SerializeField] private SharedData _sharedData = null;

    [SerializeField] private SharedVariableFloat _floatVar = null;

    [SerializeField] private SharedVariableVector3 _vector3Var = null;

    [SerializeField] private SharedVariableComplexClass _complexClassVar = null;

    private SharedDataBinder _binder;
    public SharedDataBinder Binder
    {
        get
        {
            if (_binder == null)
                _binder = new SharedDataBinder(this, _sharedData);

            return _binder;
        }
    }

    private void Awake()
    {
        Binder.Bind();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log(_floatVar.Value);
            Debug.Log(_vector3Var.Value);
            Debug.Log(_complexClassVar.Value);
        }
    }
}
