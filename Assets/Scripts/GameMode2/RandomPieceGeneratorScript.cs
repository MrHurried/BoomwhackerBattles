using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPieceGeneratorScript : MonoBehaviour
{
    // PIECE GENERATION RELATED
    public static int noteAmount = 50;

    string[] availableNotes = { "2", "4", "8", "16", "r2", "r4", "r8", "r16" };

    public static string[] generatedPiece = new string[noteAmount];

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
        for (int i = 0; i < noteAmount; i++)
        {
            int rnd = Random.Range(0, availableNotes.Length - 1);
            generatedPiece[i] = availableNotes[rnd];
        }
    }
    
}
