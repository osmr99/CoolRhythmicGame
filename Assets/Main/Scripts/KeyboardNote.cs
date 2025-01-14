#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class KeyboardNote : MonoBehaviour
{
    TMP_Text myNote;
    float randomX;
    float randomY;
    int rng = 0;
    bool canHit = false;
    void OnEnable()
    {
        myNote = GetComponent<TMP_Text>();
        myNote.text = string.Empty;
        DoKeyboardNote();
    }

    // Update is called once per frame
    void Update()
    {
        if(canHit)
        {
            if(rng == 1)
            {
                if(Keyboard.current.wKey.wasPressedThisFrame)
                {
                    SuccessNoteHit();
                }
            }
            else if (rng == 2)
            {
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    SuccessNoteHit();
                }
            }
            else if (rng == 3)
            {
                if (Keyboard.current.sKey.wasPressedThisFrame)
                {
                    SuccessNoteHit();
                }
            }
            else if (rng == 4)
            {
                if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    SuccessNoteHit();
                }
            }
        }
    }

    void DoKeyboardNote()
    {
        StartCoroutine(NoteAnim());
    }

    IEnumerator NoteAnim()
    {
        // Chooses a random key
        rng = Random.Range(1, 5);
        switch (rng)
        {
            case 1:
                myNote.text = "W";
                break;
            case 2:
                myNote.text = "A";
                break;
            case 3:
                myNote.text = "S";
                break;
            case 4:
                myNote.text = "D";
                break;
        }
        // Random Location
        randomX = Random.Range(-7f, 7f);
        randomY = Random.Range(-4f, 4f);
        myNote.transform.position = new Vector2(randomX, randomY);

        // First appereance
        myNote.DOFade(0.5f, 1);
        yield return new WaitForSeconds(1);

        // Shrinking and changing color
        myNote.DOColor(Color.yellow, 1.25f);
        myNote.DOFade(0.5f, 1.5f);
        myNote.transform.DOScale(0.25f, 1.25f);
        yield return new WaitForSeconds(1.25f);

        // Ready to hit the note and time frame
        canHit = true;
        myNote.DOColor(Color.cyan, 0);
        yield return new WaitForSeconds(0.75f);

        // Missed note
        canHit = false;
        myNote.DOColor(Color.red, 0);
        myNote.DOFade(0, 0.2f);
        StartCoroutine(DestroyMe());
    }

    public void SuccessNoteHit()
    {
        StopAllCoroutines();
        canHit = false;
        myNote.transform.DOScale(0.35f, 0);
        myNote.transform.DOScale(0.25f, 0.2f);
        myNote.DOColor(Color.green, 0);
        myNote.DOFade(0, 0.2f);
        StartCoroutine(DestroyMe());
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
