using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIFhinshGame : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text bestTimeText;
    [SerializeField] TMP_Text stepCountText;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text bestCoinText;

    [SerializeField] Button restartGameButton;
    [SerializeField] Button exitGame;

    // Start is called before the first frame update
    void Start()
    {
        restartGameButton.onClick.AddListener(() => { SceneManager.LoadSceneAsync("Level 1"); });
        exitGame.onClick.AddListener(() => SceneManager.LoadSceneAsync("Main Menu"));

        timeText.text = "Time Elapsed : " + inGameTracker.trackerInstance.timer.ToString();

        bestTimeText.text = PlayerPrefs.GetFloat(STRINGREF.SAVE_TIMER_COUNT + inGameTracker.trackerInstance.currLevel) <= 0? "Best Time : None" : 
                            "Best Time : " + PlayerPrefs.GetFloat(STRINGREF.SAVE_TIMER_COUNT + inGameTracker.trackerInstance.currLevel).ToString();

        stepCountText.text = "Step Count : " + PlayerPrefs.GetInt(STRINGREF.SAVE_STEP_COUNT + inGameTracker.trackerInstance.currLevel).ToString();
        
        coinText.text = "Coins Collected : " + inGameTracker.trackerInstance.currentCoins.ToString();
        bestCoinText.text = "Best Coins Collected : " + PlayerPrefs.GetInt(STRINGREF.SAVE_COINS_COUNT + inGameTracker.trackerInstance.currLevel).ToString();
    }
}
