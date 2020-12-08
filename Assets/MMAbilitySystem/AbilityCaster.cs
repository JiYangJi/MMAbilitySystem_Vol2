using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MildMania.AbilitySystem
{
    public class AbilityCaster : MonoBehaviour
    {
        [SerializeField] private bool _isDebugEnabled = false;
        public bool IsDebugEnabled => _isDebugEnabled;

        [SerializeField] private Transform _abilityCarrier = null;

        private List<AbilityHolder> _abilityHolders;
        public List<AbilityHolder> AbilityHolders
        {
            get
            {
                if (_abilityHolders == null)
                    _abilityHolders = _abilityCarrier.
                        GetComponentsInChildren<AbilityHolder>().ToList();

                return _abilityHolders;
            }
        }

        public Action<Ability> OnAbilityCasted { get; set; }
        public Action<Ability, bool> OnAbilityTerminated { get; set; }

        public bool TryAttachAbility(
            AbilityHolder abilityHolder,
            int abilityIndex)
        {
            if (GetAbilityHolder(abilityIndex) != null)
                return false;

            abilityHolder.AbilityIndex = abilityIndex;

            AbilityHolders.Add(abilityHolder);

            return true;
        }

        public bool TryDeattachAbility(int abilityIndex)
        {
            AbilityHolder abilityHolder = GetAbilityHolder(abilityIndex);

            if (abilityHolder == null)
                return false;

            AbilityHolders.Remove(abilityHolder);

            UnityEngine.Object.Destroy(abilityHolder);

            return true;
        }

        public bool TryCastAbility(
            int abilityIndex,
            out Ability ability,
            AbilityMessage message = null,
            IAbilityActionMessage triggeringMessage = null,
            Action<Ability> onCastingCallback = null,
            Action<Ability, bool> onTerminatedCallback = null)
        {
            AbilityHolder abilityHolder = null;
            ability = null;

            if (!TryCreateAbility(abilityIndex, out abilityHolder))
                return false;

            ability = abilityHolder.Ability;
            ability.ResetResetable();

            ability.OnAbilityTerminated += onAbilityTerminated;

            onCastingCallback?.Invoke(ability);

            ability.Cast(this, message, triggeringMessage);

            void onAbilityTerminated(Ability terminatedAbility, bool didExecutionCompleted)
            {
                terminatedAbility.OnAbilityTerminated -= onAbilityTerminated;

                onTerminatedCallback?.Invoke(terminatedAbility, didExecutionCompleted);
                AbilityTerminated(abilityHolder, didExecutionCompleted);
            }

            OnAbilityCasted?.Invoke(ability);

            return true;
        }

        private bool TryCreateAbility(int abilityIndex, out AbilityHolder abilityHolder)
        {
            abilityHolder = null;

            AbilityCreator ac = GetAbilityHolder(abilityIndex);

            if (ac == null)
                return false;

            abilityHolder = ac.SelfAbilityHolder;

            return true;
        }

        private AbilityHolder GetAbilityHolder(int abilityIndex)
        {
            return AbilityHolders.FirstOrDefault(val => val.AbilityIndex == abilityIndex);
        }

        private void AbilityTerminated(AbilityHolder abilityHolder, bool didExecutionCompleted)
        {
            abilityHolder.Ability.OnAbilityTerminated -= OnAbilityTerminated;

            OnAbilityTerminated?.Invoke(abilityHolder.Ability, didExecutionCompleted);

            /*if(abilityHolder != null)
                Destroy(abilityHolder.gameObject);*/
        }
    }
}
