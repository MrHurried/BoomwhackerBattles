using System.Collections;
using UnityEngine;
using TMPro;
using BoomWhackerBattles;
public class ThreeTwoOneGoScript : MonoBehaviour
{
    private TextMeshProUGUI textGameObject;
    private Animator animator;

    [SerializeField] private BoomWhackerScript bwScript;


    //USED FOR AUDIO FADEIN
    [SerializeField] AudioSource bgMusicAudioSrc;
    [SerializeField] float fadeInStrength;

    // Start is called before the first frame update
    void Start()
    {
        if (bgMusicAudioSrc != null && bwScript != null)
        {
            bgMusicAudioSrc.volume = 0f;
            bgMusicAudioSrc.Pause();

            bwScript.enabled = false;
        }


        animator = GetComponent<Animator>();
        textGameObject = GetComponent<TextMeshProUGUI>();

        StartCoroutine(ChangeTextCoroutine());
    }

    IEnumerator ChangeTextCoroutine()
    {
        animator.SetTrigger("StartCountDownAnimation");

        textGameObject.text = "3";
        yield return new WaitForSeconds(1f);
        textGameObject.text = "2";
        yield return new WaitForSeconds(1f);
        textGameObject.text = "1";
        yield return new WaitForSeconds(1f);
        textGameObject.text = "GO!";


        if (bgMusicAudioSrc != null && bwScript != null)
        {
            bwScript.enabled = true;
            bgMusicAudioSrc.UnPause();
            StartCoroutine(fadeInBGMusic());
        }

        yield return new WaitForSeconds(1.3f);
        textGameObject.text = "";
    }

    IEnumerator fadeInBGMusic()
    {
        while (bgMusicAudioSrc.volume < 1f)
        {
            bgMusicAudioSrc.volume += fadeInStrength;
            Debug.Log("inside fadeinBGMusic's while loop");
            yield return new WaitForSeconds(0.05f);
        }
        //yield return null;
    }

}
