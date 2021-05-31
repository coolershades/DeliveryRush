using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DeliveryRush
{
    public class DeathMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _deathMenuUI;
        
        [SerializeField] private Courier courier;
        [SerializeField] private MapBuilder mapBuilder;

        private void Start()
        {
            _deathMenuUI.SetActive(false);
        }

        public void Activate()
        {
            _deathMenuUI.SetActive(true);
            Time.timeScale = 0;
        }

        public void Restart()
        {
            courier.Reset();
            mapBuilder.Reset();
            Time.timeScale = 1;
            
            Start();
        }

        public void ToMainMenu()
        {
            throw new NotImplementedException();
        }

        public void SaveAndQuit()
        {
            // Application.Quit();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}