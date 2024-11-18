using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SceneFlow : MonoBehaviour
{
    public static SceneFlow Instance { get; private set; }

    [HideInInspector] public float volumeMusic;
    [HideInInspector] public float volumeEffects;

    [HideInInspector] public bool isVoiceModeOn;
    [HideInInspector] public int voiceModeIndex;

    [HideInInspector] public int collectedMoney;
    [HideInInspector] public int colledctedStars;

    #region booleans for Avatar Store
    [HideInInspector] public bool isAstronaut;
    [HideInInspector] public bool isCat;
    [HideInInspector] public bool isSquirrel;

    [HideInInspector] public bool isAlien;
    [HideInInspector] public bool isCloak;
    [HideInInspector] public bool isHat;
    [HideInInspector] public bool isBoots;
    [HideInInspector] public bool isShip;
    [HideInInspector] public bool isWings;
    #endregion

    [HideInInspector] public bool lockStatus;

    #region booleans for deactivating lock image, and save the unlocked character
    [HideInInspector] public bool isCatUnlock;
    [HideInInspector] public bool isSquirrelUnlock;
    [HideInInspector] public bool isAustronautUnlock;

    [HideInInspector] public bool isAlienUnlock;
    [HideInInspector] public bool isCloakUnlock;
    [HideInInspector] public bool isHatUnlcok;
    [HideInInspector] public bool iSBootsUnlock;
    [HideInInspector] public bool isShipUnlock;
    [HideInInspector] public bool isWingsUnlock;
    #endregion

    [HideInInspector] public int indexOfLastQuestion;

    [HideInInspector] public bool isVoiceBtnOn;

    [HideInInspector] public int launchIndex;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadChoice();
        }
    }

    private void Start()
    {
        LoadChoice();
        GameFollow.Instance.ApplySavedSettings();
        
    }

    [System.Serializable]
    public class SaveData
    {
        public float volumeMusic;
        public float volumeEffects;

        public bool isVoiceModeOn;
        public int voiceModeIndex;

        // Capital
        public int collectedMoney;
        public int colledctedStars;

        // Characters
        public bool isAstronaut;
        public bool isCat;
        public bool isSquirrel;

        // Accessories for characters
        public bool isAlien;
        public bool isCloak;
        public bool isHat;
        public bool isBoots;
        public bool isShip;
        public bool isWings;

        //Additional control variables;
        public bool lockStatus;

        public bool isCatUnlock;
        public bool isSquirrelUnlock;
        public bool isAustronautUnlock;

        public bool isAlienUnlock;
        public bool isCloakUnlock;
        public bool isHatUnlock;
        public bool isBootsUnlock;
        public bool isShipUnlock;
        public bool isWingsUnlock;

        public int indexOfLastQuestion;

        public bool isVoiceBtnOn;

        public bool isFirstLaunch = true;

        public int launchIndex;
    }

    public void SaveChoice()
    {
        SaveData saveData = new SaveData();

        saveData.volumeMusic = volumeMusic;
        saveData.volumeEffects = volumeEffects;

        saveData.isVoiceModeOn = isVoiceModeOn;
        saveData.voiceModeIndex = voiceModeIndex;

        saveData.collectedMoney = collectedMoney;
        saveData.colledctedStars = colledctedStars;

        saveData.isAstronaut = isAstronaut;
        saveData.isCat = isCat;
        saveData.isSquirrel = isSquirrel;

        saveData.isAlien = isAlien;
        saveData.isCloak = isCloak;
        saveData.isHat = isHat;
        saveData.isBoots = isBoots;
        saveData.isShip = isShip;
        saveData.isWings = isWings;

        saveData.lockStatus = lockStatus;

        saveData.isCatUnlock = isCatUnlock;
        saveData.isSquirrelUnlock = isSquirrelUnlock;
        saveData.isAustronautUnlock = isAustronautUnlock;
        
        saveData.isAlienUnlock = isAlienUnlock;
        saveData.isCloakUnlock = isCloakUnlock;
        saveData.isHatUnlock = isHatUnlcok;
        saveData.isBootsUnlock = iSBootsUnlock;
        saveData.isShipUnlock = isShipUnlock;
        saveData.isWingsUnlock = isWingsUnlock;

        saveData.indexOfLastQuestion = indexOfLastQuestion;

        saveData.isVoiceBtnOn = isVoiceBtnOn;

        saveData.isFirstLaunch = false;

        saveData.launchIndex = launchIndex;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);

        
    }

    public void LoadChoice()
    {
        string path = Application.persistentDataPath + "/savedata.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            volumeMusic = saveData.volumeMusic;
            volumeEffects = saveData.volumeEffects;

            if (saveData.isFirstLaunch)
            {
                isVoiceModeOn = true;
                voiceModeIndex = 0;
                
                saveData.isFirstLaunch = false;
                
                SaveChoice(); // Save the changes
            }
            else
            {
                isVoiceModeOn = saveData.isVoiceModeOn;
                voiceModeIndex = saveData.voiceModeIndex;
                
            }

            collectedMoney = saveData.collectedMoney;
            colledctedStars = saveData.colledctedStars;

            isAstronaut = saveData.isAstronaut;
            isCat = saveData.isCat;
            isSquirrel = saveData.isSquirrel;

            isAlien = saveData.isAlien;
            isCloak = saveData.isCloak;
            isHat = saveData.isHat;
            isBoots = saveData.isBoots;
            isShip = saveData.isShip;
            isWings = saveData.isWings;

            lockStatus = saveData.lockStatus;

            isCatUnlock = saveData.isCatUnlock;
            isSquirrelUnlock = saveData.isSquirrelUnlock;
            isAustronautUnlock = saveData.isAustronautUnlock;
            
            isAlienUnlock = saveData.isAlienUnlock;
            isCloakUnlock = saveData.isCloakUnlock;
            isHatUnlcok = saveData.isHatUnlock;
            iSBootsUnlock = saveData.isBootsUnlock;
            isShipUnlock = saveData.isShipUnlock;
            isWingsUnlock = saveData.isWingsUnlock;

            indexOfLastQuestion = saveData.indexOfLastQuestion;

            isVoiceBtnOn = saveData.isVoiceBtnOn;

            launchIndex = saveData.launchIndex;
           
            //Debug.Log($"Astronaut: {isAstronaut}, cat: {isCat}, squirrel: {isSquirrel}");
        }
    }
}
