using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BgResolution : MonoBehaviour
{

    private MeshRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<MeshRenderer>();

        //get the give  world screensize - height (in units)
        float screenHeightWorld = Camera.main.orthographicSize * 2;

        Debug.Log("Multiplied by two: " + screenHeightWorld);

       
        //calculate width in units
        float screenWidthWorld = screenHeightWorld / Screen.height * Screen.width;

        Debug.Log($"Without two {screenWidthWorld}");

        transform.localScale = new Vector3(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y, 1);

    }

   
}
