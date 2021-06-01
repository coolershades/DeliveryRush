using DeliveryRush;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuManager : MenuManager
{
    [SerializeField] private Text coinsDisplay;
    public const string MoneyBank = "MoneyBank";

    // https://proglib.io/p/sohranenie-igrovyh-dannyh-v-unity-2020-04-17
    
    public void Activate(float parcelPreservationStatus)
    {
        Time.timeScale = 0;
        menuUI.SetActive(true);

        var moneyMade = (int) (parcelPreservationStatus * mapBuilder.currentMapLength);

        coinsDisplay.text = moneyMade + " r.";

        var previouslyEarnedMoney = PlayerPrefs.GetInt(MoneyBank);
        PlayerPrefs.SetInt(MoneyBank, previouslyEarnedMoney + moneyMade);
    }
}
