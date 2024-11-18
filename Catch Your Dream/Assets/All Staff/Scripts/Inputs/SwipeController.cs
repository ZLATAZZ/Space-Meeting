using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SwipeController : MonoBehaviour
{
    [SerializeField] private float minDistance = .1f;
    [SerializeField] private float maxTime = 1f;



    private Vector2 startPos;
    private float startTime;

    private Vector2 endPos;
    private float endTime;

    private bool isJumpHigher;
    private float jumpInterval;

    private bool isSwiped = false;
    private bool isJumped = false;
    private bool isDown = false;
    private bool isRight = false;
    private bool isLeft = false;
    private bool isCentered = true;

    public static SwipeController Instance { get; private set; }

    #region Events for animation
    public delegate void SwipeDetect(Vector2 firstPos, Vector2 lastPos);
    public event SwipeDetect OnSwipeDetect;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (isSwiped)
        {
            MovePlayer.Instance.SwipePlayer(startPos, endPos);
        }

        if (isJumped)
        {
            float duration = isJumpHigher ? 15.0f : 0.6f;
            StartCoroutine(SetPlayerJumpToOff(duration));
        }

        if (isDown)
        {
            StartCoroutine(SetPlayerSitToOff());
        }

    }

    private void OnEnable()
    {
        PlayerController.Instance.OnStartTouch += SwipeStart;
        PlayerController.Instance.OnEndTouch += SwipeEnd;
        PlayerController.Instance.OnEnableDoubleClick += FlyStart;
    }



    private void OnDisable()
    {
        PlayerController.Instance.OnStartTouch -= SwipeStart;
        PlayerController.Instance.OnEndTouch -= SwipeEnd;
        PlayerController.Instance.OnEnableDoubleClick -= FlyStart;
    }

    private void FlyStart(Vector2 position)
    {
        if (SceneFlow.Instance.isWings)
        {
            SwipeUp(3.5f);
            isJumpHigher = true;
            ResetSwipeFlags();
        }

        Debug.Log("We Fly");
    }

    private void SwipeStart(Vector2 position, float time)
    {
        if (isJumpHigher) return;
        startPos = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        if (isJumpHigher) return;
        endPos = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        float deltaX = endPos.x - startPos.x;
        float deltaY = endPos.y - startPos.y;

        if (Vector3.Distance(startPos, endPos) <= minDistance && endTime - startTime <= maxTime)
        {

            OnSwipeDetect?.Invoke(startPos, endPos);

            if (deltaY > 0 && deltaY > .5f) // Swipe upwards
            {
                SwipeUp(2.0f);
            }
            else if (deltaY < 0 && deltaY < -.5f && isJumped == false) // Swipe down
            {
                SwipeDown();
            }

            // Swipe to the right
            else if (deltaX > .2f && isCentered)
            {
                SwipeRight();
            }
            // Swipe to the left
            else if (deltaX < -.2f && isCentered)
            {
                SwipeLeft();
            }

            else if (isRight || isLeft || deltaX < .5f || deltaX > -.2f)
            {

                SwipeCenter();
            }


        }
        else
        {
            isSwiped = false;
        }
    }

    private void ResetSwipeFlags()
    {
        isRight = false;
        isDown = false;
        isLeft = false;
        isSwiped = false;
        isCentered = false;
        isJumped = true;
    }

    protected void SwipeLeft()
    {
        isCentered = false;
        isLeft = true;
        endPos.x = -1.5f;
        isSwiped = true;
    }

    protected void SwipeRight()
    {
        isCentered = false;
        isRight = true;
        endPos.x = 1.5f;
        isSwiped = true;
    }

    protected void SwipeCenter()
    {
        isCentered = true;
        isRight = false;
        isLeft = false;

        endPos.x = 0.0f;
        isSwiped = true;
    }

    protected void SwipeUp(float height)
    {
        isJumped = true;
        endPos.y = height;
        endPos.x = MovePlayer.Instance.gameObject.transform.position.x;
    }

    protected void SwipeDown()
    {
        isDown = true;
        endPos.y = -1f;

        endPos.x = MovePlayer.Instance.gameObject.transform.position.x;
    }


    #region couroutine for how long player can stay in the jump state
    IEnumerator SetPlayerJumpToOff(float duration)
    {
        MovePlayer.Instance.JumpPlayer(endPos);
        yield return new WaitForSeconds(duration);
        isJumped = false;

        if (isJumpHigher)
        {
            isJumpHigher = false;
        }
    }


    IEnumerator SetPlayerSitToOff()
    {
        MovePlayer.Instance.SitPlayer(endPos);
        yield return new WaitForSeconds(.6f);
        isDown = false;
    }
    #endregion
}