using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Health : MonoBehaviour
    {
        public int maxHP = 1;
        public bool IsAlive => currentHP > 0;

        public event Action OnDeath;

        public int currentHP;
        public GameUIHandler UI;

        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UI.HealthChanged();
        }

        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0)/////////////////
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
                OnDeath?.Invoke(); 
            }
            UI.HealthChanged();

        }

        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
