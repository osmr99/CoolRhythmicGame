#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NoteHit : MonoBehaviour
{
    MouseNote theNote;
    BGM bgm;

    // Start is called before the first frame update
    private void OnEnable()
    {
        theNote = GetComponentInParent<MouseNote>();
        bgm = FindObjectOfType<BGM>();
    }

    public void HitboxCollision(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerDot")
        {
            theNote.SuccessNoteHit(bgm.selectedLevel);
        }
    }
}
