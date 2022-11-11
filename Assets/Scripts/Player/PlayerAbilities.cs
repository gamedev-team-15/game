using System.Collections.Generic;
using GameAssets;
using Interfaces;

namespace Player
{
    public class PlayerAbilities : IUpdatable, IPlayerConfigLoader
    {
        private readonly List<AbilityContainer> _abilityContainers = new();
        private PlayerController _player;
        public IEnumerable<AbilityContainer> AbilityContainers => _abilityContainers;

        public void SetPlayer(PlayerController player)
        {
            _player = player;
            foreach (var ability in _abilityContainers)
            {
                ability.AbilityData.Ability.SetPlayer(player);
            }
        }

        public void ActivateAbility(int abilityId)
        {
            if (!IsValidId(abilityId)) return;
            _abilityContainers[abilityId].Activate();
        }

        public void Update(float deltaTime)
        {
            foreach (var ability in _abilityContainers)
            {
                ability.Update(deltaTime);
            }
        }

        private bool IsValidId(int id)
        {
            return id >= 0 && id < _abilityContainers.Count;
        }

        public void LoadConfig(PlayerConfig config)
        {
            _abilityContainers.Clear();
            foreach (var abilityData in config.Abilities)
            {
                abilityData.Ability.SetPlayer(_player);
                _abilityContainers.Add(new AbilityContainer(abilityData));
            }
        }
        
        public class AbilityContainer : IUpdatable
        {
            public PlayerAbilityData AbilityData { get; }
            private float _timer;

            public AbilityContainer(PlayerAbilityData ability)
            {
                AbilityData = ability;
                _timer = ability.Ability.Cooldown;
            }

            public void Update(float deltaTime)
            {
                _timer -= deltaTime;
            }

            public void Activate()
            {
                if (_timer > 0) return;
                AbilityData.Ability.Activate();
                _timer = AbilityData.Ability.Cooldown;
            }
        }
    }
}