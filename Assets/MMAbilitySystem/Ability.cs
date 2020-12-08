using MildMania.SharedDataSystem;
using System;
using UnityEngine;

namespace MildMania.AbilitySystem
{
    [System.Serializable]
    public sealed class Ability : AbilityActionBase
    {
        [SerializeField] private SharedData _sharedDataTemplate = null;

        public SharedData SharedData { get; private set; }

        public Action<Ability> OnCastStarted { get; set; }
        public Action<Ability, bool> OnAbilityTerminated { get; set; }

        public Ability()
        {
            SharedData = UnityEngine.Object.Instantiate(_sharedDataTemplate);
        }

        public void Cast(
            AbilityCaster caster)
        {
            Caster = caster;

            CheckAndInvoke(Caster, this);
        }

        protected override void Invoke()
        {
            OnCastStarted?.Invoke(this);
        }
    }
}
