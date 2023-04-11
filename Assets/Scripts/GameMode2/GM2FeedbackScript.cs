using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2FeedbackScript : MonoBehaviour
{
    public NoteCarouselScript isaNoteCarouselScript;
    public NoteCarouselScript matNoteCarouselScript;

    //references the correctinputanim animator
    public AudioSource audioSource;

    public Animator isaAnimator; 
    public Animator matAnimator;

    public SpriteRenderer matBtnFeedback;
    public SpriteRenderer isaBtnFeedback;

    public Transform isaArrow;
    public Transform matArrow;

    public AudioClip arrowSound;
    
    // Wrong, neutral, correct refer to wrong,... input feedback sprites 
    public Sprite wrongSprite;
    public Sprite neutralSprite;
    public Sprite correctSprite;

    private float arrowStartY = 1.45f;
    private float arrowEndY = 1.11f;

    void Start()
    {
        setStartVariables();
    }

    public void setStartVariables()
    {
        matBtnFeedback.sprite = neutralSprite;
        isaBtnFeedback.sprite = neutralSprite;
    }

    void Update()
    {
        giveButtonFeedback();
    }

    public void giveInputFeedback(bool fromIsa, bool didCorrectInput)
    {
        // I DON'T WANT TO RUN THIS CODE
        // the current implementation is not that good and I think I have better options :p
        return;

        if(fromIsa)
        {
            isaAnimator.Play("Base Layer.CorrectInputAnim");
            //isaAnimator.SetBool("didCorrectInput", true);
        }
        else
        {
            matAnimator.Play("Base Layer.CorrectInputAnim");
        }
    }

    //CODES:
    //-1 = wrong input
    //0 = neutral
    //1 = correct input
    public void changeGradientFeedbackColor(int code, bool fromIsa)
    {
        Debug.Log("changing feedback color lol. code="+code+" fromIsa="+fromIsa);
        if (fromIsa)
        {
            switch (code)
            {
                case -1:
                    isaBtnFeedback.sprite = wrongSprite;
                    break;
                case 0:
                    isaBtnFeedback.sprite = neutralSprite;
                    break;
                case 1:
                    isaBtnFeedback.sprite = correctSprite;
                    break;
            }
        }
        else
        {
            switch (code)
            {
                case -1:
                    matBtnFeedback.sprite = wrongSprite;
                    break;
                case 0:
                    matBtnFeedback.sprite = neutralSprite;
                    break;
                case 1:
                    matBtnFeedback.sprite = correctSprite;
                    break;
            }
        }
    }

    public void changeNBColor(bool fromIsa, bool didCorrectInput, bool isFirstNB)
    {
        if (fromIsa)
        {
            if(isFirstNB)
            {
                if (didCorrectInput) isaNoteCarouselScript.firstNB.adaptColorToFeedback(true);
                else isaNoteCarouselScript.firstNB.adaptColorToFeedback(false);
            }
            else
            {
                if (didCorrectInput) isaNoteCarouselScript.secondNB.adaptColorToFeedback(true);
                else isaNoteCarouselScript.secondNB.adaptColorToFeedback(false);
            }
        }
        else
        {
            if (isFirstNB)
            {
                if (didCorrectInput) matNoteCarouselScript.firstNB.adaptColorToFeedback(true);
                else matNoteCarouselScript.firstNB.adaptColorToFeedback(false);
            }
            else
            {
                if (didCorrectInput) matNoteCarouselScript.secondNB.adaptColorToFeedback(true);
                else matNoteCarouselScript.secondNB.adaptColorToFeedback(false);
            }
        }
    }

    private void giveButtonFeedback()
    {
        //ISABEL
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 desiredPos = new Vector3(isaArrow.localPosition.x, arrowEndY, isaArrow.localPosition.z);
            isaArrow.localPosition  = desiredPos;
            audioSource.PlayOneShot(arrowSound, 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Vector3 desiredPos = new Vector3(isaArrow.localPosition.x, arrowStartY, isaArrow.localPosition.z);
            isaArrow.localPosition = desiredPos;
        }
        //MATISSE
        if(Input.GetKeyDown(KeyCode.P))
        {
            Vector3 desiredPos = new Vector3(matArrow.localPosition.x, arrowEndY, matArrow.localPosition.z);
            matArrow.localPosition = desiredPos;
            audioSource.PlayOneShot(arrowSound, 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            Vector3 desiredPos = new Vector3(matArrow.localPosition.x, arrowStartY, matArrow.localPosition.z);
            matArrow.localPosition = desiredPos;
        }
    }
}
