#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] int targetFPS;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

    void Update()
    {
        FollowMousePosition();
        //if (Input.GetKeyDown(KeyCode.LeftAlt))
            //Debug.Log("x: " + (int)Input.mousePosition.x + " y: " + (int)Input.mousePosition.y);
        //if (Input.GetKeyDown(KeyCode.Space))
            //Debug.Log("x: " + transform.position.x + " y: " + transform.position.y);
    }

    void FollowMousePosition()
    {
        //transform.position = GetWorldPositionFromMouse();
        transform.position = new Vector2(GetMouseX(), GetMouseY());
    }

    float GetMouseX()
    {
        if(Input.mousePosition.x < 50) //Left
        {
            return -8.42f;
        }
        else if (Input.mousePosition.x > 1870) //Right
        {
            return 8.42f;
        }
        else
        {
            return cam.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }

    float GetMouseY()
    {
        if (Input.mousePosition.y < 50) //Bottom
        {
            return -4.53f;
        }
        else if (Input.mousePosition.y > 1029) //Top
        {
            return 4.53f;
        }
        else
        {
            return cam.ScreenToWorldPoint(Input.mousePosition).y;
        }
    }

    //Vector2 GetWorldPositionFromMouse()
    //{
        //return cam.ScreenToWorldPoint(Input.mousePosition);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
    }
}
