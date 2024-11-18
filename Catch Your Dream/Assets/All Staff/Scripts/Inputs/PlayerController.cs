using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    #region
    //set up delegates and events, which can be used later directly, without referencing
    public delegate void StartTouch(Vector2 pos, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 pos, float time);
    public event EndTouch OnEndTouch;

    public delegate void EnableDoubleClick(Vector2 pos);
    public event EnableDoubleClick OnEnableDoubleClick;
    //end of the delegates and events
    #endregion


    private MainInputActions inputActions;

    private Camera camera;

    //crate singleton, to access this class freely
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        inputActions = new MainInputActions();
        camera = Camera.main;
        Instance = this;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        //we subscribed to the new input actions, ctx - context, where is the whole information, what this input is doing
        inputActions.Touch.PrimaryContact.started += ctx => StartPosition(ctx);
        inputActions.Touch.PrimaryContact.canceled += ctx => EndPosition(ctx);

        inputActions.Touch.Fly.started += ctx => StartPosition(ctx);
        inputActions.Touch.Fly.performed += ctx => EndDoubleClick(ctx);
    }

    private void EndDoubleClick(InputAction.CallbackContext ctx)
    {

        if(OnEnableDoubleClick != null)
        {
            float playerPosFly = inputActions.Touch.Fly.ReadValue<float>();
            OnEnableDoubleClick(ConvertTouchPosition.ConverPosToWorld(camera, new Vector3(playerPosFly, playerPosFly, playerPosFly)));
        }
    }

    //if you will have any problems regarding those methods, then just pass the InputAction.CallbackContext context into methods
    private void EndPosition(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {

            //set up the first, primary position, when we start swiping
            Vector3 playerPos = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();
            OnEndTouch(ConvertTouchPosition.ConverPosToWorld(camera, playerPos), (float)context.startTime);

        }
    }

    private void StartPosition(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) {

            //set up the first, primary position, when we start swiping
            Vector3 playerPos = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();
            //just convert the postion usign method, you could use it even directly, without creating additional scripts
            OnStartTouch(ConvertTouchPosition.ConverPosToWorld(camera, playerPos), (float)context.startTime);

        }
    }

    //is not used for now
    public Vector2 CurrentPosition()
    {
        Vector3 playerPos = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();
        playerPos.y = 0f;
        return ConvertTouchPosition.ConverPosToWorld(camera, playerPos);
    }

}
