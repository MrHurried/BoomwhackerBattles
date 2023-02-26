using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoomWhackerBattles;

public class NoteBlock
{
    public GameObject go; // this will be the name of the game object
    public int parentIndex; //eg. "nb0" -> that is the name of the GameObject underneath NoteblockHolder
    public int spawnIndex; //eg. 0 -> that is the left mask spawn index
    public string note;
    public bool fromIsa;

    NoteCarouselScript isaNoteCarouselScript;
    NoteCarouselScript matNoteCarouselScript;
    NoteBlockFunctions noteBlockFunctions;

    public NoteBlock(int ParentIndex, bool FromIsa)
    {
        isaNoteCarouselScript = GameObject.Find("NotenBovenIsaHolder").GetComponent<NoteCarouselScript>();
        matNoteCarouselScript = GameObject.Find("NotenBovenMatHolder").GetComponent<NoteCarouselScript>();
        noteBlockFunctions = GameObject.Find("GameManager").GetComponent<NoteBlockFunctions>();

        this.parentIndex = ParentIndex;
        this.fromIsa = FromIsa;
        if (FromIsa)
        {
            this.spawnIndex = 110 * (5 - ParentIndex); // 110 cus there are 110.0f units between each NB core. if ParentIndex = 0 spawnindex will also be zero. 
            /*Debug.Log("New NoteBlock created. info:"
            + "\n parentIndex = " + ParentIndex
            + "\n spawnIndex = " + spawnIndex
            + "\n fromIsa = " + fromIsa
            + "\n global position = " + SavedPossibleSpawns.possibleSpawnsIsa[spawnIndex]);*/
        }

        else
        {
            this.spawnIndex = 110 * (5 - ParentIndex); // 110 cus there are 110.0f units between each NB core. if ParentIndex = 0 spawnindex will also be zero. 
            /*Debug.Log("New NoteBlock created"
                + "\n parentIndex = " + ParentIndex
                + "\n spawnIndex = " + spawnIndex
                + "\n fromIsa = " + fromIsa
                + "\n global position = " + SavedPossibleSpawns.possibleSpawnsMat[spawnIndex]);*/
        }

        if (FromIsa)
        {
            GameObject nbholder = GameObject.Find("NotenBovenIsaHolder");
            go = nbholder.transform.GetChild(0).GetChild(parentIndex).gameObject; // first getChild is GO named "NoteBlocks", which holds nb0, nb1, etc.
        }
        else
        {
            GameObject nbholder = GameObject.Find("NotenBovenMatHolder");
            go = nbholder.transform.GetChild(0).GetChild(parentIndex).gameObject; // first getChild is GO named "NoteBlocks", which holds nb0, nb1, etc.
        }
    }

    public void setNextNote(bool doStarterNote)
    {
        Transform noteSpriteHolder = go.transform.GetChild(0);
        //Debug.Log("notespritehodler name = " + noteSpriteHolder.name);
        SpriteRenderer sr = noteSpriteHolder.GetComponent<SpriteRenderer>();

        int newestNoteIndex = isaNoteCarouselScript.currentNoteIndex + 4; //i believe this doesn't have to have a "mat" equivalent

        if (doStarterNote && this.parentIndex == 5)
        {
            newestNoteIndex = 0;
            Sprite sprite = noteBlockFunctions.getNoteSprite(newestNoteIndex);
            
            /*Debug.Log("Doing starternote sprite setting. fromIsa? " + this.fromIsa +
                "\n parentindex? mine is: " + parentIndex +
                "\n my GO's name? that's : " + go.name +
                "\n newestnoteindex = " + newestNoteIndex
                + "\n fromIsa ? " + fromIsa + ". this nb will get this sprite: " + sprite+
                "\n the character for newestNoteIndex = " + RandomPieceGeneratorScript.generatedPiece[newestNoteIndex]);*/

            sr.sprite = sprite;
            return;
        }

        if (newestNoteIndex >= 0)
        {
            Sprite sprite = noteBlockFunctions.getNoteSprite(newestNoteIndex);

            sr.sprite = sprite;

            /*Debug.Log("newest note : " + noteBlockFunctions.getNoteSprite(newestNoteIndex) +
                "\n newest note index: " + newestNoteIndex +
                "\n current note index = " + isaNoteCarouselScript.currentNoteIndex);*/
        }
    }
    public void setSlider()
    {
        Transform noteSliderHolder = go.transform.GetChild(1);
        SpriteRenderer sr = noteSliderHolder.GetComponent<SpriteRenderer>();
        Sprite slider;

        int newestNoteIndex = isaNoteCarouselScript.currentNoteIndex + 4; //i believe this doesn't have to have a "mat" equivalent

        if (RandomPieceGeneratorScript.generatedPiece[newestNoteIndex] != "0")
        {
            sr.sprite = noteBlockFunctions.emptySprite;
            return;
        }

        if (noteBlockFunctions.wasNoteBeforeHold(true))
            slider = noteBlockFunctions.noteSliderSprite;
        else
            slider = noteBlockFunctions.restSliderSprite;

        sr.sprite = slider;
    }

    public void advancePosition()
    {
        if (fromIsa)
        {
            if (spawnIndex < SavedPossibleSpawns.possibleSpawnsIsa.Length - 1) // I believe this doesn't have to have a "possibleSpawnsMat" alternative
            {
                spawnIndex++;
            }
            else
            {
                spawnIndex = 0;
                /*Debug.Log("spawnindex set to 0 because: (spawnIndex < SavedPossibleSpawns.possibleSpawnsIsa.Length - 1) is false" +
                    "\n spawnindex = " + spawnIndex +
                    "\n SavedPossibleSpawns.possibleSpawnsIsa.Length = " + SavedPossibleSpawns.possibleSpawnsIsa.Length +
                    "\n parentIndex = " + parentIndex);*/
                if(isaNoteCarouselScript.currentNoteIndex > 1) isaNoteCarouselScript.checkForWrongInputDuringNote();
                isaNoteCarouselScript.currentNoteIndex++;
                if (isaNoteCarouselScript.currentNoteIndex > 0 && RandomPieceGeneratorScript.generatedPiece[matNoteCarouselScript.currentNoteIndex] == "0") isaNoteCarouselScript.checkForWrongInputDuringNoteholder();
                this.setNextNote(false);
                setSlider();
                isaNoteCarouselScript.isaDidCorrectInputDuringNote = false;
            }
        }
        else
        {
            if (spawnIndex < SavedPossibleSpawns.possibleSpawnsMat.Length - 1) // I believe this doesn't have to have a "possibleSpawnsMat" alternative
            {
                spawnIndex++;
            }
            else
            {
                spawnIndex = 0;
                /*Debug.Log("spawnindex set to 0 because: (spawnIndex < SavedPossibleSpawns.possibleSpawnsMat.Length - 1) is false" +
                    "\n spawnindex = " + spawnIndex +
                    "\n SavedPossibleSpawns.possibleSpawnsMat.Length = " + SavedPossibleSpawns.possibleSpawnsMat.Length);*/

                if (isaNoteCarouselScript.currentNoteIndex > 1) matNoteCarouselScript.checkForWrongInputDuringNote();
                matNoteCarouselScript.currentNoteIndex++;
                if (isaNoteCarouselScript.currentNoteIndex > 0 && RandomPieceGeneratorScript.generatedPiece[matNoteCarouselScript.currentNoteIndex] == "0") matNoteCarouselScript.checkForWrongInputDuringNoteholder();
                this.setNextNote(false);
                setSlider();
                matNoteCarouselScript.matDidCorrectInputDuringNote = false;
            }
        }
        float xCoord;

        if (fromIsa)
        {
            xCoord = SavedPossibleSpawns.possibleSpawnsIsa[spawnIndex];
        }
        else
        {
            xCoord = SavedPossibleSpawns.possibleSpawnsMat[spawnIndex];
        }
        float yCoord = go.transform.position.y;
        float zCoord = go.transform.position.z;

        go.transform.position = new Vector3(xCoord, yCoord, zCoord);
    }

    public float getCurrentXCoord()
    {
        float xCoord;
        if (fromIsa)
        {
            xCoord = SavedPossibleSpawns.possibleSpawnsIsa[spawnIndex];
        }
        else
        {
            xCoord = SavedPossibleSpawns.possibleSpawnsMat[spawnIndex];
        }
        return xCoord;
    }

}
