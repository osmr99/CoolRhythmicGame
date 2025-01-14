#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class Health : MonoBehaviour
{
    [SerializeField] public Image currentHPBar;
    [SerializeField] public Image maxHPBar;
    [SerializeField] public TMP_Text healthText;
    BGM bgm;
    public float timer;
    public float iSeconds;
    public float healthDrain;
    public bool drain;
    float randomX;
    float randomY;
    [SerializeField] float shakingPower;

    // Start is called before the first frame update
    void Start()
    {
        bgm = FindObjectOfType<BGM>();
        currentHPBar.fillAmount = 1;
        maxHPBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(drain)
        {
            ChangeHealthText();
            timer += Time.deltaTime;

            if (timer > iSeconds)
            {
                if(currentHPBar.fillAmount >= 0.2f)
                {
                    currentHPBar.fillAmount -= (healthDrain / 100f) * Time.deltaTime;
                }
                else
                {
                    ShakingAnim();
                }
            }

            if(currentHPBar.fillAmount == 0 || Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Death();
            }
        }

        if (currentHPBar.fillAmount >= maxHPBar.fillAmount)
        {
            currentHPBar.fillAmount = maxHPBar.fillAmount;
        }
    }

    public void NoteHit(float change)
    {
        currentHPBar.fillAmount += change / 100f;
    }

    public void NoteMiss(float change)
    {
        currentHPBar.fillAmount -= (change * 2) / 100f;
        maxHPBar.fillAmount -= change / 100f;
    }

    public void ChangeHealthText()
    {
        healthText.text = (currentHPBar.fillAmount * 100).ToString("F0") + "% / " + (maxHPBar.fillAmount * 100).ToString("F0") + "%";
    }

    public void ResetBars()
    {
        currentHPBar.fillAmount = 1;
        maxHPBar.fillAmount = 1;
        healthText.text = (currentHPBar.fillAmount * 100).ToString("F0") + "% / " + (maxHPBar.fillAmount * 100).ToString("F0") + "%";
    }

    void ShakingAnim()
    {
        randomX = Random.Range(0 + shakingPower, 0 - shakingPower);
        randomY = Random.Range(0 + shakingPower, 0 - shakingPower);
        gameObject.transform.localPosition = new Vector2(randomX, randomY);
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        //ResetBars();
        //bgm.playerDot.gameObject.SetActive(true);
        //drain = false;
        //bgm.music.Stop();
        //bgm.music.time = 0;
        //bgm.menu.ToggleMainMenu(true);
    }
}
