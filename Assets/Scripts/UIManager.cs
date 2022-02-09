using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider speedSlider;

    private void Update()
    {
        speedSlider.value = InGameInput.inputInstance.runningPower;
    }
}
