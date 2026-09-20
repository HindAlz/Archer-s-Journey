using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer.Mechanics
{
    public class GameOverController : MonoBehaviour
    {
        public PlayerController player;
        public Health health;
        public UIDocument gameOverUI;
        private AudioSource Caudio;
        public AudioClip collect;

        private VisualElement gameOverContainer;
        float time = 2f;
        float timer = 0f;
        bool wait = false;
        void Start()
        {
            Caudio = GetComponent<AudioSource>();

            health = FindObjectOfType<Health>(); 
            gameOverContainer = gameOverUI.rootVisualElement.Q<VisualElement>("VisualElement");
            gameOverContainer.style.display = DisplayStyle.None;
            gameOverContainer.SetEnabled(false);
            Button restartButton = gameOverContainer.Q<Button>("Button");
            restartButton.clicked += RestartGame;

            health.OnDeath += setTimer;
        }

        void Update()
        {
            if (wait)
            {
                timer+= Time.deltaTime;
                if (timer >= time)
                {
                    ShowGameOverUI();
                    wait = false;
                }
            }
        }

        public void setTimer()
        {
            wait = true;
            Caudio.PlayOneShot(collect, 1.0f);
            timer = 0f;
        }

        public void ShowGameOverUI()
        {
            while (time >= 0)
            {
                time -= Time.deltaTime;
                
            }
            
            gameOverContainer.style.display = DisplayStyle.Flex;
            gameOverContainer.SetEnabled(true);
            gameOverContainer.style.opacity = 1;
            
            
        }

        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
            player.animator.Play("PlayerSpawn");
        }
    }
}
