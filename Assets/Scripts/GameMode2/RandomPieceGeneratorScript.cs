using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RandomPieceGeneratorScript : MonoBehaviour
{
    // PIECE GENERATION RELATED
    public static int noteAmount = 50;

    string[] availableNotes = { "2", "4", "8", "16", "r2", "r4", "r8", "r16" };

    public static List<string> generatedPiece = new List<string>();

    //MISC

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
        
    }

    private int pieceGenIndex;
    void GeneratePiece()
    {
        //get all the notes in
        for (int i = 0; i < noteAmount; i++)
        {
            int rnd = Random.Range(0, availableNotes.Length - 1);
            generatedPiece.Add(availableNotes[rnd]);

            AddZeroNotes(getNoteOrRestLength(generatedPiece[generatedPiece.Count-1]));
        }

    }


    //int iPiece represents the current index of the generatedPiece list initialization process
    private void AddZeroNotes(int noteLength)
    {
        int shortestNoteLength = 16;
        int amountOfZeros;
        
        if (noteLength != 0) amountOfZeros = (shortestNoteLength/noteLength) -1;
        else amountOfZeros = 0;
       
        for(int i = 1; i < amountOfZeros+1; i++)
        {
            generatedPiece.Add("0");
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
    }

}
