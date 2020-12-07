using System;
using System.Reflection;

namespace MildMania.SharedDataSystem
{
    public class SharedDataBinder
    {
        private ISharedDataBinder _dataBinder;
        private SharedData _sharedData;

        public SharedDataBinder(
            ISharedDataBinder dataBinder,
            SharedData data)
        {
            _dataBinder = dataBinder;
            _sharedData = data;
        }

        public void Bind()
        {
            FieldInfo[] fields = _dataBinder.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            Type baseType = typeof(SharedVariable);

            foreach(FieldInfo field in fields)
            {
                if (!baseType.IsAssignableFrom(field.FieldType))
                    continue;

                SharedVariable sharedVariable = (SharedVariable)field.GetValue(_dataBinder);

                sharedVariable.TryBind(_sharedData);
            }
        }
    }
}
