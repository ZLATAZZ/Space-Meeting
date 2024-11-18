using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class AnimationsStatesController : MonoBehaviour
{
    private Animator animator;

    //default Vectors, which are not used but they're just for methods
    private Vector2 first = Vector2.zero;
    private Vector2 second = Vector2.zero;
    public static AnimationsStatesController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        SwipeController.Instance.OnSwipeDetect += StartAnimations;
    }
    private void OnDisable()
    {
        SwipeController.Instance.OnSwipeDetect -= StartAnimations;
        SwipeController.Instance.OnSwipeDetect += UpAndDownSpace;
    }

    private void StartAnimations(Vector2 startPos, Vector2 endPos)
    {
        float deltaX = endPos.x - startPos.x;
        float deltaY = endPos.y - startPos.y;

        StartCoroutine(SetNormalState());

        if(deltaY > 0 && deltaY > 0.5f)
        {
            ToJump();
        }

        else if(deltaX > 0)
        {
            ToRight();
        }

        else
        {
            ToLeft();
        }
    }

    //Space Animations
    #region
    private void UpAndDownSpace(Vector2 startPos, Vector2 endPos)
    { 
        animator.SetBool("Jump", false);
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
    }
   
    private void ToJump()
    {
        animator.SetBool("Jump", true);
    }

    private void ToLeft()
    {
        animator.SetBool("Left", true);
        
    }

    private void ToRight()
    {
        animator.SetBool("Right", true);
        
    }
    #endregion
    IEnumerator SetNormalState()
    {
        yield return new WaitForSeconds(.2f);
        UpAndDownSpace(first,second);
    }
}
