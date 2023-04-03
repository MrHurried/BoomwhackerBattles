using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordSoundsStorer : MonoBehaviour
{
    [Header("C Major:")]
    public AudioClip chord_C_1;
    public AudioClip chord_C_2;
    public AudioClip chord_C_3;
    public AudioClip chord_C_4;
    public AudioClip chord_C_5;
    public AudioClip chord_C_6;
    public AudioClip chord_C_7;
    public AudioClip chord_C_8;
    public AudioClip chord_C_9;
    public AudioClip chord_C_10;
    public AudioClip chord_C_11;
    public AudioClip chord_C_12;
    public AudioClip chord_C_13;
    public AudioClip chord_C_14;
    public AudioClip chord_C_15;


    public AudioClip[] c_chords;
    private void Awake()
    {
       c_chords = new AudioClip[] { chord_C_1, chord_C_2, chord_C_3, chord_C_4, chord_C_5, chord_C_6, chord_C_7, chord_C_8, chord_C_9, chord_C_10, chord_C_11, chord_C_12, chord_C_13, chord_C_14, chord_C_15 };
    }
}
