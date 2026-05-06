using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public void OpenNewScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OpenCity()
    {
        PlayerPrefs.SetString("NowMap", "City");
        SceneManager.LoadScene("Game");
    }

    public void OpenCyber()
    {
        PlayerPrefs.SetString("NowMap", "Cyber");
        SceneManager.LoadScene("Game");
    }
}