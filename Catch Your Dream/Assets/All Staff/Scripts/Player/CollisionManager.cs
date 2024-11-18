using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class CollisionManager : MonoBehaviour
{
    public static CollisionManager Instance { get; private set; }

    [Header("All audios")]
    [SerializeField] private AudioSource collectCoins;
    [SerializeField] private AudioSource hitObstacles;

    #region
    public delegate void CollisionDetect(GameObject currentGameObject);
    public event CollisionDetect OnCollided;

    #endregion


    private void Awake()
    {
        Instance = this;

    }

    private void OnCollisionEnter(Collision collision)
    {

        OnCollided?.Invoke(collision.gameObject);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hitObstacles.Play();
        }
        

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GoldCoin"))
        {

            other.gameObject.SetActive(false);

            StartCoroutine(SetObjectActive(other.gameObject, 1f));
            collectCoins.Play();
        }
        OnCollided?.Invoke(other.gameObject);
    }


    private IEnumerator SetObjectActive(GameObject coin, float delay)
    {
        yield return new WaitForSeconds(delay);

       
        if (coin != null)
        {
            coin.SetActive(true);
        }
      
    }
}


