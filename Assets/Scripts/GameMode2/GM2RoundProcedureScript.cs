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

    public IEnumerator doNextRoundProcedure()
    {

        isaNoteCarouselScript.noteblocks = null;
        matNoteCarouselScript.noteblocks = null;

        Destroy(isaNBHolder);
        Destroy(matNBHolder);


        isaNBHolder = GameObject.Find("NotenBovenIsaHolder");
        while (isaNBHolder != null)
        {
            Debug.Log("Isa's holder still exists :'(");
            isaNBHolder = GameObject.Find("NotenBovenIsaHolder");
        }

        matNBHolder = GameObject.Find("NotenBovenMatHolder");
        while (isaNBHolder != null)
        {
            Debug.Log("Mat's holder still exists :'(");
            matNBHolder = GameObject.Find("NotenBovenMatHolder");
        }

        Debug.Log("Ready to instantiate the NBHolder prefabs");

        isaNBHolder = Instantiate(isaNBHolderPrefab);
        matNBHolder = Instantiate(matNBHolderPrefab);

        assignNBHolders();
        assignNBScripts();

        IncreaseBPM();

        yield return null;
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
