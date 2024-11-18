using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AvatarDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject austronautPref;
    [SerializeField] private GameObject catPref;
    [SerializeField] private GameObject squirrelPref;

    [SerializeField] private GameObject alienPref;
    [SerializeField] private GameObject helmetPref;
    [SerializeField] private GameObject wingsPref;
    [SerializeField] private GameObject bootsPref;
    [SerializeField] private GameObject shipPref;
    [SerializeField] private GameObject cloakPref;

    
    private void Start()
    {
        SceneFlow.Instance.LoadChoice();
        

        InitializeActions();
        
    }
   
    private void ActivateAccesories(bool isActiveAccesorie, GameObject accesorie)
    {
        if (isActiveAccesorie)
        {
            accesorie.SetActive(true);
            
        }
        else
        {
            
            accesorie.SetActive(false);
        }
    }

    private void SetAvatarToActive(bool isCurrentPlayer, GameObject currentPlayer, GameObject subPlayer1, GameObject subPlayer2)
    {
        if(isCurrentPlayer)
        {
            currentPlayer.SetActive(true);
            subPlayer1.SetActive(false);
            subPlayer2.SetActive(false);
            Debug.Log($"{currentPlayer.name} is set to active");
        }
        
        
    }

    public void InitializeActions()
    {
        SetAvatarToActive(SceneFlow.Instance.isAstronaut, austronautPref, catPref, squirrelPref);
        SetAvatarToActive(SceneFlow.Instance.isCat, catPref, austronautPref, squirrelPref);
        SetAvatarToActive(SceneFlow.Instance.isSquirrel, squirrelPref, catPref, austronautPref);

        ActivateAccesories(SceneFlow.Instance.isAlien, alienPref);
        ActivateAccesories(SceneFlow.Instance.isHat, helmetPref);
        ActivateAccesories(SceneFlow.Instance.isWings, wingsPref);
        ActivateAccesories(SceneFlow.Instance.isBoots, bootsPref);
        ActivateAccesories(SceneFlow.Instance.isShip, shipPref);
        ActivateAccesories(SceneFlow.Instance.isCloak, cloakPref);
    }
}

