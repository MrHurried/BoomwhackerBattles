using System.Collections;
using TMPro;
using UnityEngine;


namespace BoomWhackerBattles
{

    public class BoomWhackerScript : MonoBehaviour
    {
        //TEXT OBJECTS
        [SerializeField] TextMeshProUGUI IsaScoreText; //Defines the text GameObject that holds Isabel's score
        [SerializeField] TextMeshProUGUI MatScoreText; //Defines the text GameObject that holds Matisse's score

        //SPRITE RENDERERS
        [SerializeField] SpriteRenderer IsaSpriteRenderer; //Isabel's head sprite renderer
        [SerializeField] SpriteRenderer MatSpriteRenderer; // Matisse's head sprite renderer
        [SerializeField] SpriteRenderer BWAchterMatSpriteRenderer; //Boomwhacker achter Mat sprite renderer
        [SerializeField] SpriteRenderer BWAchterIsaSpriteRenderer; //Boomwhacker achter Isa Sprite Rendererer

        //SRITES
        [SerializeField] Sprite IsaWhackedSprite; //Isabel Whacked sprite
        [SerializeField] Sprite MatWhackedSprite; // Matisse Whacked sprite
        [SerializeField] Sprite IsaNotWhackedSprite; //Normal Isa sprite
        [SerializeField] Sprite MatNotWhackedSprite; // Normal Matisse sprite
        [SerializeField] Sprite BoomwhackerSwingEffectSprite; //Boomwhacker Swing Sprite
        [SerializeField] Sprite BWSprite; //BoomwhackerSprite

        //PARTICLE RELATED
        [SerializeField] ParticleSystem MatBloodParticleSystem; //Defines Matisse's blood particle sys
        [SerializeField] ParticleSystem IsaBloodParticleSystem; //Defines Isabel's blood particle sys

        //INTEGERS
        [SerializeField] int BloodBonusPoints; //defines how many extra points the blood effect should give

        [SerializeField] int MaxScore; // score until win
        [SerializeField] int cheatMultiplier; //used for debugging, making score go up faster

        public int IsaScore = 0; //tracks score for Isabel
        public int MatScore = 0; //Tracks score for Matisse

        public int IsaScoreSinceLastBlood = 0; //Tracks the score of Isabel since matisse last bled
        public int MatScoreSinceLastBlood = 0; //Tracks the score of Matisse since Isabel last bled

        [SerializeField] int HitsUntilBloodMat; //Defines how many hits it should take to trigger the blood effect FOR MATISSE
        [SerializeField] int HitsUntilBloodIsa; //Defines how many hits it should take to trigger the blood effect FOR MATISSE

        //AUDIO RELATED
        private AudioSource AudioSrc; //Defines the audio source on THIS gameobject (gamemanager)
        [SerializeField] AudioClip BonkClip; //Bonk sound effect :p
        [SerializeField] AudioClip BloodClipV1; //blood sound effect :o

        //BOOLS
        public bool hasWon = false; //this defines wether someone has won already

        public bool CR_WhackMatisse_isPlaying = false; //Used to tell if the whack matisse animation is playing
        public bool CR_WhackIsabel_isPlaying = false; //Used to tell if the whack matisse animation is playing

        public bool doFlashAnimation = false;
        public bool doSquashAnimation = true;

        //this func ASSIGNS:
        //AudioSource on this gameobject
        void Start()
        {
            hasWon = false;

            AudioSrc = GetComponent<AudioSource>();

            doFlashAnimation = GameOptionsStorer.doFlashes;
            MaxScore = GameOptionsStorer.pointsToWin;
        }

        void Update()
        {
            ProcessInput();
            CheckForWinner();
        }

        private void ProcessInput()
        {
            //ISABEL INPUT
            if (Input.GetKeyDown(KeyCode.P))
            {
                WhackMatisse();
            }

            //MATISSE INPUT
            if (Input.GetKeyDown(KeyCode.Q))
            {
                WhackIsabel();
            }
        }

        private void WhackMatisse() // P key
        {
            if (doFlashAnimation == true)
                MatSpriteRenderer.gameObject.SendMessage("Flash");

            if (doSquashAnimation == true)
                MatSpriteRenderer.gameObject.SendMessage("Squash");

            //Play the bonk sound effect
            AudioSrc.PlayOneShot(BonkClip);

            //1: check if the ParticleSystem Isn't playing
            //2: Check if Isabel has hit [hitsuntilblood] (default:10)
            //          Every 10 hits, this will become true
            //          that's because any number times 10 can be divided
            //              by an rest division and will have a remainder of 0
            //          10*7 = 70 ; 70%10 = 0 because 10 fits exactly 7 times within 70

            IsaScoreSinceLastBlood++; ;

            if (IsaScoreSinceLastBlood > 0)
            {
                if (!MatBloodParticleSystem.isPlaying && IsaScoreSinceLastBlood % HitsUntilBloodMat == 0)
                {
                    //Maak de volgende HitsUntilBlood random
                    HitsUntilBloodMat = Mathf.RoundToInt(Random.Range(1f, 20f));

                    //geef bonuspunten
                    IsaScore += BloodBonusPoints;

                    //play the audio
                    AudioSrc.PlayOneShot(BloodClipV1);

                    //play the particle system
                    MatBloodParticleSystem.Play();

                    //reset the ScoreSinceLastBlood to 0, because we just did the blood effect
                    IsaScoreSinceLastBlood = 0;
                }

            }
            //Add 1 to score and update text
            IsaScore += 1 * cheatMultiplier;
            IsaScoreText.text = IsaScore.ToString();


            //If the animation isn't arlready playing, play it for matisse (isMatisse = true)
            if (!CR_WhackMatisse_isPlaying) StartCoroutine(DoWhackAnimationMatisse());
        }

        private void WhackIsabel() // Q key
        { 

            if (doFlashAnimation == true)
                IsaSpriteRenderer.gameObject.SendMessage("Flash");

            if (doSquashAnimation == true)
                IsaSpriteRenderer.gameObject.SendMessage("Squash");

            //Play the bonk sound effect
            AudioSrc.PlayOneShot(BonkClip);

            //1: check if the ParticleSystem Isn't playing
            //2: Check if Isabel has hit [hitsuntilblood] (default:10)
            //          Every 10 hits, this will become true
            //          that's because any number times 10 can be divided
            //              by an rest division and will have a remainder of 0
            //          10*7 = 70 ; 70%10 = 0 because 10 fits exactly 7 times within 70

            MatScoreSinceLastBlood++; ;

            if (MatScoreSinceLastBlood > 0)
            {
                if (!IsaBloodParticleSystem.isPlaying && MatScoreSinceLastBlood % HitsUntilBloodIsa == 0)
                {
                    //Maak de volgende HitsUntilBlood random
                    HitsUntilBloodIsa = Mathf.RoundToInt(Random.Range(1f, 20f));

                    //geef bonuspunten
                    MatScore += BloodBonusPoints;

                    //play the audio
                    AudioSrc.PlayOneShot(BloodClipV1);

                    //play the particle system
                    IsaBloodParticleSystem.Play();

                    //reset the ScoreSinceLastBlood to 0, because we just did the blood effect
                    MatScoreSinceLastBlood = 0;
                }

            }
            //Add 1 to score and update text
            MatScore += 1 * cheatMultiplier;
            MatScoreText.text = MatScore.ToString();


            //If the animation isn't arlready playing, play it for matisse (isMatisse = true)
            if (!CR_WhackIsabel_isPlaying) StartCoroutine(DoWhackAnimationIsabel());
        }

        IEnumerator DoWhackAnimationMatisse()
        {
            //Tell Everyone that the animation is playing
            CR_WhackMatisse_isPlaying = true;

            //Change BW sprite to swing Sprite
            BWAchterMatSpriteRenderer.sprite = BoomwhackerSwingEffectSprite;

            //Change sprite to me whacked
            MatSpriteRenderer.sprite = MatWhackedSprite;

            //Wait for two seconds
            yield return new WaitForSeconds(.2f);

            //Change BW to normal BW sprite
            BWAchterMatSpriteRenderer.sprite = BWSprite;

            //Change sprite to normal again
            MatSpriteRenderer.sprite = MatNotWhackedSprite;

            //tell everyone that the animation is stopped
            CR_WhackMatisse_isPlaying = false;
        }

        IEnumerator DoWhackAnimationIsabel()
        {
            //Tell Everyone that the animation is playing
            CR_WhackIsabel_isPlaying = true;

            //Change BW sprite to swing Sprite
            BWAchterIsaSpriteRenderer.sprite = BoomwhackerSwingEffectSprite;

            //Change sprite to me whacked
            IsaSpriteRenderer.sprite = IsaWhackedSprite;

            //Wait for two seconds
            yield return new WaitForSeconds(.2f);

            //Change BW to normal BW sprite
            BWAchterIsaSpriteRenderer.sprite = BWSprite;

            //Change sprite to normal again
            IsaSpriteRenderer.sprite = IsaNotWhackedSprite;

            //tell everyone that the animation is stopped
            CR_WhackIsabel_isPlaying = false;
        }

        private void CheckForWinner()
        {
            if (hasWon) return;
            //ISABEL WINT

            if (IsaScore >= MaxScore)
            {
                Debug.Log("Isabel WINT!!!");
                GetComponent<WinnerScript>().doIsaWinSequence();
                hasWon = true;
            }
            //MATISSE WINT

            if (MatScore >= MaxScore)
            {
                Debug.Log("Matisse WINT!!!");
                GetComponent<WinnerScript>().doMatWinSequence();
                hasWon = true;
            }
        }
    }

}
