using System.Collections.Generic;
using AbilitySystem;
using Interfaces;
using JetBrains.Annotations;

namespace Player
{
    public class PlayerAbilities : IUpdatable
    {
        private readonly List<Ability> _abilities = new();

        public void Update(float deltaTime)
        {
            
        }
    }
}