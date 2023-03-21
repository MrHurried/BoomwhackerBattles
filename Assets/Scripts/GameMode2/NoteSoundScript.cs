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

        if(currentNoteString == "2" && noteNeedsToPlay())
        {
            audioSource.PlayOneShot(noteSounds.n2_C6);
        }
        if(currentNoteString == "4" && noteNeedsToPlay())
        {
            audioSource.PlayOneShot(noteSounds.n4_C6);
        }
        if (currentNoteString == "8" && noteNeedsToPlay())
        {
            audioSource.PlayOneShot(noteSounds.n8_C6);
        }
        if (currentNoteString == "16" && noteNeedsToPlay())
        {
            audioSource.PlayOneShot(noteSounds.n16_C6);
        }
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

        // Get the type of the class containing the variable
        Type type = typeof(NoteSoundsStorer);

        // Get the field based on the input string
        FieldInfo field = type.GetField("N2_" + currentNoteName);

        // Get the value of the field
        object fieldValue = field.GetValue(null);

        Debug.Log(fieldValue);
    }

    private void chooseNextNote()
    {
        //get the index of the currently played note
        int index = Array.IndexOf(playableNotes, currentNoteName);
        int indexIncrement = UnityEngine.Random.Range(-2, 2);
        int potentialIndex = index + indexIncrement;
        if(potentialIndex >= 0 && potentialIndex < playableNotes.Length)
        {
            currentNoteName = playableNotes[potentialIndex];
        }
    }

}

