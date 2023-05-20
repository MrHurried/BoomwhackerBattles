using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// THIS SCRIPT CONTAINS ALL SORTS OF USEFUL FUNCTIONS THAT HAVE TO DO WITH GAMEMODE 2'S NOTEBLOCKS
/// </summary>
public class NoteBlockFunctions : MonoBehaviour
{
    //NOTE SPRITES
    [SerializeField] public Sprite emptySprite;
    [SerializeField] Sprite note2sprite;
    [SerializeField] Sprite note4sprite;
    [SerializeField] Sprite note8sprite;
    [SerializeField] Sprite note16sprite;
    [SerializeField] Sprite rest2sprite;
    [SerializeField] Sprite rest4sprite;
    [SerializeField] Sprite rest8sprite;
    [SerializeField] Sprite rest16sprite;

    //SLIDER SPRITES
    [SerializeField] public Sprite noteSliderSprite;
    [SerializeField] public Sprite restSliderSprite;

    public NoteCarouselScript isaNoteCarouselScript;

    private bool skipReturn = false;
    private void Start()
    {
       isaNoteCarouselScript = GameObject.Find("NotenBovenIsaHolder").GetComponent<NoteCarouselScript>();
    }

    public Sprite getNoteSprite(int noteIndex)
    {
        string noteString = "0";
        //check if the noteindex isn't greater than the piece's length
        if (noteIndex <= RandomPieceGeneratorScript.generatedPiece.Count - 1)
        {
            noteString = RandomPieceGeneratorScript.generatedPiece[noteIndex];
        }
        Sprite sprite;

        //Set sprite var to right image, according to the current note (strCurrentNote)
        switch (noteString)
        {
            case "0":
                sprite = emptySprite;
                break;
            case "2":
                sprite = note2sprite;
                break;
            case "4":
                sprite = note4sprite;
                break;
            case "8":
                sprite = note8sprite;
                break;
            case "16":
                sprite = note16sprite;
                break;
            case "r2":
                sprite = rest2sprite;
                break;
            case "r4":
                sprite = rest4sprite;
                break;
            case "r8":
                sprite = rest8sprite;
                break;
            case "r16":
                sprite = rest16sprite;
                break;
            default:
                sprite = note2sprite;
                break;
        }

        return sprite;
    }

    public bool isThisIndexInsidePieceBounds(int index) 
    {
        bool answer = -1 < index && index < RandomPieceGeneratorScript.generatedPiece.Count-2;
        return answer;
    }

    public bool wasNoteBeforeHold(bool startFromNewestNote)
    {
        string strCurrentNote = "0";

        //if we need to start from the newest note, add 5
        int index = isaNoteCarouselScript.currentNoteIndex;
        if (startFromNewestNote) index = isaNoteCarouselScript.currentNoteIndex + 4;

        Debug.Log("inside the wasNoteBeforeHold method. index = " + index);

        while (strCurrentNote == "0")
        {           
            index--;
            /*if (index < 0)
            {
                Debug.LogError("Custom error: wasNoteBeforeHold, index is less than 0. This shouldn't happen. breaking from the loop now");
                skipReturn = true;
                break;
            }
            if (index >= RandomPieceGeneratorScript.generatedPiece.Count)
            {
                Debug.LogError("Custom error: NoteblockFunctions: wasNoteBeforeHold: the index is greater than the piece length. This shouldn't happen. Breaking the loop now.");
                skipReturn = true;
                break;
            }*/
            strCurrentNote = RandomPieceGeneratorScript.generatedPiece[index];
            //Debug.Log("inside the wasNoteBeforeHoldCheck: " + strCurrentNote);
        }

        if (strCurrentNote.Contains("r"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
