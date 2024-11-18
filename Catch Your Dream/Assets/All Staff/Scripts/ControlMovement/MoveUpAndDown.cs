using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public float speed = 1.0f; // Adjust the speed of the movement
    public float height = 1.2f; // Adjust the height of the movement


    private void Update()
    {
        LiftObject();
    }

    private void LiftObject()
    {
        float newY = Mathf.PingPong(Time.time * speed, height)/(-1.0f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, newY, gameObject.transform.position.z);
    }
}
