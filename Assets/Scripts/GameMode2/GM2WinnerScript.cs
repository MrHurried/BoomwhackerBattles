using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2WinnerScript : MonoBehaviour
{

    //TEXT
    [SerializeField] GameObject winnerHeader;
    //[SerializeField] GameObject matWinText;
    //[SerializeField] GameObject isaWinText;

    //OTHER UI
    [SerializeField] GameObject WinnerContinueHolder;

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


    public void doMatWinSequence()
    {
        //set matisse's sprite to one with a crown
        matSpriteRenderer.sprite = matisseWinSprite;
        //play the matisse win animation
        matAnimator.Play("MatisseWins");
        // invoke victory sound
        Invoke("playVictorySound", victorySoundDelay);
        //Enable texts and button
        winnerHeader.SetActive(true);
        //WinnerContinueHolder.SetActive(true);
        //matWinText.SetActive(true);
    }

    public void doIsaWinSequence()
    {
        //set matisse's sprite to one with a crown
        isaSpriteRenderer.sprite = isabelWinSprite;
        //play the matisse win animation
        isaAnimator.Play("IsabelWins");
        // invoke victory sound
        Invoke("playVictorySound", victorySoundDelay);
        //Enable texts
        winnerHeader.SetActive(true);
        //WinnerContinueHolder.SetActive(true);
        //isaWinText.SetActive(true);
    }

    public void playVictorySound()
    {
        victoryAudioSrc.PlayOneShot(victoryAudioClip);
    }
}
