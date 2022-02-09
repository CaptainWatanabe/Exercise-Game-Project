using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] GameObject FinishedGameUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(FinishedGameUI);
            
            if(PlayerPrefs.GetFloat(STRINGREF.SAVE_TIMER_COUNT + inGameTracker.trackerInstance.currLevel) == 0 || 
                inGameTracker.trackerInstance.timer < PlayerPrefs.GetFloat(STRINGREF.SAVE_TIMER_COUNT + inGameTracker.trackerInstance.currLevel))
                
                PlayerPrefs.SetFloat(STRINGREF.SAVE_TIMER_COUNT + inGameTracker.trackerInstance.currLevel, inGameTracker.trackerInstance.timer);
            
            if(inGameTracker.trackerInstance.stepCounter > PlayerPrefs.GetInt(STRINGREF.SAVE_STEP_COUNT + inGameTracker.trackerInstance.currLevel))
                PlayerPrefs.SetInt(STRINGREF.SAVE_STEP_COUNT + inGameTracker.trackerInstance.currLevel, inGameTracker.trackerInstance.stepCounter);
            
            if(inGameTracker.trackerInstance.currentCoins > PlayerPrefs.GetInt(STRINGREF.SAVE_COINS_COUNT + inGameTracker.trackerInstance.currLevel))
                PlayerPrefs.SetInt(STRINGREF.SAVE_COINS_COUNT + inGameTracker.trackerInstance.currLevel, inGameTracker.trackerInstance.currentCoins);

            inGameTracker.trackerInstance.gameState = GameState.Stop;
            inGameTracker.trackerInstance.isGameFinished = true;

            
        }
    }
}
