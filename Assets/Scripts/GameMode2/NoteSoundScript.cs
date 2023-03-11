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
        
    }

    // Update is called once per frame
    void Update()
    {
        

        updateCurrentNotestring();

        if(currentNoteString == "2" && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(noteSounds.n2_C6);
        }
        if(currentNoteString == "4" && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(noteSounds.n4_C6);
        }
        if (currentNoteString == "8" && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(noteSounds.n8_C6);
        }
        if (currentNoteString == "16" && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(noteSounds.n16_C6);
        }
    }

    public void updateCurrentNotestring()
    {
        if (isaNoteCarouselScript.currentNoteIndex < 0) return;

        currentNoteString = RandomPieceGeneratorScript.generatedPiece[isaNoteCarouselScript.currentNoteIndex];
    }

}
