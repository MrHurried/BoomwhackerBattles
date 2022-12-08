using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerScript : MonoBehaviour
{
    //TEXT
    [SerializeField] GameObject winnerHeader;
    [SerializeField] GameObject matWinText;
    [SerializeField] GameObject isaWinText;

    //SPRITES
    [SerializeField] Sprite matisseWinSprite;
    [SerializeField] Sprite isabelWinSprite;

    [SerializeField] SpriteRenderer matSpriteRenderer;
    [SerializeField] SpriteRenderer isaSpriteRenderer;

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
    
    //PURELY FOR TESTING
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) doMatWinSequence();
        if (Input.GetKeyUp(KeyCode.N)) doIsaWinSequence();
    }

    public void doMatWinSequence()
    {
        //fade out the background music
        StartCoroutine(fadeOutBGMusic());
        //hide the score texts
        ScoreTextsHolder.SetActive(false);
        //set matisse's sprite to one with a crown
        matSpriteRenderer.sprite = matisseWinSprite;
        //play the matisse win animation
        matAnimator.Play("MatisseWins");
        // invoke victory sound
        Invoke("playVictorySound", victorySoundDelay);
        //Enable texts
        winnerHeader.SetActive(true);
        matWinText.SetActive(true);
    }

    public void doIsaWinSequence()
    {
        //fade out the background music
        StartCoroutine(fadeOutBGMusic());
        //hide the score texts
        ScoreTextsHolder.SetActive(false);
        //set matisse's sprite to one with a crown
        isaSpriteRenderer.sprite = isabelWinSprite;
        //play the matisse win animation
        isaAnimator.Play("IsabelWins");
        // invoke victory sound
        Invoke("playVictorySound", victorySoundDelay);
        //Enable texts
        winnerHeader.SetActive(true);
        isaWinText.SetActive(true);
    }

    IEnumerator fadeOutBGMusic()
    {
        while (bgMusicAudioSrc.volume > 0f)
        {
            bgMusicAudioSrc.volume -= fadeOutStrength;
            yield return new WaitForSeconds(0.05f);
        }
        //yield return null;
    }

    public void playVictorySound()
    {
       victoryAudioSrc.PlayOneShot(victoryAudioClip);
    }
}
