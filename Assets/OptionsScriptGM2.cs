using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScriptGM2 : MonoBehaviour
{
    public bool doCameraSway;

    // Start is called before the first frame update
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
}
