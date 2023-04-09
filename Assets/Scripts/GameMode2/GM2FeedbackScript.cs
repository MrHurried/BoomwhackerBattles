using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2FeedbackScript : MonoBehaviour
{
    //references the correctinputanim animator
    public Animator isaAnimator; 
    public Animator matAnimator;

    public SpriteRenderer matBtnFeedback;
    public SpriteRenderer isaBtnFeedback;

    void Start()
    {
        matBtnFeedback.enabled = false;
        isaBtnFeedback.enabled = false;
    }

    void Update()
    {
        giveButtonFeedback();
    }

    public void giveCorrectInputFeedback(bool fromIsa, bool didCorrectInput)
    {
        if(fromIsa)
        {
            isaAnimator.Play("CorrectInputAnim");
            //isaAnimator.SetBool("didCorrectInput", true);
        }
        else
        {
            isaAnimator.SetBool("didCorrectInput", true);
        }
    }

    private void giveButtonFeedback()
    {
        //ISABEL
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isaBtnFeedback.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            isaBtnFeedback.enabled = false;
        }
        //MATISSE
        if(Input.GetKeyDown(KeyCode.P))
        {
            matBtnFeedback.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            matBtnFeedback.enabled = false;
        }
    }
}
