using UnityEngine;
using Platformer.Mechanics;
namespace Cainos.PixelArtPlatformer_VillageProps
{
    public class Chest : MonoBehaviour
    {
        private Animator animator;
        private bool isOpened = false;
        public GameUIHandler UI;

        void Start()
        {
            animator = GetComponent<Animator>();
            animator.Play("Closed");


        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !isOpened)
            {

                UI.showPower();
                animator.Play("opened");
                isOpened = true;
            }
        }
    }
}
