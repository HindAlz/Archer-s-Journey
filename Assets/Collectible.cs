using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Collectible : MonoBehaviour
    {

        public GameUIHandler UI;
        public AudioClip collect;
        private AudioSource Caudio;
        internal bool collected = false;
        float nextFrameTime = 0;
        public float frameRate = 12;
        bool wait = true;
        float time = .3f;
        float timer = 0f;
        void Start()
        {
            Caudio = GetComponent<AudioSource>();

        }


        void OnTriggerEnter2D(Collider2D other)
        {


            if (other.CompareTag("Player") && !collected)
            {
                Caudio.PlayOneShot(collect, 1.0f);
                collected = true;
                UI.IncCount();

            }
        }
        void LateUpdate()
        {
            if (collected == true)
            {
                if (wait)
                {
                    timer += Time.deltaTime;
                    if (timer >= time)
                    {
                        Destroy(gameObject);
                        wait = false;
                    }
                }
            }
        }



    }
}
