using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCarouselScript : MonoBehaviour
{
    private RandomPieceGeneratorScript randPieceScript;

    private string[] generatedPiece;

    // Start is called before the first frame update
    void Start()
    {
        generatedPiece = randPieceScript.generatedPiece;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
