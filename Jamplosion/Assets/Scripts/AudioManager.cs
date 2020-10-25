using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// play ticking noise while playing
// menu sounds
// explosion sound when losing (+sirens & car alarms)
// menu music
// swoosh when selecting module
// button clicks when editing module
// force field sound when selecting non-available module
public class AudioManager : MonoBehaviour
{
    public AudioSource menuMusic;

    public AudioSource menuSounds1;
    public AudioSource menuSounds2;
    public AudioSource menuSounds3;
    public AudioSource menuSounds4;

    public AudioSource tickingNoiseLoop;
    public AudioSource moduleSelectionSwoosh;
    //public AudioSource moduleButtonClicks;
    //public AudioSource urgentAlarm;

    public AudioSource explosion;
    public AudioSource carAlarm1;
    public AudioSource carAlarm2;
    public AudioSource carAlarm3;
    public AudioSource sirens;

    public void StopAllSounds()
    {
        menuMusic?.Stop();

        menuSounds1?.Stop();
        menuSounds2?.Stop();
        menuSounds3?.Stop();
        menuSounds4?.Stop();

        tickingNoiseLoop?.Stop();
        moduleSelectionSwoosh?.Stop();
        //moduleButtonClicks?.Stop();
        //urgentAlarm?.Stop();

        explosion?.Stop();
        carAlarm1?.Stop();
        carAlarm2?.Stop();
        carAlarm3?.Stop();
        sirens?.Stop();
    }
}
