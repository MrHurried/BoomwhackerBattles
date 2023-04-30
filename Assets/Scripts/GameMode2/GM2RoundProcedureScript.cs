using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

    }

    public void doNextRoundProcedure()
    {
        Destroy(isaNBHolder);
        Destroy(matNBHolder);

        Instantiate(isaNBHolderPrefab);
        Instantiate(matNBHolderPrefab);

        assignNBHolders();
        assignNBScripts();

        IncreaseBPM();
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

    /*public void doNextRoundProcedure()
    {
        Debug.Log("advancing to next round");

        //randomPieceGeneratorScript.generatePiece();

        randomPieceGeneratorScript.printPiece();


        isaNoteCarouselScript.enabled = false;
        matNoteCarouselScript.enabled = false;

        isaNoteCarouselScript.enabled = true;
        matNoteCarouselScript.enabled = true;

        isaNoteCarouselScript.ResetScript();
        matNoteCarouselScript.ResetScript();

        isaNoteCarouselScript.bpm += bpmIncreaseAmount;
        matNoteCarouselScript.bpm += bpmIncreaseAmount;
        //IMPORTANT: IF THIS DOESN'T WORK, TRY WAITING ONE FRAME


        //Debug.Log("GeneratedPiece: " + RandomPieceGeneratorScript.generatedPiece);

        //CALLED FOR BOTH MAT AND ISA
        /*
        isaNoteCarouselScript.createNoteBlocks();
        matNoteCarouselScript.createNoteBlocks();

        isaNoteCarouselScript.currentNoteIndex = -4; // legacy: -(isaNoteCarouselScript.noteblocksIsa.Length - 1);
        matNoteCarouselScript.currentNoteIndex = -4; //legacy: -(matNoteCarouselScript.noteblocksMat.Length - 1);

        isaNoteCarouselScript.secondsSinceLaunch = 0f;
        matNoteCarouselScript.secondsSinceLaunch = 0f;

        isaNoteCarouselScript.bpm += bpmIncreaseAmount;
        matNoteCarouselScript.bpm += bpmIncreaseAmount;

        clearNoteBlocks();*/
}

/*private void clearNoteBlocks()
{
    foreach(NoteBlock nb in isaNoteCarouselScript.noteblocksIsa)
    {
        nb.revertColorToWhite();
    }

    foreach (NoteBlock nb in matNoteCarouselScript.noteblocksMat)
    {
        nb.revertColorToWhite();
    }
}
}*/
