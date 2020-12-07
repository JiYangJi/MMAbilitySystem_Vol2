using UnityEngine;
using MildMania.SharedDataSystem;

[System.Serializable]
public class DataBindingTest : ISharedDataBinder
{
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

    private SharedData _sharedData;

    private SharedVariableFloat _floatVar =
        new SharedVariableFloat("TempFloat", true);

    public SharedVariableFloat FloatVar => _floatVar;

    private SharedVariableVector3 _vector3Var = 
        new SharedVariableVector3("TempVector3", true);

    public SharedVariableVector3 Vector3Var => _vector3Var;

    public DataBindingTest(
        SharedData sharedData)
    {
        _sharedData = sharedData;
    }
}

public class Test_DataBindingStressTest : MonoBehaviour
{
    [SerializeField] private int _bindCount = 0;
    [SerializeField] private SharedData _sharedData = null;


    private DataBindingTest[] _testArr;

    private void Awake()
    {
        CreateData();
        Bind();
    }

    private void CreateData()
    {
        _testArr = new DataBindingTest[_bindCount];

        for(int i = 0; i < _testArr.Length; i++)
            _testArr[i] = new DataBindingTest(_sharedData);
    }

    private void Bind()
    {

        _testArr[0].Binder.Bind();

        System.DateTime startTime = System.DateTime.Now;

        for (int i = 1; i < _testArr.Length; i++)
            _testArr[i].Binder.Bind();


        //for (int i = 0; i < _testArr.Length; i++)
        //{
        //    _testArr[i].FloatVar.TryBind(_sharedData);
        //    _testArr[i].Vector3Var.TryBind(_sharedData);
        //}

        //string tempFloatName;

        //for (int i = 0; i < _testArr.Length; i++)
        //    tempFloatName = _testArr[i].FloatVar.Name;

        double duration = System.DateTime.Now.Subtract(startTime).TotalMilliseconds;

        Debug.LogError("Duration: " + duration);
    }
}
