using UnityEngine;
using UnityEngine.UI;

public class MapItemSimple : MonoBehaviour
{
    [Header("Map Settings")]
    public string mapName; // "Cyber" или "City"
    public int price = 100;

    [Header("UI")]
    public GameObject buyButton;   // кнопка "Купить"
    public GameObject checkMark;   // зелёная галочка (выбрано)
    public Text coinsText;         // текст с монетами

    void Start()
    {
        UpdateUI();
    }

    // 🔄 Обновление UI
    public void UpdateUI()
    {
        bool isBought = PlayerPrefs.GetInt(mapName + "_Bought", 0) == 1;
        bool isSelected = PlayerPrefs.GetString("NowMap", "City") == mapName;

        // ❌ НЕ КУПЛЕНО
        if (!isBought)
        {
            buyButton.SetActive(true);
            checkMark.SetActive(false);
        }
        // 🎯 ВЫБРАНО
        else if (isSelected)
        {
            buyButton.SetActive(false);
            checkMark.SetActive(true);
        }
        // ✅ КУПЛЕНО НО НЕ ВЫБРАНО
        else
        {
            buyButton.SetActive(false);
            checkMark.SetActive(false);
        }
    }

    // 🟡 Купить карту
    public void Buy()
    {
        int coins = PlayerPrefs.GetInt("CarCoins", 0);

        if (coins >= price)
        {
            PlayerPrefs.SetInt("CarCoins", coins - price);
            PlayerPrefs.SetInt(mapName + "_Bought", 1);

            if (coinsText != null)
                coinsText.text = PlayerPrefs.GetInt("CarCoins").ToString();

            UpdateAllMaps();
        }
        else
        {
            Debug.Log("Недостаточно монет");
        }
    }

    // 🟢 Выбрать карту
    public void Select()
    {
        if (PlayerPrefs.GetInt(mapName + "_Bought", 0) == 1)
        {
            PlayerPrefs.SetString("NowMap", mapName);
            UpdateAllMaps();
        }
    }

    // 🔄 Обновить ВСЕ карты (без устаревшего FindObjectsOfType)
    void UpdateAllMaps()
    {
        var maps = Object.FindObjectsByType<MapItemSimple>(FindObjectsInactive.Exclude);

        foreach (var map in maps)
        {
            map.UpdateUI();
        }
    }
}