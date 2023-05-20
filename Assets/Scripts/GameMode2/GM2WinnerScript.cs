using System.Collections;
using UnityEngine;

public class GM2WinnerScript : MonoBehaviour
{

    //TEXT
    [SerializeField] GameObject winnerHeader;
    //[SerializeField] GameObject matWinText;
    //[SerializeField] GameObject isaWinText;

    //OTHER UI
    [SerializeField] GameObject WinnerContinueHolder;
    [SerializeField] GameObject isaHeartHolder;
    [SerializeField] GameObject matHeartHolder;
    [SerializeField] GameObject matNoteBlocks;
    [SerializeField] GameObject isaNoteBlocks;

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
    [SerializeField] AudioSource bgMusicAudioSrc;
    [SerializeField] AudioSource victoryAudioSrc;
    [SerializeField] float victorySoundDelay;
    [SerializeField] float fadeOutStrength;
    [SerializeField] AudioClip victoryAudioClip;


    public void doMatWinSequence()
    {
        //set matisse's sprite to one with a crown
        matSpriteRenderer.sprite = matisseWinSprite;
        //play the matisse win animation
        matAnimator.applyRootMotion = false;
        matAnimator.Play("matisseWinsGM2");
        
        doUniversalWinSequenceTasks();
    }

    public void doIsaWinSequence()
    {
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
        // invoke victory sound
        Invoke("playVictorySound", victorySoundDelay);
        //Enable texts
        winnerHeader.SetActive(true);
        matHeartHolder.SetActive(false);
        isaHeartHolder.SetActive(false);
        matNoteBlocks.SetActive(false);
        isaNoteBlocks.SetActive(false);
        isaBW.SetActive(false);
        matBW.SetActive(false);
        //WinnerContinueHolder.SetActive(true);
        //isaWinText.SetActive(true);
    }

    public void playVictorySound()
    {
        victoryAudioSrc.PlayOneShot(victoryAudioClip);
    }

    private IEnumerator fadeOutBGMusic()
    {
        while (bgMusicAudioSrc.volume > 0f)
        {
            bgMusicAudioSrc.volume -= fadeOutStrength;
            yield return new WaitForSeconds(0.05f);
        }
        //yield return null;
    }
}
