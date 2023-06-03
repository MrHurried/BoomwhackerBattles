using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BPMScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BPMText;
    [SerializeField] TextMeshProUGUI BPMEqualToSixteenthText;

    public NoteCarouselScript isaNoteCarouselScript;

    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        changeBPMText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeBPMText()
    {
        int bpm = isaNoteCarouselScript.bpm;

        BPMText.text = bpm.ToString() + " BPM"; 
        BPMEqualToSixteenthText.text = "(   = "+ bpm.ToString() + ")";

        animator.SetTrigger("DoGrowAnim");  
    }
}
