using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GM2RoundProcedureScript : MonoBehaviour
{
    //SCRIPTS
    public NoteCarouselScript isaNoteCarouselScript;
    public NoteCarouselScript matNoteCarouselScript;

    public RandomPieceGeneratorScript randomPieceGeneratorScript;

    public GM2OptionsScript optionsScript;

    //INTS 
    public int bpmIncreaseAmount;
    public int bpm;
    private int currentNoteIndex;

    //BOOLS
    public bool inIntermission = false;

    //GAMEOBJECTS
    public GameObject isaNBHolderPrefab;
    public GameObject matNBHolderPrefab;

    GameObject isaNBHolder;
    GameObject matNBHolder;

    // Start is called before the first frame update
    void Start()
    {
        assignNBHolders();
        bpm = optionsScript.startingBPM;
    }

    // Update is called once per frame
    void Update()
    {
        currentNoteIndex = isaNoteCarouselScript.currentNoteIndex;
        checkForLastNoteOfPiece();
    }

    //THIS IS THE METHOD THAT STARTS THE COROUTINE
    public void checkForLastNoteOfPiece()
    {
        if (currentNoteIndex >= RandomPieceGeneratorScript.generatedPiece.Count && !inIntermission)
        {
            //StartCoroutine(doNextRoundProcedure());
            doNextRoundProcedure();
            inIntermission = true;
        }
    }


    private void OnDestroy()
    {
        Debug.Log("Farewell. The gamemanager is going to heaven");
    }

    public void doNextRoundProcedure()
    {
        Debug.Log("Starting the next round (hopefully) :ppppp");

        isaNoteCarouselScript.noteblocks = null;
        matNoteCarouselScript.noteblocks = null;

        Debug.Log("Mat nb holder path: " +matNBHolder);
        Debug.Log("Isa nb holder path:" + isaNBHolder);

        Destroy(isaNBHolder);
        Destroy(matNBHolder);

        Debug.Log("Isa's NBHolder destroyed?" + isaNBHolder.IsDestroyed());
        Debug.Log("Mat's NBHolder destroyed?" + matNBHolder.IsDestroyed());

        Debug.Log("Ready to instantiate the NBHolder prefabs");

        //yield return null;

        isaNBHolder = Instantiate(isaNBHolderPrefab);
        matNBHolder = Instantiate(matNBHolderPrefab);

        assignNBHolders();
        assignNBScripts();

        IncreaseBPM();

        inIntermission = false;

    }

    private void IncreaseBPM()
    {
        bpm += optionsScript.BPMIncrease;
        isaNoteCarouselScript.bpm = bpm;
        matNoteCarouselScript.bpm = bpm;
    }

    private void assignNBHolders()
    {
        isaNBHolder = GameObject.Find("NotenBovenIsaHolder");
        matNBHolder = GameObject.Find("NotenBovenMatHolder");
    }
    private void assignNBScripts()
    {
        isaNoteCarouselScript = isaNBHolder.GetComponent<NoteCarouselScript>();
        matNoteCarouselScript = matNBHolder.GetComponent<NoteCarouselScript>();
    }

}
