using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using BoomWhackerBattles;

public class NoteCarouselScript : MonoBehaviour
{
    [SerializeField] HealthScript healthScript;
    [SerializeField] RandomPieceGeneratorScript randomPieceGeneratorScript;
    private NoteBlockFunctions noteBlockFunctions;
    public int bpm = 60;
    public int currentNoteIndex; // might want to make this private if a bug concerning the noteindex persists
    public int bpmIncreaseAmount = 30;

    public bool invincibile;

    [SerializeField] Transform nbHolder;
    NoteBlock isanb0, isanb1, isanb2, isanb3, isanb4, isanb5, matnb0, matnb1, matnb2, matnb3, matnb4, matnb5;


    NoteBlock[] noteblocksIsa;
    NoteBlock[] noteblocksMat;
    NoteBlock[] noteblocks;

    [SerializeField] private float leftSpawnX;

    const double nbDistance = 110.0d;

    bool debugRunning = true;
    int debugInt = 0;
    void runDebug()
    {
        //LOGGING THE noteblocks array
        foreach(NoteBlock nb in noteblocks)
        {
            Debug.Log("GO name = " + gameObject.name +
                "\n fromIsa? " + nb.fromIsa);
        } 
    }

    void Start()
    {
        noteBlockFunctions = gameObject.AddComponent<NoteBlockFunctions>();


        //this is done to prevent bugs
        if (nbHolder.name.Contains("Isa"))
        {
            isanb0 = new NoteBlock(0, true);
            isanb1 = new NoteBlock(1, true);
            isanb2 = new NoteBlock(2, true);
            isanb3 = new NoteBlock(3, true);
            isanb4 = new NoteBlock(4, true);
            isanb5 = new NoteBlock(5, true);
            noteblocksIsa = new NoteBlock[6] { isanb0, isanb1, isanb2, isanb3, isanb4, isanb5 };
            noteblocks = noteblocksIsa;
        }
        else
        { 
            matnb0 = new NoteBlock(0, false);
            matnb1 = new NoteBlock(1, false);
            matnb2 = new NoteBlock(2, false);
            matnb3 = new NoteBlock(3, false);
            matnb4 = new NoteBlock(4, false);
            matnb5 = new NoteBlock(5, false);
            noteblocksMat = new NoteBlock[6] { matnb0, matnb1, matnb2, matnb3, matnb4, matnb5 };
            noteblocks = noteblocksMat;
        }


        //noteblocks = new NoteBlock[] { isanb0, isanb1, isanb2, isanb3, isanb4, isanb5, matnb0, matnb1, matnb2, matnb3, matnb4, matnb5 };

        //start the currentnodeindex to -[length of the notebar minus 1] (currently 5)
        //minus one cus there is one nb to the left of the arrow
        //setting the noteindex to something negative means we'll have a bit of time to see the notes coming
        currentNoteIndex =  -(noteblocks.Length - 1);

        //runDebug();
    }

    //UPDATE VARS
    float secondsSinceLaunch = 0f;
    float secondsAfterFirstUpdate = 0f;
    void Update()
    {
        if (debugRunning == false) return;
        //used for waiting 3 secs, eliminates bugs and makes it easier to get ready
        secondsSinceLaunch += 1f * Time.deltaTime;
        if (secondsSinceLaunch < 3) return;

        secondsAfterFirstUpdate += 1f * Time.deltaTime;
        //if (secondsAfterFirstUpdate > 1) return;

        checkForWrongInputDuringRestAndRestHolder();

        checkForWrongInputDuringNote();

        GoLeft();

        //ChangeNBSprites();

        //advanceIndex();

        //testing
        Debug.Log("piece length: " + RandomPieceGeneratorScript.generatedPiece.Count);
        //check if the full piece is played, then run the according procedure
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count - 1)
        {
            Debug.Log("should be doin the procedure now");
            doNextRoundProcedure();
        }
    }

    double seconds = 0d;
    public void GoLeft()
    {
        seconds += Time.deltaTime;
        double secondsToWait = ((60.0d / bpm) / nbDistance);
        if (seconds >= secondsToWait)
        {
            int amountOfMovements = (int)MathF.Floor((float)(seconds / secondsToWait));
            for (int i = 0; i < amountOfMovements; i++)
            {
                foreach (NoteBlock nb in noteblocks)
                {
                    nb.advancePosition();
                }
            }
            seconds = seconds % secondsToWait;
        }
    }

    void checkForWrongInputDuringNoteholder()
    {
        if (currentNoteIndex > RandomPieceGeneratorScript.generatedPiece.Count) return;
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex]; ;

        if (strCurrentNote == "0" && noteBlockFunctions.wasNoteBeforeHold(false))
        {
            if (!Input.GetKey(KeyCode.Q))
            {
                doButtonPressProcedure(false, false);
            }
            if (!Input.GetKey(KeyCode.P))
            {
                doButtonPressProcedure(false, false);
            }
        }

    }

    void checkForWrongInputDuringNote()
    {
        if (currentNoteIndex < 0) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //check if the current note is a rest, if it is: return
        if (strCurrentNote.Contains("r")) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            doButtonPressProcedure(true, true);
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            doButtonPressProcedure(true, true);
        }

    }

    void checkForWrongInputDuringRestAndRestHolder()
    {
        if (currentNoteIndex < 0) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //checks if the current note is a red hold note
        if (strCurrentNote == "0" && noteBlockFunctions.wasNoteBeforeHold(false) == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                doButtonPressProcedure(false, true);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                doButtonPressProcedure(false, false);
            }
        }
        //check to see if the current note is a rest
        if (strCurrentNote.Contains("r"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                doButtonPressProcedure(false, true);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                doButtonPressProcedure(false, false);
            }
        }

    }

    void doButtonPressProcedure(bool didCorrectInput, bool fromIsa)
    {
        if (invincibile) return;

        //TESTING

        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        Debug.Log("did correct input? " + didCorrectInput + "\n note: " + strCurrentNote);
        //health should be decreased here

        if (didCorrectInput == false) healthScript.removeHealth(1);
    }

    void doNextRoundProcedure()
    {
        Debug.Log("advancing to next round");

        randomPieceGeneratorScript.generatePiece();
        currentNoteIndex = -(noteblocksIsa.Length - 1);
        secondsSinceLaunch = 0f;

        bpm += bpmIncreaseAmount;
    }


    /// <summary>
    /// what does this function do?
    /// 1. advance currentNoteIndex
    /// 2. move all nb's to the left
    ///     2.1 moves the nb to the right mask when it goes in the left mask
    /// 3. change the sprite of the to-be-revealed note 
    ///     3.1 if the note is a hold note, check if the note before the hold note(s) is
    ///         a note. if so, give the notesprite GO a 
    /// 4. checks when a new note spawns. when it does: call the "CheckForRightInputDuringNote" method
    /// </summary>
    /// 
    private void ChangeNBSprites()
    {
        //RIP old movement system

        //index of the newest note
        int newestNoteIndex = currentNoteIndex + noteblocks.Length - 1;

        foreach (NoteBlock nb in noteblocks)
        {

            //MASSIVE CHANGE HERE: == to <=. this will hopefully put an end to the headaches the past few weeks in turn for a (slightly) worse accuracy
            if (nb.getCurrentXCoord() <= leftSpawnX)
            {

                if (currentNoteIndex > 0)
                {
                    checkForWrongInputDuringNoteholder();
                }

                //if the currentnoteindex + 5 (aka the note that will appear soon) is greater than 0
                // this is checked so that things don't get out of bounds
                /*if (currentNoteIndex + 5 >= 0)
                {

                    //check if the note is a hold
                    if (getNoteSprite(newestNoteIndex) == emptySprite && newestNoteIndex < RandomPieceGeneratorScript.generatedPiece.Count)
                    {
                        //Give the newest note a red or green note slider sprite. this sprite is
                        //inserted in the "noteholdsprite" GO (that is the second child of any noteblock)

                        if (wasNoteBeforeHold(true))
                            nb.setSlider(noteSliderSprite);
                        else
                            nb.setSlider(restSliderSprite);
                    }
                    else
                    {
                        //this is run when the newest note isn't a hold.
                        // set the noteholdsprite to an empty pixel. 
                        // this way there is no slider overlay on top of a non-hold sprite
                        nb.setSlider(emptySprite);
                    }
                }*/

                //MAJOR CHANGE: changed >= into <= . this MIGHT BREAK THE GAME
                /*if (newestNoteIndex <= RandomPieceGeneratorScript.generatedPiece.Count)
                {
                    nb.setNote(emptySprite);
                    nb.setSlider(emptySprite);
                }*/

                //debugInt++;
                if(debugInt == 2) debugRunning = false ;
            }
        }

    }

    int lastParentIdexToHitLeftMask = 5; //this is set to 5 because 5 hits the LM last, thus allowing the nb0 to up the index. see "advanceIndex()" for more info
    /*public void advanceIndex()
    {
        //dit wordt alleen voor Isa's NB gedaan omdat de noteindex anders meerdere keren wordt geupt
        foreach (NoteBlock nb in noteblocks)
        {
            //when a new note spawns: advance the currentNoteIndex
            if (lastParentIdexToHitLeftMask != nb.parentIndex && nb.getCurrentXCoord() <= leftSpawnX && currentNoteIndex < RandomPieceGeneratorScript.generatedPiece.Count)
            {
                currentNoteIndex++;
                lastParentIdexToHitLeftMask = nb.parentIndex;
                Debug.Log("index upped. " +
                    "\nparentIndex = " + nb.parentIndex +
                    "\n nb.getCurrentXcoord = "+ nb.getCurrentXCoord() +
                    "\n leftSpawnX = " + leftSpawnX +
                    "\n fromIsa? " + nb.fromIsa);
            }
        }
    }*/

}
