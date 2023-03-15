using System.Collections;
using System.Collections.Generic;
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

}
