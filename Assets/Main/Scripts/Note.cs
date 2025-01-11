#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Note : MonoBehaviour
{
    SpriteRenderer myNote;
    NoteHit hitBox;
    float randomX;
    float randomY;

    // Start is called before the first frame update
    void OnEnable()
    {
        myNote = GetComponent<SpriteRenderer>();
        hitBox = GetComponentInChildren<NoteHit>();
        DoNote();
    }

    void DoNote()
    {
        StopAllCoroutines();
        StartCoroutine(NoteAnim());
    }

    IEnumerator NoteAnim()
    {
        // Note reset;
        hitBox.HitboxCollision(false);
        myNote.DOColor(Color.gray, 0);
        myNote.DOFade(0, 0);
        myNote.transform.localScale = new Vector3(2, 2, 2);

        // Random location
        //randomX = Random.Range(-7f, 7f); Originally
        //randomY = Random.Range(-4f, 4f); Originally
        randomX = Random.Range(-4f, 4f);
        randomY = Random.Range(-2f, 2f);
        myNote.transform.position = new Vector2(randomX, randomY);

        // First appereance
        myNote.DOFade(0.5f, 1);
        yield return new WaitForSeconds(1);

        // Shrinking and changing color
        myNote.DOColor(Color.yellow, 1.5f);
        myNote.DOFade(0.5f, 1.5f);
        myNote.transform.DOScale(1, 1.5f);
        yield return new WaitForSeconds(1.5f);


        // Ready to hit the note and time frame
        hitBox.HitboxCollision(true);
        yield return new WaitForSeconds(0.5f);

        // Missed note
        hitBox.HitboxCollision(false);
        myNote.DOColor(Color.red, 0);
        myNote.DOFade(0, 0.2f);
        StartCoroutine(DestroyMe());
    }

    public void SuccessNoteHit()
    {
        StopAllCoroutines();
        hitBox.HitboxCollision(false);
        myNote.transform.DOScale(1.25f, 0);
        myNote.transform.DOScale(1f, 0.2f);
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
