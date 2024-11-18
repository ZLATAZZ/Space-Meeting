using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        RotateInThirtyFive();
    }

    private void RotateInThirtyFive()
    {
        float value = Mathf.Sin(rotationSpeed * Time.time) * 35.0f;
        transform.rotation = Quaternion.Euler(0.0f, value, 0.0f);
        
    }
}
