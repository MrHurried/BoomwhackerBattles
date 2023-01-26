using UnityEngine;

public class NoteCarouselScript : MonoBehaviour
{
    /// <summary>
    /// BUGS I NEED TO FIX:
    /// When a correct note is pressed, only do the procedure ONCE, not more
    /// </summary>


    //private RandomPieceGeneratorScript randPieceScript;

    public int bpm = 60;
    public int currentNoteIndex;

    [SerializeField] Transform nbHolder;
    Transform nb0, nb1, nb2, nb3, nb4, nb5;

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


    Transform[] noteblockTransforms;

    [SerializeField] Transform leftMaskTransform;
    [SerializeField] Transform rightMaskTransform;

    void Start()
    {
        nb0 = nbHolder.GetChild(0);
        nb1 = nbHolder.GetChild(1);
        nb2 = nbHolder.GetChild(2);
        nb3 = nbHolder.GetChild(3);
        nb4 = nbHolder.GetChild(4);
        nb5 = nbHolder.GetChild(5);

        noteblockTransforms = new Transform[6] { nb0, nb1, nb2, nb3, nb4, nb5 };

        //start the currentnodeindex to -[length of the notebar minus 1] (currently 5)
        //minus one cus there is one nb to the left of the arrow
        //setting the noteindex to something negative means we'll have a bit of time to see the notes coming
        currentNoteIndex = -(noteblockTransforms.Length - 1);


    }

    //UPDATE VARS
    const float tempSpeed = 0.001f;
    float secondsSinceLaunch = 0f;
    void Update()
    {
        //used for waiting 3 secs, eliminates bugs and makes it easier to get ready
        secondsSinceLaunch += 1f * Time.deltaTime;
        if (secondsSinceLaunch < 3) return;

        MoveNBAndChangeNBSprites();

        checkForWrongInputDuringRest();
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
    private void MoveNBAndChangeNBSprites()
    {
        //distance to move each nb :p
        const float nbDistance = 1.1f;
        // I want to make the blocks move the blocks one "space" (= nb2 gets nb2's position) in bpm / 60f seconds
        float unitsToMove = nbDistance * Time.deltaTime / (60f / bpm);

        //make a vector3 for t.Translating purposes
        Vector3 moveVector = new Vector3(-unitsToMove, 0f, 0f);

        foreach (Transform t in noteblockTransforms)
        {
            if (t.position.x <= leftMaskTransform.position.x)
            {
                //check if we've passed the empty bars at the start of a round
                if(currentNoteIndex >= 0)
                {
                    checkForRightInputDuringNote();
                }

                t.position = rightMaskTransform.position;

                //if the currentnoteindex + 5 (aka the note that will appear soon) is greater than 0
                // this is checked so that things don't get out of bounds
                if (currentNoteIndex + 5 >= 0)
                {
                    //set the notesprite sprite (P.S. t.getchild(0) is the notesprite GO that is a child of every NB)
                    //... to the currentnoteindex + 5, which is the next note to be revealed
                    SpriteRenderer newestNoteSpriteRenderer= t.GetChild(0).GetComponent<SpriteRenderer>();
                    newestNoteSpriteRenderer.sprite = getNoteSprite(currentNoteIndex + noteblockTransforms.Length - 1);

                    //index of the newest note
                    int newestNoteIndex = currentNoteIndex + noteblockTransforms.Length - 1;

                    //check if the note is a hold
                    if (getNoteSprite(newestNoteIndex) == emptySprite)
                    {
                        //Give the newest note a red or green note slider sprite. this sprite is
                        //inserted in the "noteholdsprite" GO (that is the second child of any noteblock)

                        if (wasNoteBeforeHold(true))
                            t.GetChild(1).GetComponent<SpriteRenderer>().sprite = noteSliderSprite;
                        else
                            t.GetChild(1).GetComponent<SpriteRenderer>().sprite = restSliderSprite;
                    }
                    else
                    {
                        //this is run when the newest note isn't a hold.
                        // set the noteholdsprite to an empty pixel. 
                        // this way there is no slider overlay on top of a non-hold sprite
                        t.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptySprite;
                    }
                }
                //when a new note spawns: advance the currentNoteIndex
                if (currentNoteIndex <= RandomPieceGeneratorScript.generatedPiece.Count) currentNoteIndex++;
            }
            //move the noteblock a little to the left
            t.Translate(moveVector);
        }
    }

    void checkForRightInputDuringNote()
    {
        if (currentNoteIndex < 0) return;

        //get the string value of the current note (aka the note under the arrow)
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        //check if the current note is a rest, if it is: return
        if (strCurrentNote.Contains("r")) return;

        if(strCurrentNote == "0" && wasNoteBeforeHold(false))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                doButtonPressProcedure(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                doButtonPressProcedure(true);
            }
        }
    }

    void checkForWrongInputDuringRest()
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
        //TESTING
        string strCurrentNote = RandomPieceGeneratorScript.generatedPiece[currentNoteIndex];

        Debug.Log("did correct input? " + didCorrectInput + "\n note: " +strCurrentNote);
        //health should be decreased here
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
}
