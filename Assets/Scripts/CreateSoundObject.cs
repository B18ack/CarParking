using UnityEngine;

public class CreateSoundObject : MonoBehaviour
{

    public GameObject SoundController;
    private static bool IsCreated = false;

    void Start()
    {
        if (IsCreated) return;

        IsCreated = true;   
        DontDestroyOnLoad(Instantiate(SoundController));
    }
}
