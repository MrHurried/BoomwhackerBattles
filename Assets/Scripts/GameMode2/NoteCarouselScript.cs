using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using BoomWhackerBattles;

public class NoteCarouselScript : MonoBehaviour
{
    [SerializeField] HealthScript healthScript;
    [SerializeField] RandomPieceGeneratorScript randomPieceGeneratorScript;

    public int bpm = 60;
    public int currentNoteIndex;
    public int bpmIncreaseAmount = 30;

    public bool invincibile;

    [SerializeField] Transform nbHolder;
    NoteBlock isanb0, isanb1, isanb2, isanb3, isanb4, isanb5, matnb0, matnb1, matnb2, matnb3, matnb4, matnb5;

    //NOTE SPRITES
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite note2sprite;
    [SerializeField] Sprite note4sprite;
    [SerializeField] Sprite note8sprite;
    [SerializeField] Sprite note16sprite;
    [SerializeField] Sprite rest2sprite;
    [SerializeField] Sprite rest4sprite;
    [SerializeField] Sprite rest8sprite;
    [SerializeField] Sprite rest16sprite;

    //SLIDER SPRITES
    [SerializeField] Sprite noteSliderSprite;
    [SerializeField] Sprite restSliderSprite;


    NoteBlock[] noteblocksIsa;
    NoteBlock[] noteblocksMat;

    public float leftSpawnX;

    const double nbDistance = 110.0d;

    void Start()
    {
        isanb0 = new NoteBlock(0, true);
        isanb1 = new NoteBlock(1, true);
        isanb2 = new NoteBlock(2, true);
        isanb3 = new NoteBlock(3, true);
        isanb4 = new NoteBlock(4, true);
        isanb5 = new NoteBlock(5, true);

        matnb0 = new NoteBlock(0, false);
        matnb1 = new NoteBlock(1, false);
        matnb2 = new NoteBlock(2, false);
        matnb3 = new NoteBlock(3, false);
        matnb4 = new NoteBlock(4, false);
        matnb5 = new NoteBlock(5, false);

        noteblocksIsa = new NoteBlock[6] { isanb0, isanb1, isanb2, isanb3, isanb4, isanb5 };
        noteblocksMat = new NoteBlock[6] { matnb0, matnb1, matnb2, matnb3, matnb4, matnb5 };

        leftSpawnX = transform.GetChild(0).GetChild(noteblocksIsa.Length).transform.position.x; // the first child ("noteblocks") last child "leftboundmask". check in the hierarchy for a better insight

        //start the currentnodeindex to -[length of the notebar minus 1] (currently 5)
        //minus one cus there is one nb to the left of the arrow
        //setting the noteindex to something negative means we'll have a bit of time to see the notes coming
        currentNoteIndex = -(noteblocksIsa.Length - 1);

    }

    //UPDATE VARS
    float secondsSinceLaunch = 0f;
    float secondsAfterFirstUpdate = 0f;
    void Update()
    {
        //used for waiting 3 secs, eliminates bugs and makes it easier to get ready
        secondsSinceLaunch += 1f * Time.deltaTime;
        if (secondsSinceLaunch < 3) return;

        secondsAfterFirstUpdate += 1f * Time.deltaTime;
        //if (secondsAfterFirstUpdate > 1) return;

        checkForWrongInputDuringRestAndRestHolder();

        checkForWrongInputDuringNote();

        GoLeft();

        ChangeNBSprites();

        //testing
        Debug.Log("piece length: " + RandomPieceGeneratorScript.generatedPiece.Count);
        //check if the full piece is played, then run the according procedure
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count - 1)
        {
            Debug.Log("should be doin the procedure now");
            doNextRoundProcedure();
        }
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
        int newestNoteIndex = currentNoteIndex + noteblocksIsa.Length - 1;

        foreach (NoteBlock nb in noteblocksIsa)
        {

            //MASSIVE CHANGE HERE: == to <=. this will hopefully put an end to the headaches the past few weeks in turn for a (slightly) worse accuracy
            if (/*nb.getCurrentXCoord() */ -763f <= leftSpawnX)
            {

                if (currentNoteIndex > 0)
                {
                    checkForWrongInputDuringNoteholder();
                }

                //if the currentnoteindex + 5 (aka the note that will appear soon) is greater than 0
                // this is checked so that things don't get out of bounds
                if (currentNoteIndex + 5 >= 0)
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
                }

                if (newestNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count)
                {
                    nb.setNote(emptySprite);
                    nb.setSlider(emptySprite);
                }

                //when a new note spawns: advance the currentNoteIndex
                if (currentNoteIndex < RandomPieceGeneratorScript.generatedPiece.Count)
                {
                    currentNoteIndex++;
                }
            }
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
            for(int i = 0; i < amountOfMovements; i++)
            {
                foreach (NoteBlock nb in noteblocksIsa)
                {
                    nb.advancePosition();
                }
            }
            seconds = seconds%secondsToWait;
       }
    }

    void checkForWrongInputDuringNoteholder()
    {
        if (currentNoteIndex > RandomPieceGeneratorScript.generatedPiece.Count) return;
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        if (strCurrentNote == "0" && wasNoteBeforeHold(false))
        {
            if (!Input.GetKey(KeyCode.Q))
            {
                doButtonPressProcedure(false);
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
            doButtonPressProcedure(true);
        }

    }

    void checkForWrongInputDuringRestAndRestHolder()
    {
        if (currentNoteIndex < 0) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //checks if the current note is a red hold note
        if (strCurrentNote == "0" && wasNoteBeforeHold(false) == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                doButtonPressProcedure(false);
            }
        }
        //check to see if the current note is a rest
        if (strCurrentNote.Contains("r"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                doButtonPressProcedure(false);
            }
        }

    }

    void doButtonPressProcedure(bool didCorrectInput)
    {
        if (invincibile) return;

        //TESTING

        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        Debug.Log("did correct input? " + didCorrectInput + "\n note: " + strCurrentNote);
        //health should be decreased here

        if (didCorrectInput == false) healthScript.removeHealth(1);
    }

    private bool wasNoteBeforeHold(bool startFromNewestNote)
    {
        string strCurrentNote = "0";

        //if we need to start from the newest note, add 5
        int index = currentNoteIndex;
        if (startFromNewestNote) index = currentNoteIndex + 5;

        while (strCurrentNote == "0")
        {
            index--;
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

    void doNextRoundProcedure()
    {
        Debug.Log("advancing to next round");

        randomPieceGeneratorScript.generatePiece();
        currentNoteIndex = -(noteblocksIsa.Length - 1);
        secondsSinceLaunch = 0f;

        bpm += bpmIncreaseAmount;
    }
}
