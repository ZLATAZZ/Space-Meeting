using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    
    private float repeatPoint;
    private Vector3 startPos;


    [SerializeField] private float cofficient;

    private void Start()
    {
        startPos = transform.position;
        repeatPoint = GetComponent<BoxCollider>().size.z/cofficient;
       
    }

   
    private void Update()
    {
        
        if (transform.position.z < startPos.z - repeatPoint) {

          
            transform.position = startPos;
        }
    }

}
