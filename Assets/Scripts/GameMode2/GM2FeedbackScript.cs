using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2FeedbackScript : MonoBehaviour
{
    //references the correctinputanim animator
    public Animator isaAnimator; 
    public Animator matAnimator;



    void Start()
    {
        
    }

    void Update()
    {
        /*if (isaAnimator.GetBool("didCorrectInput"))
        {
            waitOneFrame(true);
        }
        if (matAnimator.GetBool("didCorrectInput"))
        {
            waitOneFrame(false);
        }*/
    }

    /*IEnumerator waitOneFrame(bool disableIsaAnim)
    {
        if (disableIsaAnim)
        {
            yield return null;
            isaAnimator.SetBool("didCorrectInput", false);
        }
        else
        {
            yield return null;
            matAnimator.SetBool("didCorrectInput", false);
        }
    }*/

    public void doFeedbackProcedure(bool fromIsa, bool didCorrectInput)
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
}
