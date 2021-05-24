using System;
using UnityEngine;

namespace DeliveryRush
{
    public class DeathMenuManager : MonoBehaviour
    {
        public GameObject _deathMenuUI;

        private void Start()
        {
            _deathMenuUI.SetActive(false);
            Time.timeScale = 1;
        }

        public void TriggerDeath()
        {
            _deathMenuUI.SetActive(true);
            Time.timeScale = 0;
        }

        public void Restart()
        {
            
        }

        public void ToMainMenu()
        {
            throw new NotImplementedException();
        }

        public void SaveAndQuit()
        {
            Application.Quit();
        }
    }
}