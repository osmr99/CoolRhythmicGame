#pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class BGM : MonoBehaviour
{
    [SerializeField] NumsArray tutoLevel;
    [SerializeField] NumsArray hardLevel;
    [SerializeField] NumsArray currentLevel;
    [SerializeField] MusicList musList;
    [SerializeField] AudioSource music;
    [SerializeField] NoteSpawner noteSpawner;
    [SerializeField] float delay;
    MainMenu menu;
    public bool readyToPlay = false;
    int selectedLevel = -1;

    void OnAwake()
    {
        SyncNoteSpawning(delay);
    }

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<MainMenu>();
        foreach (AudioClip clip in musList.musicList)
        {
            music.clip = clip;
            music.volume = 0;
            music.Play();
            music.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.zKey.wasPressedThisFrame)
        {
            menu.ToggleMainMenu(false);
            tutoLevel.time = 0;
            tutoLevel.alternateTime = 0;
            tutoLevel.lastPlayedIndex = -1;
            music.clip = musList.musicList[0];
            music.volume = 0.25f;
            music.Play();
        }

        if(Keyboard.current.xKey.wasPressedThisFrame)
        {
            music.Stop();
            menu.ToggleMainMenu(true);
        }

        currentLevel.time = music.time;
        currentLevel.alternateTime = currentLevel.time - delay;

        //tutoLevel.time = music.time;
        //tutoLevel.alternateTime = tutoLevel.time - delay;

        if (music.time >= 67 && selectedLevel == 0)
        {
            currentLevel.lastPlayedIndex = -1;
        } 
        else if (music.time >= 164 && selectedLevel == 1)
        {
            currentLevel.lastPlayedIndex = -1;
        }

        if(readyToPlay)
        {
            if (currentLevel.lastPlayedIndex != 52 && selectedLevel == 0) // End of the song
            {
                NoteSpawnTiming();
            }
            else if(currentLevel.lastPlayedIndex != 999 && selectedLevel == 1) // WIP
            {
                NoteSpawnTiming();
            }
            else
            {
                readyToPlay = false;
            }
        }
        else
        {
            if (selectedLevel == 0)
            {
                selectedLevel = -1;
                StartCoroutine(TutoLevelEndDelay());
            }
            else if(selectedLevel == 1)
            {
                selectedLevel = -1;
                StartCoroutine(HardLevelEndDelay());
            }
        }

            

        //if (Input.GetKeyDown(KeyCode.D) ||Input.GetKeyDown(KeyCode.F)  || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K)) /*|| Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))*/ // Manual Beat set
        //{
            //tutoLevel.nums[tutoLevel.lastPlayedIndex + 1] = tutoLevel.time;
            //tutoLevel.alternateNums[tutoLevel.lastPlayedIndex + 1] = tutoLevel.alternateTime;
            //tutoLevel.lastPlayedIndex++;
        //}
    }

    IEnumerator TutoLevelEndDelay()
    {
        yield return new WaitForSeconds(4);
        menu.ToggleMainMenu(true);
    }

    IEnumerator HardLevelEndDelay()
    {
        yield return new WaitForSeconds(0); // WIP
        menu.ToggleMainMenu(true);
    }

    void NoteSpawnTiming()
    {
        //Debug.Log("waiting");
        if(currentLevel.lastPlayedIndex < currentLevel.nums.Length)
        {
            if (currentLevel.time >= currentLevel.alternateNums[currentLevel.lastPlayedIndex + 1] && currentLevel.time < currentLevel.alternateNums[currentLevel.lastPlayedIndex + 2])
            {
                Debug.Log("spawned note index: " + currentLevel.lastPlayedIndex);
                noteSpawner.SpawnNote();
                currentLevel.lastPlayedIndex++;
            }
        }

    }

    void SyncNoteSpawning(float delay)
    {
        currentLevel.alternateNums = new float[currentLevel.nums.Length];
        for(int i = 0; i < currentLevel.alternateNums.Length; i++)
        {
            currentLevel.alternateNums[i] = currentLevel.nums[i] - delay;
        }
    }

    public void StartLevel(int level)
    {
        readyToPlay = true;
        selectedLevel = level;
        if (level == 0)
        {
            ScriptableObjectSelect(tutoLevel);
        }
        else if (level == 1)
        {
            ScriptableObjectSelect(hardLevel);
        }
        StartCoroutine(PlayLevel(level));
    }

    IEnumerator PlayLevel(int level)
    {
        yield return new WaitForSeconds(1.5f);
        if (level == 0)
        {
            music.clip = musList.musicList[0];
            music.volume = 0.25f;
            music.Play();
        }
        else if (level == 1)
        {
            music.clip = musList.musicList[1];
            music.volume = 0.25f;
            music.Play();
        }
    }

    void ScriptableObjectSelect(NumsArray musicData)
    {
        currentLevel.time = 0;
        currentLevel.alternateTime = 0;
        currentLevel.lastPlayedIndex = -1;
        currentLevel.nums = musicData.nums;
        currentLevel.alternateNums = musicData.alternateNums;
    }
}
