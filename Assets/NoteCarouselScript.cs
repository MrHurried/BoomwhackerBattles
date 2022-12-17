using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteCarouselScript : MonoBehaviour
{
    private RandomPieceGeneratorScript randPieceScript;

    private string[] generatedPiece;

    public int bpm = 60;
    public int currentNoteIndex = 0;

    [SerializeField] Transform nbHolder;
    Transform nb0,nb1, nb2, nb3, nb4;

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
    // Start is called before the first frame update
    void Start()
    {
        nb0 = nbHolder.GetChild(0);
        nb1 = nbHolder.GetChild(1);
        nb2 = nbHolder.GetChild(2);
        nb3 = nbHolder.GetChild(3);
        nb4 = nbHolder.GetChild(4);
        generatedPiece = randPieceScript.generatedPiece;

        noteblocks = new Transform[5] { nb0, nb1, nb2, nb3, nb4 };


        //StartCoroutine(tempAutomaticProceedCarousel());
    }

    //FIXEDUPDATE VARS
    const float tempSpeed = 0.0001f;
    const int cps = 50;//fixedUpdate has 50 calls per second (50cps)
    void FixedUpdate()
    {
        
        for (int i = 0; i < cps * 30; i++){
            const float nbDistance = 1.1f;
            // I want to make the blocks move the blocks one "space" (= nb2 gets nb2's position) in bpm / 60f seconds
            float unitsToMove = nbDistance * tempSpeed / cps;

            Vector3 moveVector = new Vector3(-unitsToMove, 0f, 0f);
            
            foreach(Transform t in noteblocks)
            {
                t.Translate(moveVector);
                //if(t.)
            }
        }
    }

    IEnumerator tempAutomaticProceedCarousel()
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

    }

    public Sprite getCurrentNoteSprite()
    {
        string strCurrentNote = generatedPiece[currentNoteIndex];
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


    // Update is called once per frame
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
}
