using System;
using UnityEngine;

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
        }

        public void Restart()
        {
            courier.Reset();
            mapBuilder.Reset();
            
            Start();
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