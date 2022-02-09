using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState { Playing, Stop}

public class inGameTracker : MonoBehaviour
{
    public static inGameTracker trackerInstance;
    public GameState gameState;

    public int currLevel = 1;
    public float timer = 0f;
    public int mainHealth = 3;
    public int currentCoins = 0;
    public int stepCounter = 0;
    public bool isGameStop = false;
    public bool isGameFinished = false;
    int startStepCount = 0;

    [SerializeField] TMP_Text coinText;
  //  [SerializeField] TMP_Text stepText;

    private void Awake()
    {
        if (trackerInstance == null)
            trackerInstance = this;
        else
            Destroy(this.gameObject);       
    }

    private void Start()
    {
        PlayerPrefs.SetInt(STRINGREF.SAVE_STEP_COUNT + 1, 0);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer = (float)Math.Round(timer, 2);

        if (startStepCount == 0)
            startStepCount = InGameInput.inputInstance.StepCount();

        stepCounter = InGameInput.inputInstance.StepCount() - startStepCount;
        
       // stepText.text = stepCounter.ToString();
    }

    public void CoinIncrement()
    {
        currentCoins += 1;
        coinText.text = currentCoins.ToString();
    }

    public void ChangeGameState(int _State)
    {
        switch (_State)
        {
            case 1:
                gameState = GameState.Playing;
                return;
            case 2:
                gameState = GameState.Stop;
                return;
        }
    }

    public void ChageScene(string _sceneName)
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
