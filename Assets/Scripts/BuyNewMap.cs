using UnityEngine;
using UnityEngine.UI;

public class BuyNewMap : MonoBehaviour
{
    public Text CountCoins;

    public void BuyMap(string mapName, int price)
    {
        if (PlayerPrefs.GetInt("CarCoins") >= price)
        {
            PlayerPrefs.SetInt("CarCoins", PlayerPrefs.GetInt("CarCoins") - price);
            PlayerPrefs.SetInt(mapName + "_Bought", 1); // сохраняем что куплена
            CountCoins.text = PlayerPrefs.GetInt("CarCoins").ToString();
        }
    }
}
