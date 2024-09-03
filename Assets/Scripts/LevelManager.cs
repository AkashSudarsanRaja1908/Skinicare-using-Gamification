using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private static LevelManager _instance;
    private int _framePaused = 0;
    private int _frameStart = 0;
    private CategoryButton _activeButton;

    public GameObject player;
    public Text score;
    public float PlayerScore;
    public bool isPaused;
    public int coinCount = 0;
    public Text txtCoin;
    public Image imgCoinProgress;
    public Text txtMultiplier;
    public int multiplier=1;
    public float fillAmount = 10;
    public int heartCount = 0;
    public Text txtHeart;
    public Text txtContinueHeart;
    public int distance = 0;
    public int distanceFactor = 200;
    public Text txtDistance;
    public Animator distanceAnimator;
    public float AnimSpeed = 1f;
    public GameObject GameOverUI;
    public ScrollRect achvScrollbar;
    public GameObject PlayerCanvas;
    public AudioClip mainTheme, hitSFX, endTheme;

    public Text txtCollectedCoins, txtHusamCoins, txtAzalCoins;
    public int husamRequiredCoins = 200, azalRequiredCoins = 100;
    public GameObject btnHusam, coinsHusam, characterHusam;
    public GameObject btnAzal, coinsAzal, characterAzal;
    public bool hasMagnet = false;

    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
            }
            return _instance;
        }
    }

	// Use this for initialization
    void Start()
    {
        _activeButton = GameObject.Find("BtnCurrentPlayer").GetComponent<CategoryButton>();
        _activeButton.Switch();
        PlayerScore = 0;
        isPaused = false;
        // Players 
        txtCollectedCoins.text = PlayerPrefs.GetInt("CollectedCoins").ToString();
        txtHusamCoins.text = husamRequiredCoins.ToString();
        txtAzalCoins.text = azalRequiredCoins.ToString();
        int husam = PlayerPrefs.GetInt("husam");
        btnAzal.GetComponent<CharacterUnlock>().Activate();
        if (husam == 1) // 0 -> locked  1 -> unlocked  2 -> active
        {
            btnHusam.GetComponent<CharacterUnlock>().Unlock();
            coinsHusam.SetActive(false);
            coinsAzal.SetActive(false);
            characterHusam.SetActive(false);
        }
        else if (husam == 2)
        {
            btnHusam.GetComponent<CharacterUnlock>().Activate();
            btnAzal.GetComponent<CharacterUnlock>().Unlock();
            coinsHusam.SetActive(false);
            coinsAzal.SetActive(false);
            characterAzal.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("CollectedCoins") >= husamRequiredCoins)
            {
                btnHusam.GetComponent<CharacterUnlock>().Unlock();
                coinsHusam.SetActive(false);
                coinsAzal.SetActive(false);
                characterHusam.SetActive(false);
                PlayerPrefs.SetInt("husam", 1);
                PlayerPrefs.SetInt("CollectedCoins", PlayerPrefs.GetInt("CollectedCoins") - husamRequiredCoins);

            }
            else
            {
                coinsAzal.SetActive(false);
                characterHusam.SetActive(false);
            }
        }
        UpdateAnimator();
 

    }
  

	// Update is called once per frame
    void Update()
    {
        ScoreCalculation();
        CoinCount();
        HeartCount();
    }

    private void UpdateAnimator()
    {
        for (int i = 0; i < player.gameObject.transform.childCount; i++)
        {
            if (player.gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                player.GetComponent<Animator>().avatar = 
                    player.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().avatar;
            }
        }
    }

    public void CharacterActivate(bool isHusam)
    {
        if (isHusam)
        {
            if (PlayerPrefs.GetInt("husam") == 1)
            {
                PlayerPrefs.SetInt("husam", 2);
                btnHusam.GetComponent<CharacterUnlock>().Activate();
                btnAzal.GetComponent<CharacterUnlock>().Unlock();
                characterAzal.SetActive(false);
                characterHusam.SetActive(true);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("husam") != 0)
            {
                PlayerPrefs.SetInt("husam", 1);
                btnAzal.GetComponent<CharacterUnlock>().Activate();
                btnHusam.GetComponent<CharacterUnlock>().Unlock();
                characterHusam.SetActive(false);
                characterAzal.SetActive(true);
            }
        }
        UpdateAnimator();
    }
    private void HeartCount()
    {
        txtHeart.text = heartCount.ToString();
    }

    private void CoinCount()
    {
        txtCoin.text = coinCount.ToString();
        txtMultiplier.text = multiplier.ToString();
        //Multiplier Progress 
        if (imgCoinProgress.fillAmount == 1)
        {
            imgCoinProgress.fillAmount = 0;
            multiplier++;
        }
        else
        {
            imgCoinProgress.fillAmount = (coinCount - (fillAmount * (multiplier - 1))) / fillAmount;
        }
    }

    private void ScoreCalculation()
    {
        if (player.GetComponent<PlayerController>().LevelStart && !isPaused && !player.GetComponent<PlayerController>().IsDead)
        {
            PlayerScore = (Time.frameCount - _framePaused - _frameStart) / 5f; 
            score.text = ((int) PlayerScore).ToString();
            // Distance calculation 
            if (PlayerScore % distanceFactor == 0)
            {
                distance += 100;
                txtDistance.text = distance + " m";
                StartCoroutine(ShowDistance());
                // Increasing the velocity 
                player.GetComponent<PlayerController>().fVelocity += 2;
                if( AnimSpeed<2)
                    AnimSpeed += 0.1f; 
                player.GetComponent<Animator>().SetFloat("AnimSpeed", AnimSpeed);  
            }
        }
        else if (isPaused)
        {
            _framePaused++;
        }

        else if (player.GetComponent<PlayerController>().LevelStart == false)
        {
            _frameStart++;
        }
    }

    private IEnumerator ShowDistance()
    {
        distanceAnimator.SetTrigger("Show");
        yield return new WaitForSeconds(2);
        distanceAnimator.SetTrigger("Hide");
    }
    public void KillPlayer(GameObject enemy)
    {        
        // SFX 
        gameObject.GetComponent<AudioSource>().PlayOneShot(hitSFX, 1);
        //Save Highest score 
        SaveScore();
        player.GetComponent<PlayerController>().IsDead = true;
        player.GetComponent<PlayerController>().movementSettings.forwardInput = 0;
        player.GetComponent<Animator>().SetTrigger("Die");
        // Hide Player Canvas 
        PlayerCanvas.SetActive(false);

        // Update the ContinueHearts
        if (_countinueCount == 1)
        {
            txtContinueHeart.text = (_countinueValue * 2).ToString();
        }
        // Show Game Over Window 
        GameOverUI.GetComponent<Animator>().SetTrigger("Show");
        // Save Coins 
        SaveCoins();
        // Save Hearts 
        SaveHearts();
        // Save Highest Multiplier
        SaveMultiplier();
        _enemy = enemy;

        //Change the Audio Clip 
        gameObject.GetComponent<AudioSource>().clip = endTheme;
        gameObject.GetComponent<AudioSource>().Play();
        // Ad
        GameObject.Find("AdsManager").GetComponent<AdsManager>().RequestInterstitialAd();
    }

    private GameObject _enemy;
    private int _countinueCount = 0;
    private int _countinueValue = 1;

    public void GameContinue()
    {
        if (_countinueCount == 0)
        {
            _countinueCount = 1;
        }
        else
        {
            _countinueValue = _countinueValue * 2;
        }

        if (heartCount >= _countinueValue)
        {
            GameObject.Destroy(_enemy);
            //Change the Audio Clip 
            gameObject.GetComponent<AudioSource>().clip = mainTheme;
            gameObject.GetComponent<AudioSource>().Play();

            player.GetComponent<PlayerController>().IsDead = false;
            player.GetComponent<PlayerController>().movementSettings.forwardInput = 1;
            player.GetComponent<Animator>().SetTrigger("Run");
            // Show Player Canvas 
            PlayerCanvas.SetActive(true);
            // Hide Game Over Window 
            GameOverUI.GetComponent<Animator>().SetTrigger("Hide");
            // Update the CollectedHearts
            heartCount = heartCount - _countinueValue;

        }
        else
        {
            Menus.instance.ShowAchievements();
        }


    }

    private void SaveHearts()
    {
        int CollectedHearts = PlayerPrefs.GetInt("CollectedHearts");
        CollectedHearts = heartCount + CollectedHearts;
        PlayerPrefs.SetInt("CollectedHearts", CollectedHearts);
    }

    private void SaveCoins()
    {
        int CollectedCoins = PlayerPrefs.GetInt("CollectedCoins");
        CollectedCoins = coinCount + CollectedCoins;
        PlayerPrefs.SetInt("CollectedCoins", CollectedCoins);
    }
    private void SaveScore()
    {
        int HighestScore = PlayerPrefs.GetInt("HighestScore");
        if (PlayerScore > HighestScore)
        {
            PlayerPrefs.SetInt("HighestScore", (int)PlayerScore);
        }
    }
    private void SaveMultiplier()
    {
        int HighestMultiplier = PlayerPrefs.GetInt("HighestMultiplier");
        if (multiplier > HighestMultiplier)
        {
            PlayerPrefs.SetInt("HighestMultiplier", (int)multiplier);
        }
    }

    public void CategorySwitch(CategoryButton btn)
    { // All Players
        btn.Switch(); 
        _activeButton.Switch();
        _activeButton = btn;
        achvScrollbar.content = btn.achievementMenu.GetComponent<RectTransform>();
 
    }

    public void StartGame()
    {
        player.GetComponent<Animator>().SetTrigger("Run");
        player.GetComponent<PlayerController>().LevelStart = true;
        GameObject.Find("MainMenu").SetActive(false);
        PlayerCanvas.SetActive(true);
        //Change the Audio Clip 
        LevelManager.instance.gameObject.GetComponent<AudioSource>().clip = mainTheme;
        LevelManager.instance.gameObject.GetComponent<AudioSource>().Play();
        // Ad
        GameObject.Find("AdsManager").GetComponent<AdsManager>().RequestBannerAd();
    }

    public void Magnet()
    {
        hasMagnet = true;
        StartCoroutine(StopMagnet());

    }

    public IEnumerator StopMagnet()
    {
        yield return new WaitForSeconds(10);
        hasMagnet = false;
    }
}
