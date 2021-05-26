using System;
using System.Text;
using DeliveryRush;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    public float SecondsToEnd;
    public Text CountdownDisplay;

    [SerializeField] private DeathMenuManager _deathMenuManager;
    
    private const float SecondsPerBuilding = 1f;

    /*public void Start()
    {
        SecondsToEnd = 30f;
    }*/

    public void SetTime(int buildingsCount) => SecondsToEnd = buildingsCount * SecondsPerBuilding;

    public void Update()
    {
        SecondsToEnd -= Time.deltaTime;
        CountdownDisplay.text = TimerFormat((int) SecondsToEnd);
        
        if (SecondsToEnd <= 0) _deathMenuManager.TriggerDeath();
    }

    private string TimerFormat(int seconds)
    {
        var stringBuilder = new StringBuilder();
        var minutes = seconds / 60;

        stringBuilder
            .Append(TimeFormat(minutes))
            .Append(":")
            .Append(TimeFormat(seconds - minutes * 60));
        
        return stringBuilder.ToString();
    }

    private string TimeFormat(int time) => (time < 10 ? "0" : "") + time;
}
