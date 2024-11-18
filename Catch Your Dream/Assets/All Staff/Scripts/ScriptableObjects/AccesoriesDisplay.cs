using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoriesDisplay : AvatarDisplay
{
    [SerializeField] private Toggle alienToggle;
    [SerializeField] private Toggle cloakToggle;
    [SerializeField] private Toggle hatToggle;
    [SerializeField] private Toggle bootsToggle;
    [SerializeField] private Toggle shipToggle;
    [SerializeField] private Toggle wingsToggle;

    private void Start()
    {
        base.Start();
        alienToggle.onValueChanged.AddListener(delegate { ChangeToggleStatus(alienToggle); });
        cloakToggle.onValueChanged.AddListener(delegate { ChangeToggleStatus(cloakToggle); });
        hatToggle.onValueChanged.AddListener(delegate { ChangeToggleStatus(hatToggle); });
        bootsToggle.onValueChanged.AddListener(delegate { ChangeToggleStatus(bootsToggle); });
        shipToggle.onValueChanged.AddListener(delegate { ChangeToggleStatus(shipToggle); });
        wingsToggle.onValueChanged.AddListener(delegate { ChangeToggleStatus(wingsToggle); });

        SetToggleAutomatically();
        ChangeAccesorieStatus();
    }

    private void Update()
    {
        
        CheckAvatarCost();
        UpdateAvatarStatus(alienToggle, ref SceneFlow.Instance.isAlien);
        UpdateAvatarStatus(cloakToggle, ref SceneFlow.Instance.isCloak);
        UpdateAvatarStatus(hatToggle, ref SceneFlow.Instance.isHat);
        UpdateAvatarStatus(bootsToggle, ref SceneFlow.Instance.isBoots);
        UpdateAvatarStatus(shipToggle, ref SceneFlow.Instance.isShip);
        UpdateAvatarStatus(wingsToggle, ref SceneFlow.Instance.isWings);

        //Debug.Log($"Alien: {SceneFlow.Instance.isAlien} Cloak: {SceneFlow.Instance.isCloak} Hat: {SceneFlow.Instance.isHat} Boots: {SceneFlow.Instance.isBoots} Ship: {SceneFlow.Instance.isShip} Wings: {SceneFlow.Instance.isWings}");
    }

    protected override void MakeAvatarAvailable()
    {
        
        int starsPrice = avatar.cost;
        if (starsPrice == 0 || starsPrice <= SceneFlow.Instance.colledctedStars)
        {
            Debug.Log("Function works correctly");
            CalculateCurrentCapital(starsPrice);
            CheckWhichAvatarIsAvailable();
        }
    }

    protected override void CheckWhichAvatarIsAvailable()
    {
        if (avatar.isActivated)
        {
            switch (avatar.name)
            {
                case "Alien":
                    SceneFlow.Instance.isAlien = true;
                    break;
                case "Cloak":
                    SceneFlow.Instance.isCloak = true;
                    break;
                case "Hat":
                    SceneFlow.Instance.isHat = true;
                    break;
                case "Boots":
                    SceneFlow.Instance.isBoots = true;
                    break;
                case "Ship":
                    SceneFlow.Instance.isShip = true;
                    break;
                case "Wings":
                    SceneFlow.Instance.isWings = true;
                    break;
            }
            SceneFlow.Instance.SaveChoice();
        }
    }

    private void SetToggleAutomatically()
    {
        SceneFlow.Instance.LoadChoice();
        alienToggle.isOn = SceneFlow.Instance.isAlien;
        cloakToggle.isOn = SceneFlow.Instance.isCloak;
        hatToggle.isOn = SceneFlow.Instance.isHat;
        bootsToggle.isOn = SceneFlow.Instance.isBoots;
        shipToggle.isOn = SceneFlow.Instance.isShip;
        wingsToggle.isOn = SceneFlow.Instance.isWings;
    }

    private void ChangeToggleStatus(Toggle avatarToggle)
    {
        CheckWhichAvatarIsAvailable();

        SceneFlow.Instance.SaveChoice();
    }

    //ref initializing local variable for referenced variable
    private void UpdateAvatarStatus(Toggle avatarToggle, ref bool isAvatar)
    {
        isAvatar = avatarToggle.isOn;
    }

    protected override void CalculateCapital(int price)
    {
        SceneFlow.Instance.colledctedStars -= price;
        SceneFlow.Instance.SaveChoice();
    }

    protected override void CheckAvatarCost()
    {
        if (avatar.cost <= 0)
        {
            switch (avatar.name)
            {
                case "Alien":
                    SceneFlow.Instance.isAlienUnlock = true;
                    alienToggle.interactable = true;
                    SetStatusOfAvatar();
                    break;
                case "Cloak":
                    SceneFlow.Instance.isCloakUnlock = true;
                    cloakToggle.interactable = true;
                    SetStatusOfAvatar();
                    break;
                case "Hat":
                    SceneFlow.Instance.isHatUnlcok = true;
                    hatToggle.interactable = true;
                    SetStatusOfAvatar();
                    break;
                case "Boots":
                    SceneFlow.Instance.iSBootsUnlock = true;
                    bootsToggle.interactable = true;
                    SetStatusOfAvatar();
                    break;
                case "Ship":
                    SceneFlow.Instance.isShipUnlock = true;
                    shipToggle.interactable = true;
                    SetStatusOfAvatar();
                    break;
                case "Wings":
                    SceneFlow.Instance.isWingsUnlock = true;
                    wingsToggle.interactable = true;
                    SetStatusOfAvatar();
                    break;
            }
            SceneFlow.Instance.SaveChoice();
        }
    }

    private void ChangeAccesorieStatus()
    {
        switch (avatar.name)
        {
            case "Alien":
                 
                if (SceneFlow.Instance.isAlienUnlock)
                {
                    alienToggle.interactable = true;
                    SetStatusOfAvatar();
                }
                
                break;
            case "Cloak":
                
                if (SceneFlow.Instance.isCloakUnlock)
                {
                    alienToggle.interactable = true;
                    SetStatusOfAvatar();
                }
                break;
            case "Hat":
                 
                if (SceneFlow.Instance.isHatUnlcok)
                {
                    alienToggle.interactable = true;
                    SetStatusOfAvatar();
                }
                break;
            case "Boots":
                
                if (SceneFlow.Instance.iSBootsUnlock)
                {
                    alienToggle.interactable = true;
                    SetStatusOfAvatar();
                }
                break;
            case "Ship":
                 
                if (SceneFlow.Instance.isShipUnlock)
                {
                    alienToggle.interactable = true;
                    SetStatusOfAvatar();
                }
                break;
            case "Wings":
                if (SceneFlow.Instance.isWingsUnlock)
                {
                    alienToggle.interactable = true;
                    SetStatusOfAvatar();
                }
                break;
        }
    }

}
