using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPieceGeneratorScript : MonoBehaviour
{
    // PIECE GENERATION RELATED
    public static int noteAmount = 50;

    public int currentNoteIndex = 0;

    string[] availableNotes = { "2", "4", "8", "16", "r2", "r4", "r8", "r16" };

    string[] generatedPiece = new string[noteAmount];

    //MISC
    public int bpm = 60;

    //NOTE SPRITES
    [SerializeField] Sprite note2;
    [SerializeField] Sprite note4;
    [SerializeField] Sprite note8;
    [SerializeField] Sprite note16;
    [SerializeField] Sprite rest2;
    [SerializeField] Sprite rest4;
    [SerializeField] Sprite rest8;
    [SerializeField] Sprite rest16;

    /// <summary>
    /// triple slash = summary :p
    /// </summary>

    //testing Sprite renderer
    [SerializeField] SpriteRenderer srTest;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePiece();
        string testStr = "";
        foreach( string str in generatedPiece)
        {
            testStr += str + " - ";
        }

        Debug.Log(testStr);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (noteAmount-1 > currentNoteIndex))
        {
            tempProceedeCarousel();
        }

        
    }

    void GeneratePiece()
    {
        for (int i = 0; i < noteAmount; i++)
        {
            int rnd = Random.Range(0, availableNotes.Length - 1);
            generatedPiece[i] = availableNotes[rnd];
        }
    }

    void tempManualProceedCarousel()
    {
        currentNoteIndex++;
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

        srTest.sprite = sprite;
    }

    IEnumerator tempAutomaticProceedCarousel()
    {
        for(; ; )
        {
            int symbolLength = getNoteOrRestLength(generatedPiece[currentNoteIndex]);
            float secondsToWait = (bpm / 60) / (symbolLength / 4);
            yield return new WaitForSeconds(secondsToWait);
            currentNoteIndex++;
        }

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

        //default value in case something goes wrong :p
        return 4;
    }
}
