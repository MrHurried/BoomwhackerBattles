using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteCarouselScript : MonoBehaviour
{
    //private RandomPieceGeneratorScript randPieceScript;

    string[] generatedPiece;

    public int bpm = 60;
    public int currentNoteIndex;

    [SerializeField] Transform nbHolder;
    Transform nb0, nb1, nb2, nb3, nb4, nb5;

    //NOTE SPRITES
    [SerializeField] Sprite note2;
    [SerializeField] Sprite note4;
    [SerializeField] Sprite note8;
    [SerializeField] Sprite note16;
    [SerializeField] Sprite rest2;
    [SerializeField] Sprite rest4;
    [SerializeField] Sprite rest8;
    [SerializeField] Sprite rest16;

    Transform[] noteblocks;

    [SerializeField] Transform leftMaskTransform;
    [SerializeField] Transform rightMaskTransform;

    // Start is called before the first frame update
    void Start()
    {
        nb0 = nbHolder.GetChild(0);
        nb1 = nbHolder.GetChild(1);
        nb2 = nbHolder.GetChild(2);
        nb3 = nbHolder.GetChild(3);
        nb4 = nbHolder.GetChild(4);
        nb5 = nbHolder.GetChild(5);

        generatedPiece = RandomPieceGeneratorScript.generatedPiece;

        noteblocks = new Transform[6] { nb0, nb1, nb2, nb3, nb4, nb5 };

        //THIS DOES NOT APPLY ANYMORE: minus one cus there is one nb to the left of the arrow
        currentNoteIndex = -(noteblocks.Length-1) ;

        //temporary test
        //currentNoteIndex = 0;


        //StartCoroutine(tempAutomaticProceedCarousel());
    }

    //FIXEDUPDATE VARS
    const float tempSpeed = 0.001f;
    float secondsSinceLaunch = 0f;
    float secondsSinceCarouselStart = 0f;
    bool lastChangedNoteHasSpawned = false;
    void Update()
    {
        //used for waiting 3 secs, eliminates bugs
        secondsSinceLaunch += 1f * Time.deltaTime;
        if (secondsSinceLaunch < 3) return;
        //update the seconds since carousel start
        secondsSinceCarouselStart += 1f * Time.deltaTime;

        //distance to move each nb :p
        const float nbDistance = 1.1f;
        // I want to make the blocks move the blocks one "space" (= nb2 gets nb2's position) in bpm / 60f seconds
        float unitsToMove = nbDistance * Time.deltaTime / (60f/bpm);

        //make a vector3 for t.Translating purposes
        Vector3 moveVector = new Vector3(-unitsToMove, 0f, 0f);

        foreach (Transform t in noteblocks)
        {
            if (t.position.x <= leftMaskTransform.position.x) 
            {
                
                t.position = rightMaskTransform.position;
                if (currentNoteIndex + 5 >= 0) 
                {
                    t.GetChild(0).GetComponent<SpriteRenderer>().sprite = getNoteSprite(currentNoteIndex + noteblocks.Length-1);
                }
                if(currentNoteIndex < RandomPieceGeneratorScript.noteAmount) currentNoteIndex++;
            }
            t.Translate(moveVector);
        }
        /*
        for(int i = 0; i < noteblocks.Length; i++)
        {
            Transform t = noteblocks[i];

            if (t.position.x <= leftMaskTransform.position.x)
            {
                t.position = rightMaskTransform.position;
                if (currentNoteIndex >= 0) t.GetChild(0).GetComponent<SpriteRenderer>().sprite = getCurrentNoteSprite();
                if (currentNoteIndex < RandomPieceGeneratorScript.noteAmount) currentNoteIndex++;
            }
            t.Translate(moveVector);

        }*/
    }

    public Sprite getNoteSprite(int noteIndex)
    {
        string strCurrentNote = generatedPiece[noteIndex];
        Sprite sprite;

        //Set sprite var to right image, according to the current note (strCurrentNote)
        switch (strCurrentNote)
        {
            case "2":
                sprite = note2;
                break;
            case "4":
                sprite = note4;
                break;
            case "8":
                sprite = note8;
                break;
            case "16":
                sprite = note16;
                break;
            case "r2":
                sprite = rest2;
                break;
            case "r4":
                sprite = rest4;
                break;
            case "r8":
                sprite = rest8;
                break;
            case "r16":
                sprite = rest16;
                break;
            default:
                sprite = note2;
                break;
        }

        return sprite;
    }

    public int getNoteOrRestLength(string strNote)
    {
        if (strNote[0] == 'r')
        {
            return int.Parse(strNote.Replace("r", ""));
        }
        else
        {
            return int.Parse(strNote);
        }
    }



    /* IEnumerator tempAutomaticProceedCarousel()
     {

         for (; ; )
         {
             int symbolLength = getNoteOrRestLength(generatedPiece[currentNoteIndex]);
             Debug.Log("symbol length: " + symbolLength);


             //epic gamer formula
             float beatspersecond = 0f;
             float temp = 0f;
             float secondsToWait = (bpm / 60f) / (symbolLength / 4f);

             Debug.Log(secondsToWait);

             yield return new WaitForSeconds(secondsToWait);

             if (RandomPieceGeneratorScript.noteAmount - 1 > currentNoteIndex) currentNoteIndex++;
             else break;
         }

     } */
}
