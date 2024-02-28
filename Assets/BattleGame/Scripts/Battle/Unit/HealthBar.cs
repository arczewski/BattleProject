using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AFSInterview
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthAttribute healthAttribute;
        [SerializeField] private Slider healthSlider;

        private void Start()
        {
            healthSlider.maxValue = healthAttribute.MaxHealth;
            healthSlider.value = healthAttribute.Health;
        }

        public void OnHealthReduced(int health, int value)
        {
            healthSlider.value = health;
        }
    }
}
