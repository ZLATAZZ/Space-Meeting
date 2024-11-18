using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CountSystem : MonoBehaviour
{
    public static CountSystem Instance {  get; private set; }

    public TextMeshProUGUI money;
    [Header("UI Components")]
    [SerializeField] private Button gameOver;

    [HideInInspector] public bool isOver;
    
    private GameObject player;

    private int currentAmount;
    


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        money.text = "Money: ";
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void SetScore(int moneyAmount)
    {
        currentAmount += moneyAmount;
        SceneFlow.Instance.collectedMoney += moneyAmount;
        
        money.text = "Money: " + currentAmount.ToString();
        SceneFlow.Instance.SaveChoice();
    }

   

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        player.SetActive(false);
        GameFollow.Instance.PauseGame();
        isOver = true;
    }

}
