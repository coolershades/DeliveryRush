using System;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _endMenuUI;
    [SerializeField] private Text _coinsDisplay;

    private void Start()
    {
        _endMenuUI.SetActive(false);
    }

    public void Activate()
    {
        Time.timeScale = 0;
        _endMenuUI.SetActive(true);

        _coinsDisplay.text = "$420";
    }

    public void Restart()
    {
        throw new NotImplementedException();
    }

    public void SaveAndQuit()
    {
        throw new NotImplementedException();
    }
}
