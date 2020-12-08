using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MildMania.AbilitySystem
{
    public class AbilityHolder : MonoBehaviour
    {
        [SerializeField] private int _abilityIndex = 0;
        public int AbilityIndex
        {
            get => _abilityIndex;
            set => _abilityIndex = value;
        }

        public Ability Ability { get; private set; }
            = new Ability();

        private void OnDestroy()
        {
            Ability.Dispose();
        }
    }
}
