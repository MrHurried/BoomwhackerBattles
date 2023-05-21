using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class ChordSoundScript : MonoBehaviour
{
    private ChordSoundsStorer chordStorer;
    public NoteCarouselScript isaNoteCarouselScript;
    public GM2WinnerScript winnerScript;

    public float twoBarDuration = 8f; // measured in seconds$

    public AudioClip currentChordClip;
    public AudioSource audioSource;

    // Start is called before the first frame update


    WaitForSeconds waitTwoBars;

    private void Awake()
    {
        waitTwoBars = new WaitForSeconds(twoBarDuration);
    }

    void Start()
    {
        if (this.enabled)
        {
            winnerScript.bgMusicAudioSrc = audioSource;
        }

        chordStorer = GetComponent<ChordSoundsStorer>(); 
    }


    /*public float transitionTime = 0.01f;

    public float fadeMagnitude = .01f;*/
    float time = 8f;

    private void Update()
    {
        time += Time.deltaTime;

        bool eightSecondsHaveElapsed = time >= 8f;

        if(eightSecondsHaveElapsed && isaNoteCarouselScript.currentNoteIndex > -1)
        {
            setNewCurrentChordClip();
            audioSource.PlayOneShot(currentChordClip);
            time = 0f;
        }

    }

    private void setNewCurrentChordClip()
    {
        int minIndex = 0;
        int maxIndex = chordStorer.c_chords.Length-1;

        int randIndex = Random.Range(minIndex, maxIndex);

        currentChordClip = chordStorer.c_chords[randIndex];
    }
    
}