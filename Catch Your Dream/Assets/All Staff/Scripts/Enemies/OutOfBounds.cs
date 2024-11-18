using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private float bounds = -2.6f;


    private void Update()
    {
        if(transform.position.z < bounds)
        {
           gameObject.SetActive(false);

        }

    }

   
}
