using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class MovePlayer : MonoBehaviour
{
   

    public static MovePlayer Instance { get; private set; }

    private Vector3 velocity = Vector3.zero;
    private float primaryPosY;
   

    [Header("Control of Speed")]
    [SerializeField] private float speedOfSwipe;
    [SerializeField] private float smoothOfSwipe;

    private void Awake()
    {
       
        Instance = this;
    }

    private void Start()
    {
        primaryPosY = transform.position.y;
      
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    public void SwipePlayer(Vector3 startPos, Vector3 endPos)
    {
       
        Vector3 destination = new Vector3(endPos.x, transform.position.y, transform.position.z);
        
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothOfSwipe);
       
    }

    public void JumpPlayer(Vector3 endPos)
    {
       
        Vector3 destination = new Vector3(transform.position.x, endPos.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.0f);
       
    }

    public void SitPlayer(Vector3 endPos)

    {
        Vector3 destination = new Vector3(transform.position.x, endPos.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.0f);
        transform.rotation = Quaternion.Euler(35.0f, 0.0f, 0.0f);
    }

    
}