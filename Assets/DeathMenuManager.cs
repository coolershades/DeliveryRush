using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryRush
{
    public class DeathMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _deathMenuUI;
        [SerializeField] private Text _score;

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