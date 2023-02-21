using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using BoomWhackerBattles;

public class NoteCarouselScript : MonoBehaviour
{
    [SerializeField] HealthScript isaHealthScript;
    [SerializeField] HealthScript matHealthScript;
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
        //MAJOR CHANGE: i removed the "-1". I think this fixed a bug
        currentNoteIndex =  -(noteblocks.Length);

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

        Debug.Log("didCorrectInput == false && nbHolder.name.Contains(Isa) && fromIsa = " + (didCorrectInput == false && nbHolder.name.Contains("Isa") && fromIsa));
        if (didCorrectInput == false && nbHolder.name.Contains("Isa") && fromIsa) isaHealthScript.removeHealth(1);
        if (didCorrectInput == false && nbHolder.name.Contains("Mat") && !fromIsa) matHealthScript.removeHealth(1);
    }

    void doNextRoundProcedure()
    {
        Debug.Log("advancing to next round");

        randomPieceGeneratorScript.generatePiece();
        currentNoteIndex = -(noteblocksIsa.Length);
        secondsSinceLaunch = 0f;

        bpm += bpmIncreaseAmount;
    }

}
