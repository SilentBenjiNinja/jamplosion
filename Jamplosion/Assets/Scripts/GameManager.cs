using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
// navigation between menus;

public class GameManager : MonoBehaviour
{
    #region Cameras
    public List<Camera> cameras;

    int currentCam = 0;

    void SwitchToCamera(int camIndex)
    {
        cameras[currentCam].enabled = false;
        cameras[camIndex].enabled = true;
        currentCam = camIndex;
    }
    #endregion

    float timer;

    public bool[] modulesFinished;

    [SerializeField] private GameObject[] modules;

    bool gameRunning = false;

    public ParticleSystem deathVfx;

    GameState currentState = GameState.StartMenu;

    private void Start()
    {
        GoToMenu();
        deathVfx?.Pause();
        //modulesFinished = new bool[moduleAmount];
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
        // spawn modules
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
    }

    void SpawnModules()
    {
        for (int i = 0; i < moduleAmount; i++)
        {
            // choose a random module
            // choose a slot
            // set the modules rotation to the slots rotation
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

    private IEnumerator DelayedExplosion()
    {
        yield return new WaitForSeconds(1);

        deathVfx?.Play();
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
        Application.Quit();
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
