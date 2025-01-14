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
    [SerializeField] public AudioSource music;
    [SerializeField] NoteSpawner noteSpawner;
    [SerializeField] float delay;
    Health playerHealth;
    public Player playerDot;
    public MainMenu menu;
    public bool readyToPlay = false;
    public int selectedLevel = -1;

    void OnAwake()
    {
        //SyncNoteSpawning(delay);
    }

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<MainMenu>();
        playerDot = FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<Health>();
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
            hardLevel.time = 0;
            hardLevel.alternateTime = 0;
            hardLevel.lastPlayedIndex = -1;
            music.clip = musList.musicList[1];
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

        //hardLevel.time = music.time;
        //hardLevel.alternateTime = tutoLevel.time - delay;

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
            else if(currentLevel.lastPlayedIndex != 388 && selectedLevel == 1) // End of the song
            {
                KeyboardOnlyNoteSpawnTiming();
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
                playerDot.gameObject.SetActive(true);
                selectedLevel = -1;
                StartCoroutine(HardLevelEndDelay());
            }
        }

            

        //if (Input.GetKeyDown(KeyCode.D) ||Input.GetKeyDown(KeyCode.F)  || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K)) /*|| Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))*/ // Manual Beat set
        //{
            //hardLevel.nums[hardLevel.lastPlayedIndex + 1] = hardLevel.time;
            //hardLevel.alternateNums[hardLevel.lastPlayedIndex + 1] = hardLevel.alternateTime;
            //hardLevel.lastPlayedIndex++;
        //}
    }

    IEnumerator TutoLevelEndDelay()
    {
        playerHealth.drain = false;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        //playerHealth.ResetBars();
        //menu.ToggleMainMenu(true);
    }

    IEnumerator HardLevelEndDelay()
    {
        playerHealth.drain = false;
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        //playerHealth.ResetBars();
        //menu.ToggleMainMenu(true);
    }

    void NoteSpawnTiming()
    {
        //Debug.Log("waiting");
        if(currentLevel.lastPlayedIndex < currentLevel.nums.Length)
        {
            if (currentLevel.time >= currentLevel.alternateNums[currentLevel.lastPlayedIndex + 1] && currentLevel.time < currentLevel.alternateNums[currentLevel.lastPlayedIndex + 2])
            {
                Debug.Log("spawned note index: " + currentLevel.lastPlayedIndex);
                noteSpawner.SpawnNote(false);
                currentLevel.lastPlayedIndex++;
            }
        }
    }

    void KeyboardOnlyNoteSpawnTiming()
    {
        //Debug.Log("waiting");
        if (currentLevel.lastPlayedIndex < currentLevel.nums.Length)
        {
            if (currentLevel.time >= currentLevel.alternateNums[currentLevel.lastPlayedIndex + 1] && currentLevel.time < currentLevel.alternateNums[currentLevel.lastPlayedIndex + 2])
            {
                Debug.Log("spawned note index: " + currentLevel.lastPlayedIndex);
                noteSpawner.SpawnNote(true);
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
            SyncNoteSpawning(delay);
        }
        else if (level == 1)
        {
            playerDot.gameObject.SetActive(false);
            ScriptableObjectSelect(hardLevel);
            SyncNoteSpawning(delay);
        }
        StartCoroutine(PlayLevel(level));
    }

    IEnumerator PlayLevel(int level)
    {
        yield return new WaitForSeconds(0.5f);
        if (level == 0)
        {
            music.clip = musList.musicList[0];
            music.volume = 0.25f;
            music.Play();
            playerHealth.healthDrain = currentLevel.healthDrainAmount;
            playerHealth.drain = true;
        }
        else if (level == 1)
        {
            music.clip = musList.musicList[1];
            music.volume = 0.25f;
            music.Play();
            playerHealth.healthDrain = currentLevel.healthDrainAmount;
            playerHealth.drain = true;
        }
    }

    void ScriptableObjectSelect(NumsArray musicData)
    {
        currentLevel.time = 0;
        currentLevel.alternateTime = 0;
        currentLevel.lastPlayedIndex = -1;
        currentLevel.nums = musicData.nums;
        currentLevel.healthDrainAmount = musicData.healthDrainAmount;
    }
}
