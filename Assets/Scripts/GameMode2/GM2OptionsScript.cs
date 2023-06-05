using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2OptionsScript : MonoBehaviour
{
    //set in the settings menu or, when starting the game in the GM2 scene, set to null
    public static int? options_startingBPM = null;
    public static int? options_BPMIncrease = null;
    public static bool? options_doCameraSway = null;
 
    public int startingBPM = 120;
    public int BPMIncrease = 60;
    public bool doCameraSway = false;

    void Awake()
    {
        options_BPMIncrease = GM2GameOptionsStorer.bpmIncrease;
        options_startingBPM = GM2GameOptionsStorer.startingBpm;
        if(options_doCameraSway != null) options_doCameraSway = GM2GameOptionsStorer.doCameraSway;

        if(options_startingBPM != 0) { startingBPM = (int)options_startingBPM; }
        if (options_BPMIncrease != 0) { BPMIncrease = (int)options_BPMIncrease; }
        if (options_doCameraSway != null) { doCameraSway = (bool)options_doCameraSway; }


        changeStartingBPM();
    }

    void Start()
    {
        //disable or enable the cameraShake
        ChangeCameraShake();

    }

    void ChangeCameraShake()
    {
        if (!doCameraSway)
        {
            GameObject mainCamera = GameObject.Find("Main Camera");
            mainCamera.GetComponent<Animator>().enabled = false;
        }
    }

    public void changeStartingBPM()
    {
        GameObject.Find("NotenBovenIsaHolder").GetComponent<NoteCarouselScript>().bpm = startingBPM;
        GameObject.Find("NotenBovenMatHolder").GetComponent<NoteCarouselScript>().bpm = startingBPM;
    }
}
