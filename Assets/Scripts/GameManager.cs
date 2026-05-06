using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int CountCars;

    void Start()
    {
        CountCars = FindObjectsByType<CarController>().Length;
    }
}