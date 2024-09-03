using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour {

    public PlayerController player;
    public GameObject PauseMenu;
    public Animator AchievementAnimator;
    public Animator GameOverAnimator;
    public Text CurrentCoins, AllCoins, CurrentHearts, AllHearts, CurrentScore, HighestScore, CurrentMultiplier, HighestMultiplier;
    private static Menus _instance;

    public static Menus instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Menus>();
            }
            return _instance;
        }
    }

    public void PauseGame()
    {
        if (!player.IsDead)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            LevelManager.instance.isPaused = true;
        }
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        LevelManager.instance.isPaused = false;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);
        
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Audio_on_off()
    {
        AudioIcon icon = GameObject.Find("BtnAudio").GetComponent<AudioIcon>();
        icon.SpriteSwitch();
        // Adjusting the volume 
        if (LevelManager.instance.gameObject.GetComponent<AudioSource>().volume == 0)
        {
            LevelManager.instance.gameObject.GetComponent<AudioSource>().volume = 1;  
        }
        else
        {
            LevelManager.instance.gameObject.GetComponent<AudioSource>().volume = 0; // Mute
        }
    }
    public void ShowAchievements()
    {
        GameOverAnimator.SetTrigger("Hide");
        // Get Data 
        CurrentCoins.text = LevelManager.instance.coinCount.ToString();
        CurrentHearts.text = LevelManager.instance.heartCount.ToString();
        CurrentMultiplier.text = LevelManager.instance.multiplier.ToString();
        CurrentScore.text = ((int)LevelManager.instance.PlayerScore).ToString();
        //
        AllCoins.text = PlayerPrefs.GetInt("CollectedCoins").ToString();
        AllHearts.text = PlayerPrefs.GetInt("CollectedHearts").ToString();
        HighestMultiplier.text = PlayerPrefs.GetInt("HighestMultiplier").ToString();
        HighestScore.text = PlayerPrefs.GetInt("HighestScore").ToString();

        AchievementAnimator.SetTrigger("Show");
    }
}
