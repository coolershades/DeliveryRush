using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeliveryRush
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] protected GameObject menuUI;
        [SerializeField] protected Courier courier;
        [SerializeField] protected MapBuilder mapBuilder;
        
        public void Start()
        {
            menuUI.SetActive(false);
            Time.timeScale = 1;
        }

        public void Activate()
        {
            menuUI.SetActive(true);
            Time.timeScale = 0;
        }
        
        public void Restart()
        {
            courier.Reset();
            mapBuilder.Reset();
            
            Start();
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}