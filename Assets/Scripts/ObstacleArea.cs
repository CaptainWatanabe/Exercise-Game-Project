using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleArea : MonoBehaviour
{
    [SerializeField] GameObject UINotifPrefab;
    [SerializeField] string notifText;

    GameObject UiNotifObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UiNotifObject = Instantiate(UINotifPrefab);
            UiNotifObject.GetComponentInChildren<TMP_Text>().text = notifText;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(UiNotifObject);
        }
    }
}
