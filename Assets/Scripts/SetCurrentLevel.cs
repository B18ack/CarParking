using UnityEngine;
using UnityEngine.UI;


public class SetCurrentLevel : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "#" + PlayerPrefs.GetInt("Game Level");
    }
}
