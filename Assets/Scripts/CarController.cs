using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 5f, finalSpeed = 15f, rotateSpeed = 50f;
    private bool isClicked;

    private float curPointX, curPointY;

    [NonSerialized] public Vector3 FinalPosition;

    public enum Axis
    {
        Vertical, Horizontal
    }

    public Axis CarAxis;

    private enum Direction
    {
        Right, Left, Top, Bottom, None
    }

    private Direction CarDirectionX = Direction.None; 
    private Direction CarDirectionY = Direction.None;

    public Text CountMoves, CountMoney;
    public GameObject StartGameBtn;

    private static int CountCars;

    private AudioSource _audio;
    public AudioClip AudioStart, AudioCrash;
    public ParticleSystem CrashEffect;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();

    }

    void OnMouseDown()
    {
        if (!StartGame.IsGameStarted) return;   // для нажатия пальцем

        curPointX = Input.mousePosition.x; // Input.GetTouch(0).position.x
        curPointY = Input.mousePosition.y; // Input.GetTouch(0).position.y
    }

    void OnMouseUp()
    {
        if (!StartGame.IsGameStarted) return;

        int moves = int.Parse(CountMoves.text);

        if (moves <= 0) return;

        if (Input.mousePosition.x - curPointX > 0)
            CarDirectionX = Direction.Right;
        else
            CarDirectionX = Direction.Left;

        if (Input.mousePosition.y - curPointY > 0)
            CarDirectionY = Direction.Top;
        else
            CarDirectionY = Direction.Bottom;

        isClicked = true;


        moves--;
        CountMoves.text = moves.ToString();

        _audio.Stop();
        _audio.clip = AudioStart;
        _audio.pitch = UnityEngine.Random.Range(1.4f, 1.8f);
        _audio.Play();

        StartGameBtn.GetComponent<StartGame>().CheckLose();
    }

    void Update()
    {

        if (FinalPosition.x != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, FinalPosition, finalSpeed * Time.deltaTime);

            Vector3 lookAtPos = FinalPosition - transform.position;
            lookAtPos.y = 0;

            if (lookAtPos.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(lookAtPos),
                    Time.deltaTime * rotateSpeed
                );
            }
        }

        if (transform.position == FinalPosition)
        {
            PlayerPrefs.SetInt("CarCoins", PlayerPrefs.GetInt("CarCoins") + 1);

            CountMoney.text = (int.Parse(CountMoney.text) + 1).ToString();

            GameManager.CountCars--;

            Destroy(gameObject);

            if (GameManager.CountCars <= 0 && StartGame.IsGameStarted)
            {
                StartGameBtn.GetComponent<StartGame>().WinGame();
            }
        }
    }
    void FixedUpdate()
    {
        if (isClicked && FinalPosition.x == 0)
        {
            Vector3 whichWay = CarAxis == Axis.Horizontal ? Vector3.forward : Vector3.left;

            speed = Mathf.Abs(speed);
            if (CarDirectionX == Direction.Left && CarAxis == Axis.Horizontal)
                speed *= -1;
            else if (CarDirectionY == Direction.Bottom && CarAxis == Axis.Vertical)
                speed *= -1;

            _rb.MovePosition(_rb.position + whichWay * speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Barier"))
        {

            Destroy(
                Instantiate(CrashEffect, other.ClosestPoint(transform.position), Quaternion.Euler(new Vector3(270f, 0, 0))),
            2f);

            if (_audio.clip != AudioCrash && !_audio.isPlaying)
            {
                _audio.Stop();

                _audio.pitch = UnityEngine.Random.Range(0.9f, 1.3f); // 🔥 ускорение/разнообразие
                _audio.PlayOneShot(AudioCrash); // 🔥 лучше чем clip + Play()
            }




                if (CarAxis == Axis.Horizontal && isClicked)
            {
                float adding = CarDirectionX == Direction.Left ? 0.5f : -0.5f;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z + adding);
            }

            if (CarAxis == Axis.Vertical && isClicked)
            {
                float adding = CarDirectionY == Direction.Top ? 0.5f : -0.5f;
                transform.position = new Vector3(transform.position.x + adding, 0, transform.position.z);
            }
            isClicked = false;
        }
    }

}
