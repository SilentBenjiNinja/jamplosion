﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    StartMenu,
    InGame,
    LoseScreen,
    WinScreen
}

// handles win and lose condition; X
// initializes and starts game; X
// handles difficulty settings; X
// handles timer; X
// navigation between menus; X

public class GameManager : MonoBehaviour
{
    #region Cameras

    public List<Camera> cameras;

    int currentCam = 0;

    void SwitchToCamera(int camIndex)
    {
        am.StopAllSounds();
        cameras[currentCam].enabled = false;
        cameras[camIndex].enabled = true;
        currentCam = camIndex;
    }

    #endregion

    float timer;

    public bool[] modulesFinished;

    [SerializeField] private GameObject[] modules;
    [SerializeField] private Transform[] slots;

    [SerializeField] private PickUpAndInspect pickUpAndInspect;

    public DisappearAfterThreeSecs dis;

    bool gameRunning = false;

    public ParticleSystem deathVfx;
    public AudioManager am;
    public CameraShake camShake;

    GameState currentState = GameState.StartMenu;

    private void Start()
    {
        GoToMenu();
        deathVfx?.Stop();
        //modulesFinished = new bool[moduleAmount];
        SpawnModules();
    }

    void Update()
    {
        if (gameRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                LoseGame();
            }
        }

        FetchTestInputs();
    }

    public void StartGame()
    {
        Debug.Log("start game");
        timer = timeLimit;
        gameRunning = true;
        currentState = GameState.InGame;
        SwitchToCamera((int)currentState);

        modulesFinished = new bool[moduleAmount];

        // despawn old modules
        DespawnModules();

        // spawn modules
        SpawnModules();
        MyLockDispatcher.LockSomePuzzles();

        am.tickingNoiseLoop.Play();
    }

    public void LoseGame()
    {
        Debug.Log("lose game");
        gameRunning = false;
        currentState = GameState.LoseScreen;
        SwitchToCamera((int)currentState);

        // trigger explosion here
        StartCoroutine(DelayedExplosion());
        //deathVfx?.Play();
    }

    void WinGame()
    {
        Debug.Log("win game");
        gameRunning = false;
        currentState = GameState.WinScreen;
        SwitchToCamera((int)currentState);
        dis.WinScreen();
        // trigger win screen here
    }

    public void GoToMenu()
    {
        Debug.Log("go to menu");
        gameRunning = false;
        currentState = GameState.StartMenu;
        SwitchToCamera((int)currentState);
        startMenu.SetActive(true);
        difficulty.SetActive(false);

        am.menuMusic.Play();
    }

    void SpawnModules()
    {
        if (null == modules || modules.Length < 1)
            return;

        for (int i = 0; i < moduleAmount; i++)
        {
            // choose a random module
            int randomModule = Random.Range(0, modules.Length);
            var module = modules[randomModule];

            // choose a slot
            var slot = slots[i];

            // set the modules rotation to the slots rotation

            GameObject tmp = Instantiate(module, slot);

            var moduleBase = tmp.GetComponent<ModuleBase>();

            if (null == moduleBase)
                continue;

            moduleBase.gameManager = this;
            moduleBase.slotIndex = i;
            moduleBase.camLock = pickUpAndInspect;
        }
    }

    void DespawnModules()
    {
        print("despawn");
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].childCount == 0)
                continue;

            var child = slots[i].GetChild(0).gameObject;
            Destroy(child);
        }
    }

    // called from module
    public void FinishModule(int moduleIndex)
    {
        if (modulesFinished[moduleIndex] == false)
        {
            modulesFinished[moduleIndex] = true;
            if (CheckWinCondition())
            {
                WinGame();
            }
        }
    }

    bool CheckWinCondition()
    {
        for (int i = 0; i < moduleAmount; i++)
        {
            if (modulesFinished[i] == false)
            {
                return false;
            }
        }

        return true;
    }

    private IEnumerator DelayedExplosion()
    {
        am.sirens.Play();

        yield return new WaitForSeconds(1);
        deathVfx?.Play();

        yield return new WaitForSeconds(0.5f);
        am.explosion.Play();
        StartCoroutine(camShake.Shake());

        yield return new WaitForSeconds(0.5f);
        am.carAlarm1.Play();
        yield return new WaitForSeconds(0.1f);
        am.carAlarm2.Play();
        yield return new WaitForSeconds(0.05f);
        am.carAlarm3.Play();
    }

    // use these to adjust game settings (in menu?)
    #region Game Settings

    public float timeLimit = 90f;
    public int moduleAmount = 4;

    public float maxTimeLimit = 240f;
    public float minTimeLimit = 15f;

    public int maxModuleAmount = 8;
    public int minModuleAmount = 1;

    public GameObject startMenu;
    public GameObject difficulty;

    public TextMeshProUGUI txtModules;
    public TextMeshProUGUI txtTime;

    public void SelectDifficulty()
    {
        startMenu.SetActive(false);
        difficulty.SetActive(true);
        UpdateModuleSelection();
    }

    public void RaiseTime()
    {
        if (timeLimit < maxTimeLimit)
        {
            timeLimit += 5f;
            UpdateTimeSelection();
        }
    }

    public void DecreaseTime()
    {
        if (timeLimit > minTimeLimit)
        {
            timeLimit -= 5f;
            UpdateTimeSelection();
        }
    }

    public void RaiseModules()
    {
        if (moduleAmount < maxModuleAmount)
        {
            moduleAmount += 1;
            UpdateModuleSelection();
        }
    }

    public void DecreaseModules()
    {
        if (moduleAmount > minModuleAmount)
        {
            moduleAmount -= 1;
            UpdateModuleSelection();
        }
    }

    void UpdateTimeSelection()
    {
        txtTime.text = Mathf.CeilToInt(timeLimit).ToString();
    }

    void UpdateModuleSelection()
    {
        txtModules.text = moduleAmount.ToString();
    }

    #endregion

    // for displaying timer
    public int GetTimerInSeconds()
    {
        return Mathf.CeilToInt(timer);
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = true;
        Debug.LogWarning("Pused the playmode | Application.Quit");
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
        //Application.Quit();
    }

    void FetchTestInputs()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GoToMenu();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            LoseGame();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            WinGame();
        }
    }
}