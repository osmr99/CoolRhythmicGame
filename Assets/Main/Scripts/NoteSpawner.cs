#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject mouseNote;
    [SerializeField] GameObject keyboardNote;
    int rng = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //Instantiate(note);
        //}
    }

    public void SpawnNote(bool keyboardOnly)
    {
        if(!keyboardOnly)
        {
            rng = Random.Range(0, 101); //0, 101
            if (rng > 50)
            {
                Instantiate(mouseNote);
            }
            else
            {
                Instantiate(keyboardNote);
            }
        }
        else
        {
            Instantiate(keyboardNote);
        }


    }
}
