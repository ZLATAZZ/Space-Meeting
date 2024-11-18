using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertTouchPosition : MonoBehaviour
{

    public static Vector3 ConverPosToWorld (Camera camera, Vector3 position)
    {
        
        position.z = 2.39f;
        return camera.ScreenToWorldPoint(position);
    }
}
