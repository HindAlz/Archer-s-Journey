using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer.Mechanics
{
    public class SummaryUI : MonoBehaviour
    {
        public PlayerController player;
        public Health health;
        public UIDocument summaryUI;
        public GameUIHandler gameUIHandler;
        private VisualElement container;
        private Label amtCollected;
        private int total;
        static public bool reached=false;
        private void Awake()
        {
            container = summaryUI.rootVisualElement.Q<VisualElement>("VisualElement");
            amtCollected = summaryUI.rootVisualElement.Q<Label>("amtCollected");

        }
        void Start()
        {
            container.style.display = DisplayStyle.None;
            container.SetEnabled(false);


        }

        void Update()
        {
            total = gameUIHandler.totalCollected();
            amtCollected.text = total.ToString();

            if (reached)
            {
                showSummaryUI();
                reached = false;

            }
        }
        public void Reached(bool a)
        {
            reached = a;
        }

        public void showSummaryUI()
        {

            container.style.display = DisplayStyle.Flex;
            container.SetEnabled(true);
            container.style.opacity = 1;


        }

        
    }
}
