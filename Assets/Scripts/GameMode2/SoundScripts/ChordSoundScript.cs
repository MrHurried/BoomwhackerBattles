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

    /*
    bool fadingOut = false;
    bool fadingIn = false;
    IEnumerator waitToFadeOutChord()
    {
        while (fadingIn)
        {
            yield return null;
        }
        //FADE BG MUSIC
        float basevolume = audioSource.volume;

        //Debug.Log(fadeoutMagnitude / transitionTime);
        yield return new WaitForSeconds( twoBarDuration - transitionTime *2 ); // the 0.1F is just as a safe guard

        fadingOut = true;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= fadeMagnitude;
            yield return new WaitForSeconds(fadeMagnitude / transitionTime);
        }
        fadingOut = false;
    }

    IEnumerator fadeInChord()
    {

        while (!fadingOut)
        {
            yield return null;
        }

        //FADE BG MUSIC
        float basevolume = audioSource.volume;

        //Debug.Log(fadeoutMagnitude / transitionTime);

        fadingIn = true;

        while (audioSource.volume < 1)
        {
            audioSource.volume += fadeMagnitude;
            yield return new WaitForSeconds(fadeMagnitude / transitionTime);
        }

        fadingIn = false;
    }*/

    /*ivate IEnumerator playChosenChord()
    {
        for(; ; )
        {
            setNewCurrentChordClip();
            audioSource.clip = currentChordClip;
            audioSource.Play();
            yield return waitTwoBars;
            StartCoroutine(playChosenChord());
        }
    }*/

    private void setNewCurrentChordClip()
    {
        int minIndex = 0;
        int maxIndex = chordStorer.c_chords.Length-1;

        int randIndex = Random.Range(minIndex, maxIndex);

        currentChordClip = chordStorer.c_chords[randIndex];
    }
    
}