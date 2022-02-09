using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MainMenuManager : MonoBehaviour
{
    //#if UNITY_EDITOR
    //    private void Start()
    //    {
    //        PlayerPrefs.SetFloat(STRINGREF.SAVE_TIMER_COUNT + "1", 0);

    //    }
    //#endif

    private void Start()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
#endif
    }

    public void MoveScene(string _sceneName)
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
