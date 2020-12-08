using MildMania.SharedDataSystem;
using System;
using System.Collections.Generic;

namespace MildMania.AbilitySystem
{
    public abstract class AbilityActionBase : ISharedDataBinder, IDisposable, ICloneable
    {
        public AbilityCaster Caster { get; protected set; }
        public Ability Ability { get; private set; }

        public List<AbilityActionBase> ActionStartedActionList { get; set; }
            = new List<AbilityActionBase>();

        public SharedDataBinder Binder { get; private set; }

        public Action<AbilityActionBase> OnActionStarted { get; set; }

        public void InitAction(
            Ability ability,
            SharedData sharedData = null)
        {
            Ability = ability;

            if (sharedData != null)
            {
                Binder = new SharedDataBinder(this, sharedData);
                Binder.Bind();
            }
        }

        public void CheckAndInvoke(
            AbilityCaster caster,
            Ability ability)
        {
            Caster = caster;
            Ability = ability;

            if (Caster == null)
            {
                //Ability.Terminate(false);
                return;
            }


            OnActionStarted?.Invoke(this);

            Invoke();

            CheckAndInvokeTriggeringActions(
                ActionStartedActionList);
        }

        protected abstract void Invoke();

        protected void CheckAndInvokeTriggeringActions(
            List<AbilityActionBase> actionList)
        {
            foreach (AbilityActionBase action in actionList)
                action.CheckAndInvoke(Caster, Ability);
        }
    }
}
