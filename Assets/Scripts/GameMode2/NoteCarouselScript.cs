using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using BoomWhackerBattles;
using UnityEditor.ShaderGraph.Drawing;

public class NoteCarouselScript : MonoBehaviour
{
    //SCRIPTS
    [SerializeField] HealthScript isaHealthScript;
    [SerializeField] HealthScript matHealthScript;
    [SerializeField] RandomPieceGeneratorScript randomPieceGeneratorScript;
    public GM2FeedbackScript feedbackScript;
    private NoteBlockFunctions noteBlockFunctions;

    //INTS
    public int bpm = 60;
    public int currentNoteIndex; // might want to make this private if a bug concerning the noteindex persists
    public int bpmIncreaseAmount = 30;
    public int lastPlayedNoteIndex;

    //TRANSFORMS
    [SerializeField] Transform nbHolder;

    //NOTEBLOCK
    NoteBlock isanb0, isanb1, isanb2, isanb3, isanb4, isanb5, matnb0, matnb1, matnb2, matnb3, matnb4, matnb5;
    NoteBlock[] noteblocksIsa;
    NoteBlock[] noteblocksMat;
    NoteBlock[] noteblocks;

    //BOOLS
    public bool isaDidCorrectInputDuringNote = false;
    public bool matDidCorrectInputDuringNote = false;
    public bool invincibile;
    public bool buttonPressProcedureWasCalled;

    //DOUBLES
    const double nbDistance = 110.0d;
    double seconds = 0d;

    //FLOATS
    float secondsSinceLaunch = 0f;


    //set currentNoteIndex to -4
    //get gamemanger object
    //set lastPlayedNoteIndex to currentNoteIndex
    void Start()
    {
        noteBlockFunctions = GameObject.Find("GameManager").GetComponent<NoteBlockFunctions>();
        currentNoteIndex = -4;
        lastPlayedNoteIndex = currentNoteIndex;
        createNoteBlocks();
    }

    private void createNoteBlocks()
    {
        //this is done to prevent bugs
        if (nbHolder.name.Contains("Isa"))
        {
            isanb0 = new NoteBlock(0, true);
            isanb1 = new NoteBlock(1, true);
            isanb2 = new NoteBlock(2, true);
            isanb3 = new NoteBlock(3, true);
            isanb4 = new NoteBlock(4, true);
            isanb5 = new NoteBlock(5, true);
            isanb5.setNextNote(true);
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
            matnb5.setNextNote(true);
            noteblocksMat = new NoteBlock[6] { matnb0, matnb1, matnb2, matnb3, matnb4, matnb5 };
            noteblocks = noteblocksMat;
        }

    }

    void Update()
    {
        //used for waiting 3 secs, eliminates bugs and makes it easier to get ready
        secondsSinceLaunch += 1f * Time.deltaTime;
        if (secondsSinceLaunch < 3) return;

        callInputChecks();

        handleInputDuringNote();

        GoLeft();

        //every new index, the checks 
        //WARNING: THIS MIGHT CAUSE ISSUES WITH CORRECT INPUT DETECTION
        //IF ANYTHING BREAKS:
        //1 try to move this if statement below callInutChecks(); if it isn't already
        //2 remove this if statement to see if the input detection still works as intended
        if (indexWasUpped())
        {
            isaDidCorrectInputDuringNote = false;
            matDidCorrectInputDuringNote = false;
        }

        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count - 1)
        {
            Debug.Log("should be doin the procedure now");
            doNextRoundProcedure();
        }
    }

    //this will be called every frame
    public void callInputChecks()
    {
        if(currentNoteIndex < 0) return;

        //used to fix the bug where wasnoteBeforeHoldGets called when the current note isn't even a hold note
        string currentNoteString = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //used to make sure no errors occur (my brain is fried so I can't think of something more descriptive :p)
        bool indexIsGreaterThanZero = currentNoteIndex > 0;
        string previousNoteString = "none";
        if(indexIsGreaterThanZero) 
        { 
            previousNoteString = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex-1];
        }
        
        bool currentNoteIsHold = currentNoteString == "0";
        bool previousNoteWasNote = previousNoteString.Contains("n"); //there are two hard things in coding, one of them being naming things
        //these will be called every new index
        if (indexWasUpped())
        {
            if (noteBlockFunctions.wasNoteBeforeHold(false) && currentNoteIsHold)
            {
                checkForWrongInputDuringNoteHolder();
            }
            if (previousNoteWasNote){
                checkForWrongInputDuringNote();
            }
        }

        //these will be called every frame
        checkForWrongInputDuringRest();
        checkForWrongInputDuringRestHolder();

        checkForCorrectInput();  
    }

    public bool indexWasUpped()
    {
        if (currentNoteIndex != lastPlayedNoteIndex)
        {
            lastPlayedNoteIndex = currentNoteIndex;
            return true;
        }
        else { return false; }
    }

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


    // Declare a function that will be called periodically to check if the function was called
    private void checkForCorrectInput()
    {
        // Check if the bool is true
        if (indexWasUpped() )
        {
            if (buttonPressProcedureWasCalled)
            {
                buttonPressProcedureWasCalled = false;
                return;
            }
            else
            {
                doLocalCorrectInputAnim();
            }
        }
    }

    //local because it is run on this gameobject
    public void doLocalCorrectInputAnim()
    {
        string holderName = gameObject.name;
        bool isIsa = holderName.Contains("Isa");

        if(isIsa)
        {
            feedbackScript.isaAnimator.Play("CorrectInputAnim"); 
        }
        else
        {
            feedbackScript.matAnimator.Play("CorrectInputAnim");
        }

    }

    //this will be called at the start of each hold note
    public void checkForWrongInputDuringNoteHolder()
    {
        // if the current piece hasn't started rolling OR if we're transitioning to another piece -> return
        if (currentNoteIndex > RandomPieceGeneratorScript.generatedPiece.Count || currentNoteIndex < 0) return;
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //if the current note is not a hold note OR if it's a rest hold note -> return
        if (strCurrentNote != "0" || !noteBlockFunctions.wasNoteBeforeHold(false)) return;

        //if the player didn't hold the right key AS SOON AS THE NEW NOTE APPEARED, they will lose health
        if (!Input.GetKey(KeyCode.Q) && nbHolder.name.Contains("Isa"))
        {
            doButtonPressProcedure(false, true);
            Debug.Log("Isabel did a wrong input during a note holder");
        }
        if (!Input.GetKey(KeyCode.P) && nbHolder.name.Contains("Mat"))
        {
            doButtonPressProcedure(false, false);
            Debug.Log("Matisse did a wrong input during a note holder");
        }


    }

    //this will be called at right before every index up
    //WARNING: this function NEEDS to be called before "didcorrectinputduringnote" is reset
    public void checkForWrongInputDuringNote()
    {
        if (currentNoteIndex < 0) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //check if the current note is a rest, if it is: return
        if (strCurrentNote.Contains("r")) return;
        if (strCurrentNote == "0") return;

        if (isaDidCorrectInputDuringNote == false && nbHolder.name.Contains("Isa"))
        {
            doButtonPressProcedure(false, true);
        }
        if (matDidCorrectInputDuringNote == false && nbHolder.name.Contains("Mat"))
        {
            doButtonPressProcedure(false, false);
        }
    }

    //This method will be called every frame
    //checks if a key is pressed during a note
    // if it is, didCorrectInputDuringNote will be set to true
    // didCorrectInputDuringNote will be reset to false when a new index arises
    void handleInputDuringNote()
    {
        if (currentNoteIndex < 0) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //check if the current note is a rest, if it is: return
        if (strCurrentNote.Contains("r")) return;
        if (strCurrentNote == "0") return;
        if (!isaDidCorrectInputDuringNote && nbHolder.name.Contains("Isa"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isaDidCorrectInputDuringNote = true;
                Debug.Log("Isabel did correct input during this note");
            }
        }
        if (!matDidCorrectInputDuringNote && nbHolder.name.Contains("Mat"))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                matDidCorrectInputDuringNote = true;
                Debug.Log("matisse did correct input during this note");
            }
        }
    }

    void checkForWrongInputDuringRest()
    {
        if (currentNoteIndex < 0) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //check to see if the current note is a rest
        if (strCurrentNote.Contains("r"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                doButtonPressProcedure(false, true);
                Debug.Log("Isa pressed on the rest >:(");
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                doButtonPressProcedure(false, false);
                Debug.Log("Matisse pressed on the rest :|");
            }
        }

    }

    void checkForWrongInputDuringRestHolder()
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
    }

    void doButtonPressProcedure(bool didCorrectInput, bool fromIsa)
    {
        if (invincibile) return;

        buttonPressProcedureWasCalled = true;

        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //Debug.Log("didCorrectInput == false && nbHolder.name.Contains(Isa) && fromIsa = " + (didCorrectInput == false && nbHolder.name.Contains("Isa") && fromIsa));
        if (didCorrectInput == false && nbHolder.name.Contains("Isa") && fromIsa) { 
            isaHealthScript.removeHealth(1);
        }
        //Debug.Log("didCorrectInput == false && nbHolder.name.Contains(Mat) && !fromIsa = " + (didCorrectInput == false && nbHolder.name.Contains("Mat") && !fromIsa));
        if (didCorrectInput == false && nbHolder.name.Contains("Mat") && !fromIsa)
        {
            matHealthScript.removeHealth(1);
        }
    }

    void doNextRoundProcedure()
    {
        Debug.Log("advancing to next round");

        randomPieceGeneratorScript.generatePiece();
        currentNoteIndex = -(noteblocksIsa.Length - 1);
        secondsSinceLaunch = 0f;

        bpm += bpmIncreaseAmount;
    }

}
