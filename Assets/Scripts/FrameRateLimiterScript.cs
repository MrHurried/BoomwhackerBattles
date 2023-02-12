using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateLimiterScript : MonoBehaviour
{
    [SerializeField] int targetFPS = 30;
    [SerializeField] bool frameCapEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        if(frameCapEnabled) Application.targetFrameRate = targetFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
