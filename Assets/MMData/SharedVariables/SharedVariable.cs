using System;
using System.Reflection;
using UnityEngine;
using System.Linq.Expressions;
using System.Security;

[assembly: SecurityTransparent]
namespace MildMania.SharedDataSystem
{
    [Serializable]
    public abstract class SharedVariable
    {
        [SerializeField] private string _name = "";
        public string Name => _name;

        [SerializeField] protected bool _isBinded = false;
        [SerializeField] protected bool _bindToValueChange = false;

        public SharedVariable()
        {

        }

        public SharedVariable(
            string name,
            bool isBinded)
        {
            _name = name;
            _isBinded = isBinded;
        }

        public abstract bool TryBind<TData>(TData sharedData)
            where TData : SharedData;
    }


    [Serializable]
    public abstract class SharedVariable<TType> : SharedVariable
    {

        [SerializeField] private TType _value = default;
        public TType Value
        {
            get
            {
                if(_isBinded 
                    && _bindedProperty != null)
                    _value = _getter(_sharedData);

                return _value;
            }
            set
            {
                _value = value;

                if (_isBinded
                    && _bindedProperty != null)
                    _setter(_sharedData, _value);
            }
        }

        private SharedData _sharedData;
        private PropertyInfo _bindedProperty;

        private delegate TType Getter(SharedData data);
        private delegate void Setter(SharedData data, TType value);

        private static Getter _getter;
        private static Setter _setter;

        public SharedVariable(
            string name,
            bool isBinded)
            : base(name, isBinded)
        {
        }

        public SharedVariable()
        {
            Value = default;
        }

        public sealed override bool TryBind<TData>(TData sharedData)
        {
            if (!_isBinded)
                return false;

            _sharedData = sharedData;

            _bindedProperty = _sharedData.GetType().GetProperty(Name);

            if (_bindedProperty == null)
                return false;

            TryInitGetterDelegate(_bindedProperty.GetGetMethod());
            TryInitSetterDelegate(_bindedProperty.GetSetMethod());

            _value = _getter(_sharedData);

            return true;
        }

        private static void TryInitGetterDelegate(MethodInfo method)
        {
            if (_getter != null)
                return;

            var instanceParameter = Expression.Parameter(typeof(SharedData));
            var body = Expression.Call(
                Expression.Convert(instanceParameter, method.DeclaringType),
                method);
            var lambda = Expression.Lambda<Getter>(body, instanceParameter);

            System.DateTime startTime = System.DateTime.Now;
            _getter = lambda.Compile();

            double duration = System.DateTime.Now.Subtract(startTime).TotalMilliseconds;

            //Debug.LogError("Duration2: " + duration);
        }

        private static void TryInitSetterDelegate(MethodInfo method)
        {
            if (_setter != null)
                return;

            var instanceParameter = Expression.Parameter(typeof(SharedData));
            var valueParameter = Expression.Parameter(typeof(TType));
            var body = Expression.Call(
                Expression.Convert(instanceParameter, method.DeclaringType),
                method,
                valueParameter);
            var lambda = Expression.Lambda<Setter>(body, instanceParameter, valueParameter);

            _setter = lambda.Compile();
        }
    }
}
