using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public static bool IsGameStarted = false;

    public GameObject Logo, PlayImage, CountMoves, LoseText, WinText, ShopImage, Levels;

    private bool IsLoseGame = false, IsWinGame = false;
    public static bool CanLose = false;
    public static int TotalCarsInLevel;

    private Text movesText;

    void Start()
    {
        IsGameStarted = false;
        IsLoseGame = false;
        IsWinGame = false;

        Time.timeScale = 1f;

        LoseText.SetActive(false);
        WinText.SetActive(false);

        TotalCarsInLevel = FindObjectsByType<CarController>().Length;
        GameManager.CountCars = TotalCarsInLevel;
    }

    public void PlayGame()
    {
        if (!IsLoseGame && !IsWinGame)
        {
            IsGameStarted = true;

            Logo.SetActive(false);
            ShopImage.SetActive(false);
            PlayImage.SetActive(false);
            Levels.SetActive(false);
            CountMoves.SetActive(true);

            CanLose = true;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void WinGame()
    {
        if (IsWinGame) return;

        IsWinGame = true;
        IsLoseGame = false; 

        IsGameStarted = false;

        WinText.SetActive(true);
        LoseText.SetActive(false); 

        Logo.SetActive(true);
        ShopImage.SetActive(true);
        PlayImage.SetActive(true);

        PlayerPrefs.SetInt("Game Level", PlayerPrefs.GetInt("Game Level") + 1);
    }

    public void LoseGame()
    {
        if (IsLoseGame) return;

        IsLoseGame = true;
        IsWinGame = false; 

        IsGameStarted = false;

        LoseText.SetActive(true);
        WinText.SetActive(false); 

        Logo.SetActive(true);
        ShopImage.SetActive(true);
        PlayImage.SetActive(true);
    }


    public void CheckLose()
    {
        if (CountMoves == null) return;

        Text txt = CountMoves.GetComponent<Text>();
        if (txt == null) return;

        int moves = int.Parse(txt.text);

        if (moves <= 0 && CanLose)
        {
            LoseGame();
        }
    }
}