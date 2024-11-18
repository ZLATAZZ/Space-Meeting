using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetTheVoiceInput : MonoBehaviour
{
    
    [SerializeField] private GameObject voiceRules;

    public Button voiceDetectBtn;

    public AudioSource rulesAudio;

    [SerializeField] private GameObject quizTest;

    [SerializeField] private GameObject errorSpeaker;
    public AudioSource errorAudio;


    public static GetTheVoiceInput Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        if (HelperManager.Instance.startingTheGameAudio.activeSelf)
        {
            StartCoroutine(WaitBeforeFirstResponse());
        }
        
        
    }


    public void GetTheFirstAnswer(string response)
    {

        Dictionary<string, string> answers = new Dictionary<string, string>()
        {
            {"No", "close"},
            {"no", "close"},
            {"Yes", "continue"},
            {"yes", "continue"},
            {"Yeah",  "continue"},
            {"Play", "startGame" },
            {"lay", "startGame" },
            {"Home", "quitGame" },
            {"ome", "quitGame" },
            

        };
        
        string command = " ";

        response = response.ToLower();

        foreach(var a in answers)
        {
            if (response.Contains(a.Key))
            {
                command = a.Value;
                
                break;
            }
            
        }
        switch (command)
        {
            case "close":
                GameFollow.Instance.SetMode(1);
                break;
            case "continue":
                GameFollow.Instance.SetMode(0);
                voiceRules.SetActive(true);
                break;
            case "startGame":
                SceneManager.LoadScene(1);
                GameFollow.Instance.isGameRunning = true;
                GameFollow.Instance.ResumeGame();
                break;
            case "quitGame":
                GameFollow.Instance.ExitGame();
                break;
            
        }
        
    }

    public void GetCommandsMenu(string response)
    {
        string answerPart = " ";
        response.ToLower().Contains(answerPart);
        
    }
    public void DeactivateRules()
    {
        voiceRules.SetActive(false);
    }
    IEnumerator WaitBeforeFirstResponse()
    {
        yield return new WaitForSeconds(23.0f);
        VoiceMovement.Instance.StartRecording();
        
    }
   
}
