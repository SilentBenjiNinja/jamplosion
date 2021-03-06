﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTimer : MonoBehaviour
{
    public GameManager gm;

    TextMeshProUGUI txtTimer;

    private void Awake()
    {
        txtTimer = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        txtTimer.text = GetTimerString();
    }

    string GetTimerString()
    {
        int timerInSeconds = gm.GetTimerInSeconds();
        int seconds = timerInSeconds % 60;
        string secondsStr = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        int minutes = Mathf.FloorToInt(timerInSeconds / 60);
        string minutesStr = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        return $"{minutesStr}:{secondsStr}";
    }
}
