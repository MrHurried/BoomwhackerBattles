using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class NoteSoundScript : MonoBehaviour
{
    [SerializeField] NoteCarouselScript isaNoteCarouselScript;
    private NoteSoundsStorer noteSounds;

    public string currentNoteString = "";

    private AudioSource audioSource;

    public bool doCustomPiece;
    public List<string> customPieceString = new List<string>();

    public int lastPlayedNoteIndex;

    //these are sorted lowest to highest
    string[] playableNotes = new string[8] { "C6", "D6", "E6", "F6", "G6", "A6", "B6", "C7" };

    public string currentNoteName = "D6";

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        noteSounds = GetComponent<NoteSoundsStorer>();
        if (doCustomPiece)
        {
            customPieceString = new List<string> { "2", "0", "0", "0", "0", "0", "0", "0 ", "2", "0", "0", "0", "0", "0", "0", "0 " };
            RandomPieceGeneratorScript.generatedPiece = customPieceString;
        }

        lastPlayedNoteIndex = isaNoteCarouselScript.currentNoteIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (isaNoteCarouselScript.currentNoteIndex < 0) return;

        updateCurrentNotestring();


        if(!currentNoteString.Contains("r") && noteNeedsToPlay() && currentNoteString != "0")
        {
            playNextNote();
            Debug.Log("just called playNextNote()");
        }

        /*if(currentNoteString == "2" && noteNeedsToPlay())
        {
            //audioSource.PlayOneShot(noteSounds.n2_C6);
            playNextNote();
        }
        if(currentNoteString == "4" && noteNeedsToPlay())
        {
            playNextNote();
        }
        if (currentNoteString == "8" && noteNeedsToPlay())
        {
            audioSource.PlayOneShot(noteSounds.n8_C6);
        }
        if (currentNoteString == "16" && noteNeedsToPlay())
        {
            audioSource.PlayOneShot(noteSounds.n16_C6);
        }*/
    }

    public bool noteNeedsToPlay()
    {
        if(isaNoteCarouselScript.currentNoteIndex != lastPlayedNoteIndex)
        {
            lastPlayedNoteIndex = isaNoteCarouselScript.currentNoteIndex;
            return true;
        }
        else { return false; }
    }
    public void updateCurrentNotestring()
    {
        if (isaNoteCarouselScript.currentNoteIndex < 0) return;

        currentNoteString = RandomPieceGeneratorScript.generatedPiece[isaNoteCarouselScript.currentNoteIndex];
    }

    private void playNextNote()
    {
        chooseNextNote();

        string fullNoteName = "N" + currentNoteString.ToUpper() + "_" + currentNoteName;

        Debug.Log(fullNoteName);

        System.Reflection.FieldInfo field = noteSounds.GetType().GetField(fullNoteName);

        if (field != null)
        {
            // Get the value of the variable
            AudioClip noteClip = (AudioClip)field.GetValue(noteSounds);

            audioSource.PlayOneShot(noteClip);
        }
        else
        {
            Debug.Log("this clip doesn't exist :(");
        }
    }

    private void chooseNextNote()
    {
        int potentialIndex = -1;
        while (!(potentialIndex >= 0 && potentialIndex < playableNotes.Length))
        {
            //get the index of the currently played note
            int index = Array.IndexOf(playableNotes, currentNoteName);
            int indexIncrement = UnityEngine.Random.Range(-2, 2);
            potentialIndex = index + indexIncrement;
        }
        
        currentNoteName = playableNotes[potentialIndex];

    }

}

