using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip arrowSound;
        public AudioClip fireSound;
        private AudioSource Caudio;
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public GameObject rightArrow;//
        public GameObject leftArrow; //
        public GameObject rightArrowF;//
        public GameObject leftArrowF; //
        bool goRight = true;//
        public GameUIHandler UI;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        bool hasPlayed = false;
        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            Caudio = GetComponent<AudioSource>();

        }

        protected override void Update()
        {
            if (controlEnabled && health.currentHP > 0)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();

            if (Input.GetKeyDown(KeyCode.E) && !hasPlayed && !UI.isFire() && health.currentHP>0)//
            {
                animator.Play("playerAttack");
                hasPlayed = true;
                if (goRight)
                {
                    Caudio.PlayOneShot(arrowSound, 1.0f);
                    Instantiate(rightArrow, transform.position, rightArrow.transform.rotation);
                }
                else
                {
                    Caudio.PlayOneShot(arrowSound, 1.0f);
                    Instantiate(leftArrow, transform.position, leftArrow.transform.rotation);
                }
                Invoke("resetAttack", 0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.E) && !hasPlayed && UI.isFire() && health.currentHP > 0)//
            {
                animator.Play("playerAttack");
                hasPlayed = true;
                if (goRight)
                {
                    Caudio.PlayOneShot(fireSound, 1.0f);
                    Instantiate(rightArrowF, transform.position, rightArrowF.transform.rotation);
                }
                else
                {
                    Caudio.PlayOneShot(fireSound, 1.0f);
                    Instantiate(leftArrowF, transform.position, leftArrowF.transform.rotation);
                }
                Invoke("resetAttack", 0.5f);
            }

        }
            
        void resetAttack()
        {
            hasPlayed = false;
        }
        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            
            if (jump && IsGrounded && health.currentHP > 0)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump && health.currentHP > 0)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f && health.currentHP > 0)
             {

                spriteRenderer.flipX = false;
                goRight = true;
            }
            else if (move.x < -0.01f && health.currentHP > 0)
            {
                spriteRenderer.flipX = true;
                goRight = false;
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
        

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}