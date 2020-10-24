using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTimer : MonoBehaviour
{
    public GameManager gm;

    Text txtTimer;

    private void Awake()
    {
        txtTimer = GetComponent<Text>();
    }

    void Update()
    {
        txtTimer.text = gm.GetTimerInSeconds().ToString();
    }
}
