using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public enum Constraint { Landscape, Portrait }

    #region FIELDS
    public Color wireColor = Color.white;
    public float UnitsSize = 1; // size of your scene in unity units
    public Constraint constraint = Constraint.Portrait;
    public static CameraResolution Instance;
    public new Camera camera;

    public bool executeInUpdate;

    private float _width;
    private float _height;
    //*** bottom screen
    private Vector3 _bl;
    private Vector3 _bc;
    private Vector3 _br;
    //*** middle screen
    private Vector3 _ml;
    private Vector3 _mc;
    private Vector3 _mr;
    //*** top screen
    private Vector3 _tl;
    private Vector3 _tc;
    private Vector3 _tr;
    #endregion

    #region PROPERTIES
    public float Width
    {
        get
        {
            return _width;
        }
    }
    public float Height
    {
        get
        {
            return _height;
        }
    }

    // helper points:
    public Vector3 BottomLeft
    {
        get
        {
            return _bl;
        }
    }
    public Vector3 BottomCenter
    {
        get
        {
            return _bc;
        }
    }
    public Vector3 BottomRight
    {
        get
        {
            return _br;
        }
    }
    public Vector3 MiddleLeft
    {
        get
        {
            return _ml;
        }
    }
    public Vector3 MiddleCenter
    {
        get
        {
            return _mc;
        }
    }
    public Vector3 MiddleRight
    {
        get
        {
            return _mr;
        }
    }
    public Vector3 TopLeft
    {
        get
        {
            return _tl;
        }
    }
    public Vector3 TopCenter
    {
        get
        {
            return _tc;
        }
    }
    public Vector3 TopRight
    {
        get
        {
            return _tr;
        }
    }
    #endregion

  
    private void Awake()
    {
        camera = GetComponent<Camera>();
        Instance = this;
        ComputeResolution();
    }

    private void ComputeResolution()
    {
        float leftX, rightX, topY, bottomY;

        if (constraint == Constraint.Landscape)
        {
            camera.orthographicSize = 1f / camera.aspect * UnitsSize / 2f;
        }
        else
        {
            camera.orthographicSize = UnitsSize / 2f;
        }

        _height = 2f * camera.orthographicSize;
        _width = _height * camera.aspect;

        float cameraX, cameraY;
        cameraX = camera.transform.position.x;
        cameraY = camera.transform.position.y;

        leftX = cameraX - _width / 2;
        rightX = cameraX + _width / 2;
        topY = cameraY + _height / 2;
        bottomY = cameraY - _height / 2;

        //*** bottom
        _bl = new Vector3(leftX, bottomY, 0);
        _bc = new Vector3(cameraX, bottomY, 0);
        _br = new Vector3(rightX, bottomY, 0);
        //*** middle
        _ml = new Vector3(leftX, cameraY, 0);
        _mc = new Vector3(cameraX, cameraY, 0);
        _mr = new Vector3(rightX, cameraY, 0);
        //*** top
        _tl = new Vector3(leftX, topY, 0);
        _tc = new Vector3(cameraX, topY, 0);
        _tr = new Vector3(rightX, topY, 0);
    }

    private void Update()
    {
        if (executeInUpdate)
        {
            ComputeResolution();
        }
    }

}
