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
    [SerializeField] NumsArray numsArray;
    [SerializeField] MusicList musList;
    [SerializeField] AudioSource music;
    [SerializeField] NoteSpawner noteSpawner;

    void OnAwake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioClip clip in musList.musicList)
        {
            music.clip = clip;
            music.volume = 0;
            music.Play();
            music.Stop();
        }
        music.clip = musList.musicList[0];
        numsArray.time = music.time;
        numsArray.lastPlayedIndex = -1;
        numsArray.time = 0;
        numsArray.alternateTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        numsArray.time = music.time;
        numsArray.alternateTime = numsArray.time - 2.5f;
        if (Input.GetKeyDown(KeyCode.A))
        {
            music.volume = 0.25f;
            music.Play();
        }

        if (music.time >= 67)
            numsArray.lastPlayedIndex = -1;
        if (numsArray.lastPlayedIndex != 62  ) // End of the song
            NoteSpawnTiming();

        if (Input.GetKeyDown(KeyCode.D) ||Input.GetKeyDown(KeyCode.F)  || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K)) /*|| Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))*/ // Manual Beat set
        {
            numsArray.nums[numsArray.lastPlayedIndex + 1] = numsArray.time;
            numsArray.alternateNums[numsArray.lastPlayedIndex + 1] = numsArray.alternateTime;
            numsArray.lastPlayedIndex++;
        }
    }

    void NoteSpawnTiming()
    {
        //Debug.Log("waiting");
        if (numsArray.time >= numsArray.alternateNums[numsArray.lastPlayedIndex + 1] && numsArray.time < numsArray.alternateNums[numsArray.lastPlayedIndex + 2])
        {
            Debug.Log("spawned note index: " + numsArray.lastPlayedIndex);
            noteSpawner.SpawnNote();
            numsArray.lastPlayedIndex++;
        }
    }
}
