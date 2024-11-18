using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CollisionBehaviour : MonoBehaviour
{
    private int moneyToAdd;
    private void Start()
    {
        if (SceneFlow.Instance.isBoots)
        {
            moneyToAdd = 4;
        }
        else
        {
            moneyToAdd = 1;
        }
    }
    private void OnEnable()
    {
        if (CollisionManager.Instance == null)
        {
            Debug.LogError("CollisionController.Instance is null!");
            return;
        }
        CollisionManager.Instance.OnCollided += CollideAndBehave;
    }

    private void OnDisable()
    {
        CollisionManager.Instance.OnCollided -= CollideAndBehave;
    }

    private void CollideAndBehave(GameObject currentGameObject)
    {
       

        switch (currentGameObject.tag)
        {

            case "GoldCoin":
                CountSystem.Instance.SetScore(moneyToAdd);
                break;
            case "Obstacle":
                CountSystem.Instance.GameOver();
                break;
            case "SmallAlien":
                ManageAliensFacts(0);
               
                break;
            case "TallAlien":
                ManageAliensFacts(1);
                break;
            case "FatAlien":
                ManageAliensFacts(2);
                break;
            case "UnderGround":
                Debug.Log("We fall");
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(player.transform.position.x, -0.2f, player.transform.position.z).normalized;
                break;



        }
    }


    private void ManageAliensFacts(int index)
    {
        Time.timeScale = 0;
        QuestsManager.Instance.ActivateFact(index);
        GameFollow.Instance.PauseGame();
        Destroy(SpawnManager.Instance.alienMain);
    }
}
