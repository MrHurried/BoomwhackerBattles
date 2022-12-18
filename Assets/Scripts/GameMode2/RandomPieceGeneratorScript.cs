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
        //temp, delete this after testing
        Debug.Log(int.Parse("r2".Replace("r", "")));

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

    void GeneratePiece()
    {
        //get all the notes in
        for (int i = 0; i < noteAmount; i++)
        {
            int rnd = Random.Range(0, availableNotes.Length - 1);
            generatedPiece.Insert( i, availableNotes[rnd]);

            int noteLength = getNoteOrRestLength(generatedPiece[i]);

            int shortestNoteLength = 16;
            int amountOfZeros = shortestNoteLength / noteLength - 1;
            for (int i2 = 1; i < amountOfZeros + 1; i++)
            {
                generatedPiece.Insert(i + i2, "0");
            }

            //AddZeroNotes(getNoteOrRestLength(generatedPiece[i]), i);
        }

    }


    //int iPiece represents the current index of the generatedPiece list initialization process
    private void AddZeroNotes(int noteLength, int iPiece)
    {
        int shortestNoteLength = 16;
        int amountOfZeros =  shortestNoteLength/noteLength -1;
        for(int i = 1; i < amountOfZeros+1; i++)
        {
            generatedPiece.Insert(iPiece + i, "0");
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
