using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DistanceManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button btnForInput;
    [SerializeField] private GameObject loseGame;
    [SerializeField] private AudioSource errorAudio;

    private float initialY;

    public static DistanceManager InstanceDist { get; private set; }

    private void Awake()
    {
        InstanceDist = this;
    }

    private void Start()
    {
        initialY = player.transform.position.y;
    }

    private void Update()
    {
        if (pauseMenu.activeSelf)
        {
            GameFollow.Instance.PauseGame();
        }

        if (CountSystem.Instance.isOver)
        {
            btnForInput.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            VoiceMovement.Instance.StartRecording();
        }
    }

    public void SetMovement(string response)
    {
        Dictionary<string, string> commands = new Dictionary<string, string>
        {
            { "le", "left" },
            { "Le", "left" },
            { "Ri", "right" },
            { "ri", "right" },
            { "si", "sit" },
            { "Si", "sit" },
            { "Ju", "jump" },
            { "ju", "jump" },
            { "ce", "center" },
            { "Ce", "center" },
            { "th", "center" },
            { "Th", "center" },
            { "Home", "quitGame" },
            { "ome", "quitGame" },
            { "Pause", "pauseGame" },
            { "ause", "pauseGame" },
            { "s", "pauseGame" },
            { "z", "pauseGame" },
            { "Pa", "pauseGame" },
            { "Play", "playGame" },
            { "lay", "playGame" },
        };

        string command = "";

        response = response.ToLower();
        Debug.Log($"Voice response: {response}");

        foreach (var c in commands)
        {
            if (response.Contains(c.Key))
            {
                command = c.Value;
                break;
            }
        }

        Debug.Log($"Parsed command: {command}");

        switch (command)
        {
            case "left":
                MovePlayerLeft();
                break;
            case "right":
                MovePlayerRight();
                break;
            case "sit":
                SitPlayer();
                break;
            case "jump":
                JumpPlayer();
                break;
            case "center":
                MovePlayerCenter();
                break;
            case "quitGame":
                SceneManager.LoadScene(0);
                SceneFlow.Instance.SaveChoice();
                break;
            case "pauseGame":
                pauseMenu.SetActive(true);
                btnForInput.gameObject.SetActive(true);
                break;
            case "playGame":
                if (!pauseMenu.activeSelf)
                {
                    SceneManager.LoadScene(1);
                }
                GameFollow.Instance.ResumeGame();
                pauseMenu.SetActive(false);
                btnForInput.gameObject.SetActive(false);
                loseGame.SetActive(false);
                player.SetActive(true);
                break;
            default:
                Debug.LogWarning("Unrecognized command");
                break;
        }
    }

    private void MovePlayerLeft()
    {
        Debug.Log("Move player left");
        player.transform.Translate(Vector3.left * 1.5f);
    }

    private void MovePlayerRight()
    {
        Debug.Log("Move player right");
        player.transform.Translate(Vector3.right * 1.5f);
    }

    private void SitPlayer()
    {
        Debug.Log("Sit player");
        player.transform.Translate(Vector3.down * 1.0f);
    }

    private void JumpPlayer()
    {
        Debug.Log("Jump player");
        player.transform.Translate(Vector3.up * 10.0f);

    }

    private void MovePlayerCenter()
    {
        Debug.Log("Move player to center");
        player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
    }

   
}
