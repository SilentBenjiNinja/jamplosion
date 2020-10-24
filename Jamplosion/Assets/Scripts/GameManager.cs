using System.Collections;
using System.Collections.Generic;
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

    public float timeLimit = 90f;
    public float timer;

    public int moduleAmount = 4;

    public bool[] modulesFinished;

    bool gameRunning = false;

    public ParticleSystem deathVfx;

    GameState currentState = GameState.StartMenu;

    private void Start()
    {
        GoToMenu();
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
        timer = timeLimit;
        gameRunning = true;
        currentState = GameState.InGame;
        SwitchToCamera((int)currentState);

        modulesFinished = new bool[moduleAmount];
    }

    void LoseGame()
    {
        gameRunning = false;
        currentState = GameState.LoseScreen;
        SwitchToCamera((int)currentState);
        deathVfx?.Play();
        // trigger explosion here
    }
    void WinGame()
    {
        gameRunning = false;
        currentState = GameState.WinScreen;
        SwitchToCamera((int)currentState);
        // trigger win screen here
    }
    void GoToMenu()
    {
        gameRunning = false;
        currentState = GameState.StartMenu;
        SwitchToCamera((int)currentState);
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
    public void SetModuleAmount(int _moduleAmount)
    {
        if (!gameRunning)
        {
            moduleAmount = _moduleAmount;
        }
    }
    public void SetTime(float _timeLimit)
    {
        if (!gameRunning)
        {
            timeLimit = _timeLimit;
        }
    }
    #endregion

    // for displaying timer
    public int getTimerInSeconds()
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
