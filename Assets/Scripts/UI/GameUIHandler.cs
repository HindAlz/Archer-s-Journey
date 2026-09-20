using UnityEngine;
using UnityEngine.UIElements;
namespace Platformer.Mechanics
{
    public class GameUIHandler : MonoBehaviour
    {
        public PlayerController PlayerControl;
        public Health health;
        public UIDocument UIDoc;
        private Label ItemNo;
        private int total=0;
        public static bool fire=false;
        float time = 7f;
        float timer = 0f;

        private VisualElement m_HealthBarMask;
        private Label m_HealthLabel;
        private VisualElement fireImage;
        private VisualElement waterImage;
        private VisualElement grassImage;
        private void Awake()
        {
            ItemNo = UIDoc.rootVisualElement.Q<Label>("ItemNo");
           
        }

        private void Start()
        {
            fireImage = UIDoc.rootVisualElement.Q<VisualElement>("fire");
            fireImage.style.display = DisplayStyle.None;
            fireImage.SetEnabled(false);
            
            m_HealthLabel = UIDoc.rootVisualElement.Q<Label>("HealthLabel");
            UpdateCounter();
            m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
            HealthChanged();
        }


        public void HealthChanged()
        {
            float healthRatio = (float)health.currentHP / health.maxHP;
            float healthPercent = Mathf.Lerp(8, 88, healthRatio);
            m_HealthBarMask.style.width = Length.Percent(healthPercent);
        }

        public void UpdateCounter()
        {
          
            if (ItemNo == null)
            {
                return;
            }

            ItemNo.text = total.ToString();
        

        }

        public void IncCount()
        {
            total++;
            UpdateCounter();
        }

        public void showPower()
        {
            Debug.Log("show");
            fire = true;

        }

        void Update()
        {
            if (fire==true)
            {
                
                if (timer < time)
                {
                    timer+=Time.deltaTime;
                    fireImage.style.display = DisplayStyle.Flex;
                    fireImage.SetEnabled(true);
                    fireImage.style.opacity = 1;
                }
                else 
                {
                    Debug.Log("end");

                    fire = false;
                    timer = 0f;
                    fireImage.style.display = DisplayStyle.None;
                    fireImage.SetEnabled(false);
                }
               
            }
            
        }

        public bool isFire()
        {
            return fire;
        }

        public int totalCollected()
        {
            return total;
        }
    }
}