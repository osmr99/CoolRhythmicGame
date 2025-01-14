#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    BGM bgm;

    // Start is called before the first frame update
    void Start()
    {
        bgm = FindObjectOfType<BGM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMainMenu(bool toggle)
    {
        canvas.gameObject.SetActive(toggle);
    }

    public void ClickedTuto()
    {
        ToggleMainMenu(false);
        bgm.StartLevel(0);
    }

    public void ClickedHard()
    {
        ToggleMainMenu(false);
        bgm.StartLevel(1);
    }
}