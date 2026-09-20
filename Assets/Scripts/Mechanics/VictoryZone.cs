using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        public SummaryUI UI;
        public ParticleSystem win;
        public AudioClip collect;
        private AudioSource Caudio;
        void Start()
        {
            Caudio = GetComponent<AudioSource>();

        }
        void OnTriggerEnter2D(Collider2D collider)
        { 
            
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                Caudio.PlayOneShot(collect, 1.0f);
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
                UI.Reached(true);
                win.Play();
            }

        }
   }
}