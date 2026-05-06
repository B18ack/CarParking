using UnityEngine;

public class SelectMap : MonoBehaviour
{
    public GameObject city, cyber;

    void Start()
    {
        string map = PlayerPrefs.GetString("NowMap", "City"); // по умолчанию City

        if (map == "Cyber")
        {
            cyber.SetActive(true);
            city.SetActive(false);
        }
        else
        {
            cyber.SetActive(false);
            city.SetActive(true);
        }
    }
}