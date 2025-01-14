#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseNote : MonoBehaviour
{
    SpriteRenderer myNote;
    NoteHit hitBox;
    float randomX;
    float randomY;
    BGM bgm;
    Health playerHealth;

    // Start is called before the first frame update
    void OnEnable()
    {
        myNote = GetComponent<SpriteRenderer>();
        hitBox = GetComponentInChildren<NoteHit>();
        bgm = FindObjectOfType<BGM>();
        playerHealth = FindObjectOfType<Health>();
        DoMouseNote(bgm.selectedLevel);
    }

    void Update()
    {
        if (playerHealth.currentHPBar.fillAmount == 0 || Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Destroy(gameObject);
        }
    }

    void DoMouseNote(int hardLevel)
    {
        StopAllCoroutines();
        StartCoroutine(NoteAnim(hardLevel));
    }

    IEnumerator NoteAnim(int hardLevel)
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
        myNote.DOColor(Color.cyan, 0);
        yield return new WaitForSeconds(0.5f);

        // Missed note
        if (hardLevel == 0)
        {
            playerHealth.NoteMiss(4);
        }
        else if (hardLevel == 1)
        {
            playerHealth.NoteMiss(1);
        }
        hitBox.HitboxCollision(false);
        myNote.DOColor(Color.red, 0);
        myNote.DOFade(0, 0.2f);
        StartCoroutine(DestroyMe());
    }

    public void SuccessNoteHit(int hardLevel)
    {
        StopAllCoroutines();
        if (hardLevel == 0 && playerHealth.timer > playerHealth.iSeconds)
        {
            if (playerHealth.currentHPBar.fillAmount != playerHealth.maxHPBar.fillAmount)
            {
                playerHealth.NoteHit(8);

            }

            if(playerHealth.currentHPBar.fillAmount >= playerHealth.maxHPBar.fillAmount)
            {
                playerHealth.timer = 0;
            }
        }
        else if (hardLevel == 1 && playerHealth.timer > playerHealth.iSeconds)
        {
            if (playerHealth.currentHPBar.fillAmount != playerHealth.maxHPBar.fillAmount)
            {
                playerHealth.NoteHit(4);

            }

            if (playerHealth.currentHPBar.fillAmount >= playerHealth.maxHPBar.fillAmount)
            {
                playerHealth.timer = 0;
            }
        }
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
