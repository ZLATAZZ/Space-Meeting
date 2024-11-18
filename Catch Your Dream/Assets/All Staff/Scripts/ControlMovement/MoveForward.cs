using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speedOfObject;
    private float increasedSpeed;
   
    
    private void Update()
    {
        if (GameFollow.Instance.isGameRunning)
        {
            MoveObjectForward();
        }
        
    }
    private void FixedUpdate()
    {
        if(GameFollow.Instance.hasSettingForVoiceMode == false) {

            increasedSpeed += .001f;
            

            if (increasedSpeed > 0.03f)
            {
                increasedSpeed = 0.03f;
                
            }
        }
        else
        {
            transform.Translate(Vector3.back * speedOfObject * Time.deltaTime);
            
        }

    }
    private void MoveObjectForward()
    {
       
        ChangeSpeed();
    }

    private void ChangeSpeed()
    {
        if(SceneFlow.Instance.isCloak)
        {
            transform.Translate(Vector3.back * speedOfObject * Time.deltaTime);
            
        }
        else
        {
            transform.Translate(Vector3.back * speedOfObject * increasedSpeed);
        }
    }
}
