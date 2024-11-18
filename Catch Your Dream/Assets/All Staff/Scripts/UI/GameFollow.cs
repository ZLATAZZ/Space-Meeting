using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFollow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fact;
    [Header("Settings components")]
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderEffects;
    [SerializeField] AudioMixer audioMixer;

    [Header("LoadScene components")]
    [SerializeField] public int sceneIndexToLoad;

    [Header("Mode")]
    [SerializeField] private GameObject voiceMode;
    [SerializeField] private TMP_Dropdown dropMode;

    [Header("Shop components")]
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI stars;

    private bool[] modes = new bool[2];

    private int currentMoney;
    private int currentStars;

   [HideInInspector] public bool isGameRunning;
   [HideInInspector] public bool hasSettingForVoiceMode;

    public static GameFollow Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ApplySavedSettings();
        
    }

    private void Update()
    {
        voiceMode.gameObject.SetActive(SceneFlow.Instance.isVoiceModeOn);
    }

    public void StartAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameRunning = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isGameRunning = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGameRunning = true;
    }

    public void DestroyAlien()
    {
        fact.text = " ";
        isGameRunning = true;
    }

    public void ExitGame()
    {
        SceneFlow.Instance.SaveChoice();
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndexToLoad);
        SceneFlow.Instance.LoadChoice();
        isGameRunning = true;
    }

    public void SetMusicVolume(float volume)
    {
        SceneFlow.Instance.volumeMusic = volume;
        sliderMusic.value = volume;
        audioMixer.SetFloat("MainMusic", SceneFlow.Instance.volumeMusic);
    }

    public void SetEffectsVolume(float volume)
    {
        SceneFlow.Instance.volumeEffects = volume;
        sliderEffects.value = volume;
        audioMixer.SetFloat("Effects", SceneFlow.Instance.volumeEffects);
    }

    public void SetMode(int modeIndex)
    {
        SceneFlow.Instance.voiceModeIndex = modeIndex;
        switch (SceneFlow.Instance.voiceModeIndex)
        {
            case 0:
                modes[0] = true;
                hasSettingForVoiceMode = true;
                isGameRunning = true;
                SceneFlow.Instance.isVoiceBtnOn = true;
                break;
            case 1:
                modes[1] = false;
                hasSettingForVoiceMode = false;
                isGameRunning = true;
                SceneFlow.Instance.isVoiceBtnOn = false;
                
                break;
        }
        SceneFlow.Instance.SaveChoice();
        if(GetTheVoiceInput.Instance.voiceDetectBtn != null)
        {
            GetTheVoiceInput.Instance.voiceDetectBtn.gameObject.SetActive(SceneFlow.Instance.isVoiceBtnOn);
        }
        bool isVoiceMode = modes[modeIndex];
        SceneFlow.Instance.isVoiceModeOn = isVoiceMode;
        SceneFlow.Instance.SaveChoice(); 
        dropMode.value = SceneFlow.Instance.voiceModeIndex;
        voiceMode.SetActive(SceneFlow.Instance.isVoiceModeOn);
        
    }

    public void DisplayMoneyStarsAmount()
    {
        SceneFlow.Instance.LoadChoice();
        currentMoney = SceneFlow.Instance.collectedMoney;
        currentStars = SceneFlow.Instance.colledctedStars;

        money.text = "Money: " + currentMoney.ToString();
        stars.text = "Stars: " + currentStars.ToString();
    }

    public void ApplySavedSettings()
    {
        SetMusicVolume(SceneFlow.Instance.volumeMusic);
        SetEffectsVolume(SceneFlow.Instance.volumeEffects);
        SetMode(SceneFlow.Instance.voiceModeIndex);
        

    }
}
