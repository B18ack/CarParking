using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; // номер уровня

    public void LoadLevel()
    {
        PlayerPrefs.SetInt("Game Level", levelIndex);
        SceneManager.LoadScene("Game"); // имя сцены с игрой
    }
}