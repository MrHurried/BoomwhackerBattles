using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using BoomWhackerBattles;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;

//WARNING: I put a lot of if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;
//'s in this script without really thinking about it too much. Keep this in mind if you come across a bug
public class NoteCarouselScript : MonoBehaviour
{
    //GAME OBJECTS
    GameObject gameManager;

    //SCRIPTS
    HealthScript isaHealthScript;
    HealthScript matHealthScript;
    private GM2FeedbackScript feedbackScript;
    private NoteBlockFunctions noteBlockFunctions;

    //INTS
    public int bpm;
    public int currentNoteIndex; // might want to make this private if a bug concerning the noteindex persists
    public int lastPlayedNoteIndex = -1;
    public int firstNBParentIndex = 0;
    public int secondNBParentIndex = 1;

    //TRANSFORMS
    [SerializeField] Transform nbHolder;

    //NOTEBLOCK
    NoteBlock isanb0, isanb1, isanb2, isanb3, isanb4, isanb5, matnb0, matnb1, matnb2, matnb3, matnb4, matnb5;
    public NoteBlock[] noteblocksIsa;
    public NoteBlock[] noteblocksMat;
    public NoteBlock[] noteblocks;
    public NoteBlock firstNB;
    public NoteBlock secondNB;

    //BOOLS
    public bool isaDidCorrectInputDuringNote = false;
    public bool matDidCorrectInputDuringNote = false;
    public bool invincibile;
    public bool buttonPressProcedureWasCalled = true; //set to true so the first note doesn't automatically get green

    //DOUBLES
    const double nbDistance = 110.0d;
    double seconds = 0d;

    //FLOATS
    public float secondsSinceLaunch = 0f;

    //This is done to prevent GameObject.Find() from failing
    //info: every time a gameObject gets instantiated, "(clone)" gets appended to it's name, which messes it up
    void Awake()
    {
        
    }

    //set currentNoteIndex to -4
    //get gamemanger object
    //set lastPlayedNoteIndex to currentNoteIndex
    void Start()
    {
        changeCloneName(); // this should always be first in the start method

        AssignGameObjectReferences();

        AssignComponentReferences();

        editOldExternalReferences();

        currentNoteIndex = -4;
        Debug.Log("just ran start. the currentnoteindex is set to: " + currentNoteIndex);
        lastPlayedNoteIndex = currentNoteIndex;
        createNoteBlocks();
    }

    /// <summary>
    /// this script is mainly used to fix issues when instantiating the NBHolder
    /// references to the NBHolders and their components don't always update in other scripts
    /// </summary>
    private void editOldExternalReferences()
    {
        if (gameObject.name.Contains("Isa")) noteBlockFunctions.isaNoteCarouselScript = this; // used to fix outdated index when reinstatiating

        if (gameObject.name.Contains("Isa"))
        {
            feedbackScript.isaBtnFeedback = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        if (gameObject.name.Contains("Mat")) 
        {
            feedbackScript.matBtnFeedback = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

    }

    private void changeCloneName()
    {
        if (gameObject.name.Contains("Isa")) gameObject.name = "NotenBovenIsaHolder";
        if (gameObject.name.Contains("Mat")) gameObject.name = "NotenBovenMatHolder";
    }

    private void AssignGameObjectReferences()
    {
        gameManager = GameObject.Find("GM2GameManager");
    }

    //There are better ways to make references, but I believe this will work well enough
    private void AssignComponentReferences()
    {
        feedbackScript = gameManager.GetComponent<GM2FeedbackScript>();
        noteBlockFunctions = gameManager.GetComponent<NoteBlockFunctions>();
        isaHealthScript = GameObject.Find("IsaHeartHolder").GetComponent<HealthScript>();
        matHealthScript = GameObject.Find("MatHeartHolder").GetComponent<HealthScript>();
    }

    public void createNoteBlocks()
    {
        Debug.Log("Creating NB's");
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
            firstNB = isanb0;
            secondNB = isanb1;
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
            firstNB = matnb0;
            secondNB = matnb1;
        }

    }

    private void UpdateFirstAndSecondNB()
    {
        firstNBParentIndex += 1;
        secondNBParentIndex += 1;

        if (firstNBParentIndex > noteblocks.Length - 1) firstNBParentIndex = 0;
        firstNB = noteblocks[firstNBParentIndex];

        if (secondNBParentIndex > noteblocks.Length - 1) secondNBParentIndex = 0;
        secondNB = noteblocks[secondNBParentIndex];
    }

    void Update()
    {
        if (this == null) return;

        Debug.Log("Max piece index = " + (RandomPieceGeneratorScript.generatedPiece.Count - 1));
        //used for waiting 3 secs, eliminates bugs and makes it easier to get ready
        secondsSinceLaunch += 1f * Time.deltaTime;
        if (secondsSinceLaunch < 3) return;

        //This code is a bit junk, I know. When everything works I can still rewrite it :)

        callInputChecksEveryFrame();

        handleInputDuringNote();

        GoLeft();

        //every new index, the checks 
        //WARNING: THIS MIGHT CAUSE ISSUES WITH CORRECT INPUT DETECTION
        //IF ANYTHING BREAKS:
        //1 try to move this if statement below callInutChecks(); if it isn't already
        //2 remove this if statement to see if the input detection still works as intended
        if (indexWasUpped())
        {
            UpdateFirstAndSecondNB();
            checkForInputsEveryIndex();
        }
    }

    //called in update
    //does everything needed when a new index appears
    public void checkForInputsEveryIndex()
    {
        if (currentNoteIndex < 1) return;
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;
        //used to fix the bug where wasnoteBeforeHoldGets called when the current note isn't even a hold note
        string currentNoteString = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //used to make sure no errors occur (my brain is fried so I can't think of something more descriptive :p)
        bool indexIsGreaterThanZero = currentNoteIndex > 0;
        string previousNoteString = "none";
        if (indexIsGreaterThanZero)
        {
            previousNoteString = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex - 1];
        }

        bool currentNoteIsHold = currentNoteString == "0";
        bool previousNoteWasNote = previousNoteString.Contains("n"); //there are two hard things in coding, one of them being naming things

        if (currentNoteIndex + 4 < RandomPieceGeneratorScript.generatedPiece.Count - 1 && currentNoteIndex > 0)
        {
            //CHECK FOR WRONG INPUTS
            if (noteBlockFunctions.wasNoteBeforeHold(false) && currentNoteIsHold)
            {
                checkForWrongInputDuringNoteHolder();
            }
        }

        //CHECK FOR WRONG INPUTS
        if (previousNoteWasNote)
        {
            checkForWrongInputDuringNote();
        }

        checkForCorrectInputEveryIndex(true);
    }

    //this will be called every frame
    public void callInputChecksEveryFrame()
    {
        if (currentNoteIndex < 0) return;
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;

        //these will be called every frame
        checkForWrongInputDuringRest();
        checkForWrongInputDuringRestHolder();
    }

    public bool indexWasUpped()
    {
        if (currentNoteIndex != lastPlayedNoteIndex)
        {
            lastPlayedNoteIndex = currentNoteIndex;
            Debug.Log("index is upped lol. current index = " + currentNoteIndex);
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
            if (gameObject.IsDestroyed() || gameObject == null) return;
            int amountOfMovements = (int)MathF.Floor((float)(seconds / secondsToWait));
            for (int i = 0; i < amountOfMovements; i++)
            {
                if (gameObject.IsDestroyed() || gameObject == null) break;
                foreach (NoteBlock nb in noteblocks)
                {
                    if (gameObject.IsDestroyed() || gameObject == null) break;
                    nb.advancePosition();
                }
            }
            seconds = seconds % secondsToWait;
        }
    }


    // Declare a function that will be called periodically to check if the function was called
    private void checkForCorrectInputEveryIndex(bool changeFirstNB)
    {
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;
        if(currentNoteIndex < 0 ) return;
        if (buttonPressProcedureWasCalled)
        {
            buttonPressProcedureWasCalled = false;
            return;
        }
        else
        {
            //doLocalCorrectInputAnim();
            if (gameObject.name.Contains("Isa")) feedbackScript.changeGradientFeedbackColor(1, true);
            else feedbackScript.changeGradientFeedbackColor(1, false);

            if (changeFirstNB) firstNB.adaptColorToFeedback(true);
            else secondNB.adaptColorToFeedback(true);
        }

    }

    //local because it is run on this gameobject
    public void doLocalCorrectInputAnim()
    {
        string holderName = gameObject.name;
        bool isIsa = holderName.Contains("Isa");

        if (isIsa)
        {
            feedbackScript.giveInputFeedback(true, true);
        }
        else
        {
            feedbackScript.giveInputFeedback(false, true);
        }

    }

    //this will be called at the start of each hold note
    public void checkForWrongInputDuringNoteHolder()
    {
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;
        // if the current piece hasn't started rolling OR if we're transitioning to another piece -> return
        if (currentNoteIndex > RandomPieceGeneratorScript.generatedPiece.Count || currentNoteIndex < 0) return;
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //if the current note is not a hold note OR if it's a rest hold note -> return
        if (strCurrentNote != "0" || !noteBlockFunctions.wasNoteBeforeHold(false)) return;

        //if the player didn't hold the right key AS SOON AS THE NEW NOTE APPEARED, they will lose health
        if (!Input.GetKey(KeyCode.Q) && nbHolder.name.Contains("Isa") && !buttonPressProcedureWasCalled)
        {
            doButtonPressProcedure(false, true, false);
            Debug.Log("Isabel did a wrong input during a note holder");
        }
        if (!Input.GetKey(KeyCode.P) && nbHolder.name.Contains("Mat") && !buttonPressProcedureWasCalled)
        {
            doButtonPressProcedure(false, false, false);
            Debug.Log("Matisse did a wrong input during a note holder");
        }


    }

    //this will be called at right before every index up
    //WARNING: this function NEEDS to be called before "didcorrectinputduringnote" is reset
    public void checkForWrongInputDuringNote()
    {
        if (currentNoteIndex < 0) return;
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //check if the current note is a rest, if it is: return
        if (strCurrentNote.Contains("r")) return;
        if (strCurrentNote == "0") return;

        if (isaDidCorrectInputDuringNote == false && nbHolder.name.Contains("Isa"))
        {
            doButtonPressProcedure(false, true, true);
        }
        if (matDidCorrectInputDuringNote == false && nbHolder.name.Contains("Mat"))
        {
            doButtonPressProcedure(false, false, true);
        }
    }

    //This method will be called every frame
    //checks if a key is pressed during a note
    // if it is, didCorrectInputDuringNote will be set to true
    // didCorrectInputDuringNote will be reset to false when a new index arises
    void handleInputDuringNote()
    {
        if (currentNoteIndex < 0) return;
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;

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
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //check to see if the current note is a rest
        if (strCurrentNote.Contains("r"))
        {
            if (Input.GetKeyDown(KeyCode.Q) && nbHolder.name.Contains("Isa"))
            {
                doButtonPressProcedure(false, true, false);
                Debug.Log("Isa pressed on the rest >:(");
            }

            if (Input.GetKeyDown(KeyCode.P) && nbHolder.name.Contains("Mat"))
            {
                doButtonPressProcedure(false, false, false);
                Debug.Log("Matisse pressed on the rest :|");
            }
        }

    }

    void checkForWrongInputDuringRestHolder()
    {
        if (currentNoteIndex < 0) return;
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //checks if the current note is a red hold note
        if (strCurrentNote == "0" && noteBlockFunctions.wasNoteBeforeHold(false) == false)
        {
            if (Input.GetKeyDown(KeyCode.Q) && nbHolder.name.Contains("Isa"))
            {
                doButtonPressProcedure(false, true, false);
                secondNB.adaptColorToFeedback(false);
            }
            if (Input.GetKeyDown(KeyCode.P) && nbHolder.name.Contains("Mat"))
            {
                doButtonPressProcedure(false, false, false);
            }
        }
    }

    void doButtonPressProcedure(bool didCorrectInput, bool fromIsa, bool changeFirstNB)
    {
        if (invincibile) return;

        buttonPressProcedureWasCalled = true;

        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //Debug.Log("didCorrectInput == false && nbHolder.name.Contains(Isa) && fromIsa = " + (didCorrectInput == false && nbHolder.name.Contains("Isa") && fromIsa));
        if (didCorrectInput == false && nbHolder.name.Contains("Isa") && fromIsa)
        {
            isaHealthScript.removeHealth(1);
            feedbackScript.changeGradientFeedbackColor(-1, true);
            if (changeFirstNB) firstNB.adaptColorToFeedback(false);
            else secondNB.adaptColorToFeedback(false);
        }
        //Debug.Log("didCorrectInput == false && nbHolder.name.Contains(Mat) && !fromIsa = " + (didCorrectInput == false && nbHolder.name.Contains("Mat") && !fromIsa));
        if (didCorrectInput == false && nbHolder.name.Contains("Mat") && !fromIsa)
        {
            matHealthScript.removeHealth(1);
            feedbackScript.changeGradientFeedbackColor(-1, false);
            if (changeFirstNB) firstNB.adaptColorToFeedback(false);
            else secondNB.adaptColorToFeedback(false);
        }
    }

}
