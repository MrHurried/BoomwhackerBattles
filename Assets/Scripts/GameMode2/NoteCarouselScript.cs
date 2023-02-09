using System;
using UnityEngine;

public class NoteCarouselScript : MonoBehaviour
{
    /// <summary>
    /// BUGS I NEED TO FIX:
    /// When a correct note is pressed, only do the procedure ONCE, not more
    /// </summary>


    //private RandomPieceGeneratorScript randPieceScript;

    [SerializeField] HealthScript healthScript;
    [SerializeField] RandomPieceGeneratorScript randomPieceGeneratorScript;

    public float bpm = 60f;
    public int currentNoteIndex;
    public int bpmIncreaseAmount = 30;

    public bool invincibile;

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

    Vector2 _movement = Vector2.zero;
    public int moveSpeed = 1;
    float moveIncrement;


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

        MoveNBAndChangeNBSprites();

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
    float current;
    private void MoveNBAndChangeNBSprites()
    {
        //RIP old movement system

        //index of the newest note
        int newestNoteIndex = currentNoteIndex + noteblockTransforms.Length - 1;

        foreach (Transform t in noteblockTransforms)
        {
            if (t.position.x == leftMaskTransform.position.x)
            {

                if (currentNoteIndex > 0)
                {
                    checkForWrongInputDuringNoteholder();
                }

                t.position = rightMaskTransform.position;



                //if the currentnoteindex + 5 (aka the note that will appear soon) is greater than 0
                // this is checked so that things don't get out of bounds
                if (currentNoteIndex + 5 >= 0)
                {
                    //set the notesprite sprite (P.S. t.getchild(0) is the notesprite GO that is a child of every NB)
                    //... to the currentnoteindex + 5, which is the next note to be revealed
                    SpriteRenderer newestNoteSpriteRenderer = t.GetChild(0).GetComponent<SpriteRenderer>();
                    newestNoteSpriteRenderer.sprite = getNoteSprite(newestNoteIndex);


                    //check if the note is a hold
                    if (getNoteSprite(newestNoteIndex) == emptySprite && newestNoteIndex < RandomPieceGeneratorScript.generatedPiece.Count)
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

                if (newestNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count)
                {
                    t.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptySprite;
                    t.GetChild(0).GetComponent<SpriteRenderer>().sprite = emptySprite;
                }

                //when a new note spawns: advance the currentNoteIndex
                if (currentNoteIndex < RandomPieceGeneratorScript.generatedPiece.Count) currentNoteIndex++;
            }
            //move the noteblock a little to the left
            //t.Translate(moveVector);



            const float nbDistance = 1.1f;

            moveIncrement = (nbDistance / (60f / bpm)); //* Time.deltaTime;
                                                        //moveIncrement = (Math.Round(moveIncrement,1));
                                                        //current = Mathf.MoveTowards(t.position.x, leftMaskTransform.position.x, moveIncrement);

            //SmoveIncrement = Math.Round(moveIncrement, 1);

            //t.Translate((Vector3)(new Vector3d(-moveIncrement, 0d, 0d)));

            //double xpos = Mathf.Clamp(t.position.x, leftMaskTransform.position.x, rightMaskTransform.position.x);
            //t.position = ((Vector3)(new Vector3d(xpos, 0d, 0d)));



            Debug.Log("still moving lololol");
        }


    }

    private void LateUpdate()
    {

        GoLeft();

        // Clamp the current movement

        //EDITED: second param of vector2 construct
        Vector2 clamped_movement = new Vector2((int)_movement.x, (int)0);
        // Check if a movement is needed (more than 1px move)
        if (clamped_movement.magnitude >= 1.0f)
        {
            // Update velocity, removing the actual movement
            _movement = _movement - clamped_movement;
            if (clamped_movement != Vector2.zero)
            {
                foreach (Transform t in noteblockTransforms)
                {
                    //TESTING
                    Debug.Log("_movement = " + _movement);


                    // Move to the new position

                    //EDITED: ClampVector(transform.position) to 
                    t.position = new Vector2((int)t.position.x, (int)t.position.y) + clamped_movement;
                }

            }
        }
    }

    public void GoLeft()
    {
        // speed is defined in pixel per second.
        _movement.x -= moveSpeed * Time.deltaTime;
        //_movement.x -= moveSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //used for waiting 3 secs, eliminates bugs and makes it easier to get ready
        secondsSinceLaunch += 1f * Time.deltaTime;
        if (secondsSinceLaunch < 3) return;

        foreach (Transform t in noteblockTransforms)
        {



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
        currentNoteIndex = -(noteblockTransforms.Length - 1);
        secondsSinceLaunch = 0f;

        bpm += bpmIncreaseAmount;
    }
}
