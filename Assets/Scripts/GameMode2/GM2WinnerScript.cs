using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM2WinnerScript : MonoBehaviour
{

    //TEXT
    [SerializeField] GameObject winnerHeader;
    [SerializeField] GameObject matWinText;
    [SerializeField] GameObject isaWinText;
    [SerializeField] GameObject BPMText;

    //OTHER UI
    [SerializeField] GameObject continueHolder;
    [SerializeField] GameObject isaHeartHolder;
    [SerializeField] GameObject matHeartHolder;
    public GameObject matNBHolder;
    public GameObject isaNBHolder;

    //SPRITES
    [SerializeField] Sprite matisseWinSprite;
    [SerializeField] Sprite isabelWinSprite;

    [SerializeField] SpriteRenderer matSpriteRenderer;
    [SerializeField] SpriteRenderer isaSpriteRenderer;

    [SerializeField] GameObject isaBW;
    [SerializeField] GameObject matBW;

    //ANIMATIONS
    [SerializeField] Animator matAnimator;
    [SerializeField] Animator isaAnimator;

    //SCORES
    [SerializeField] GameObject ScoreTextsHolder;

    //AUDIO RELATED
    public AudioSource bgMusicAudioSrc; // assigned in ChordSounScript
    [SerializeField] AudioSource victoryAudioSrc;
    [SerializeField] float victorySoundDelay;
    [SerializeField] float fadeOutStrength;
    [SerializeField] AudioClip victoryAudioClip;


    public void doMatWinSequence()
    {
        //enable or disable the [name] texts
        isaWinText.SetActive(false);
        matWinText.SetActive(true);

        //set matisse's sprite to one with a crown
        matSpriteRenderer.sprite = matisseWinSprite;
        //play the matisse win animation
        matAnimator.applyRootMotion = false;
        matAnimator.Play("matisseWinsGM2");
        
        doUniversalWinSequenceTasks();
    }

    public void doIsaWinSequence()
    {
        //enable or disable the [name] texts
        isaWinText.SetActive(true);
        matWinText.SetActive(false);

        //set matisse's sprite to one with a crown
        isaSpriteRenderer.sprite = isabelWinSprite;
        //play the matisse win animation
        isaAnimator.applyRootMotion = false;
        isaAnimator.Play("isabelWinsGM2");
        
        doUniversalWinSequenceTasks();

    }

    //A function to avoid boilerplate code
    public void doUniversalWinSequenceTasks()
    {
        //debug.logging for debugging purposes
        Debug.Log("Doing the universal win sequence");
        //start to fade out the BGMusic
        StartCoroutine(fadeOutBGMusic());
        // invoke victory sound
        Invoke("playVictorySound", victorySoundDelay);
        //Enable texts
        winnerHeader.SetActive(true);
        matHeartHolder.SetActive(false);
        isaHeartHolder.SetActive(false);
        BPMText.SetActive(false);
        matNBHolder.SetActive(false);
        isaNBHolder.SetActive(false);
        isaBW.SetActive(false);
        matBW.SetActive(false);
        continueHolder.SetActive(true);

    }

    public void playVictorySound()
    {
        victoryAudioSrc.PlayOneShot(victoryAudioClip);
    }

    private IEnumerator fadeOutBGMusic()
    { 
        Debug.Log("Fading out the BG music (hopefully). audio src: " + bgMusicAudioSrc);
        while (bgMusicAudioSrc.volume > 0f)
        {
            bgMusicAudioSrc.volume -= fadeOutStrength;
            yield return new WaitForSeconds(0.05f);
        }
        //yield return null;
    }

    public void ContinueToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
