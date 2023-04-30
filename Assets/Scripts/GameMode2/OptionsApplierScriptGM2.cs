using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsApplierScriptGM2 : MonoBehaviour
{
    public static bool doCameraSway;
    public static int startingBPM;

    // Start is called before the first frame update
    private void Awake()
    {
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
