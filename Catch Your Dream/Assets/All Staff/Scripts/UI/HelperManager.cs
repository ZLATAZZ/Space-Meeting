using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HelperManager : MonoBehaviour
{
    public static HelperManager Instance { get; private set; }
    public GameObject startingTheGameAudio;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SceneFlow.Instance.LoadChoice();
        if(SceneFlow.Instance.launchIndex == 0) 
        {
            startingTheGameAudio.SetActive(true);
            StartCoroutine(OpenGreeting());
        }
        else
        {
            startingTheGameAudio.SetActive(false);
        }
    }

    

    IEnumerator OpenGreeting()
    {
        yield return new WaitForSeconds(24.0f);
        SceneFlow.Instance.launchIndex = 1;
        SceneFlow.Instance.SaveChoice();
        
    }

}
