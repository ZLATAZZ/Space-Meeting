using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AvatarDisplay : MonoBehaviour
{
    [Header("Change accordingly to each avatar")]
    public Avatar avatar;

    [Header("Common for all avatars")]
    [SerializeField] private Image imageAvatar;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Button btnAvatar;
    [SerializeField] private Image lockImage;
    [SerializeField] private Toggle astonautToggle;
    [SerializeField] private Toggle catToggle;
    [SerializeField] private Toggle squirrelToggle;


    protected void Start()
    {
        // Ensure we load choices before setting the cost
        SceneFlow.Instance.LoadChoice();

        

        // Set the cost and display it
        imageAvatar.sprite = avatar.avatarImage;
        cost.text = "Price: " + avatar.cost.ToString();
        btnAvatar.onClick.AddListener(MakeAvatarAvailable);

        avatar.isActivated = false;
        
        SetAvatarStatus();
        ChangeToggleStatus();
    }

    private void Update()
    {
        CheckAvatarCost();
    }

    protected virtual void MakeAvatarAvailable()
    {
        int updatedPrice = avatar.cost;
        if (updatedPrice == 0 || updatedPrice <= SceneFlow.Instance.collectedMoney)
        {
            CalculateCurrentCapital(updatedPrice);
        }
        CheckWhichAvatarIsAvailable();
        
    }

    protected virtual void CheckWhichAvatarIsAvailable()
    {
        if (avatar.isActivated)
        {
            switch (avatar.name)
            {
                case "Austrounaut":
                    SceneFlow.Instance.isAstronaut = true;
                    SceneFlow.Instance.isCat = false;
                    SceneFlow.Instance.isSquirrel = false;
                    
                    break;
                case "Cat":
                    SceneFlow.Instance.isCat = true;
                    SceneFlow.Instance.isAstronaut = false;
                    SceneFlow.Instance.isSquirrel = false;
                    
                    break;
                case "Squirrel":
                    SceneFlow.Instance.isSquirrel = true;
                    SceneFlow.Instance.isCat = false;
                    SceneFlow.Instance.isAstronaut = false;
                    
                    break;
            }
            ChangeToggleStatus();
            SceneFlow.Instance.SaveChoice();
        }
    }

    protected void CalculateCurrentCapital(int price)
    {
        avatar.isActivated = true;
        CalculateCapital(price);

        SceneFlow.Instance.LoadChoice();
        GameFollow.Instance.DisplayMoneyStarsAmount();

        cost.text = " ";
        avatar.cost = 0;

        lockImage.gameObject.SetActive(false);

        SceneFlow.Instance.SaveChoice();
    }

   

    protected virtual void CalculateCapital(int price)
    {
        SceneFlow.Instance.collectedMoney -= price;
        SceneFlow.Instance.SaveChoice();
    }

    protected virtual void ChangeToggleStatus()
    {
        astonautToggle.isOn = SceneFlow.Instance.isAstronaut;
        catToggle.isOn = SceneFlow.Instance.isCat;
        squirrelToggle.isOn = SceneFlow.Instance.isSquirrel;
        
    }

    protected virtual void CheckAvatarCost()
    {
        if(avatar.cost <= 0)
        {
            switch (avatar.name)
            {
                case "Austrounaut":
                    SceneFlow.Instance.isAustronautUnlock = true;

                    break;
                case "Cat":
                    SceneFlow.Instance.isCatUnlock = true;

                    break;
                case "Squirrel":
                    SceneFlow.Instance.isSquirrelUnlock = true;
                    break;
            }
            SceneFlow.Instance.SaveChoice();
        }
    }

    protected virtual void SetAvatarStatus()
    {
        switch (avatar.name)
        {
            case "Austrounaut":

                if(SceneFlow.Instance.isAustronautUnlock)
                {
                    SetStatusOfAvatar();
                }

                break;
            case "Cat":
                if (SceneFlow.Instance.isCatUnlock)
                {
                    SetStatusOfAvatar();
                }

                break;
            case "Squirrel":
                if (SceneFlow.Instance.isSquirrelUnlock)
                {
                    SetStatusOfAvatar();
                }
                break;
        }
    }

    protected void SetStatusOfAvatar()
    {
        avatar.cost = 0;
        cost.text = " ";
        lockImage.gameObject.SetActive(false);
    }
    
}