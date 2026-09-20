using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        static int hitTimes = 0;
        public PatrolPath path;
        public AudioClip ouch;
        private Animator animator;
        public AudioClip collect;
        private AudioSource Caudio;
        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        bool dead=false;
        bool emidead = false;
        SpriteRenderer spriteRenderer;
       

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            Caudio = GetComponent<AudioSource>();

        }



        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
           

        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Arrow"))
            {
                Caudio.PlayOneShot(collect, 1.0f);
                hitTimes++;
                Debug.Log(hitTimes);
                if (hitTimes == 3)
                {
                    animator.SetTrigger("death");
                    emidead = true;
                    Destroy(other.gameObject);

                    Destroy(gameObject, 0.9f);
                    hitTimes = 0;
                }
                else
                {
                    Destroy(other.gameObject);

                    animator.SetTrigger("hurt");
                }
            }
            else if (other.CompareTag("FireArrow"))
            {
                Caudio.PlayOneShot(collect, 1.0f);
                animator.SetTrigger("death");
                    emidead = true;
                    Destroy(other.gameObject);

                    Destroy(gameObject, 0.9f);
                    hitTimes = 0;
               
            }
            var playerHealth = other.GetComponent<Health>();
            var playerAnimator = other.GetComponent<Animator>();
            if (other.CompareTag("Player") && !dead && !emidead)
            {
                dead = true;
                playerHealth.Decrement();
                playerAnimator.SetTrigger("dead");
            }
        }

    }
}