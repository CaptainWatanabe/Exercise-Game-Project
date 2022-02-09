using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelDetailsManager : MonoBehaviour
{

    [SerializeField] int currLevel = 1;
    [SerializeField] TMP_Text bestTimeText;
    [SerializeField] TMP_Text stepCounterText;
    [SerializeField] TMP_Text coinsText;

    void OnEnable()
    {
        bestTimeText.text = PlayerPrefs.GetFloat(STRINGREF.SAVE_TIMER_COUNT + currLevel) <= 0 ? "Your Best Time : None" :
                            "Your Best Time : " + PlayerPrefs.GetFloat(STRINGREF.SAVE_TIMER_COUNT + currLevel).ToString();

        stepCounterText.text = "Last Step Counter : " + PlayerPrefs.GetInt(STRINGREF.SAVE_STEP_COUNT + currLevel).ToString();

        coinsText.text = "Coins Collected : " + PlayerPrefs.GetInt(STRINGREF.SAVE_COINS_COUNT + currLevel).ToString();
    }

    
}
