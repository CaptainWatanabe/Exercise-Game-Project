using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;

public class InGameInput: MonoBehaviour
{
    public static InGameInput inputInstance;
    GameInput gameInput;

    public UnityAction onSwingLeft;
    public UnityAction onSwingRight;
    public UnityAction onSwingUp;
    public UnityAction onSwingDown;

    public float runningPower;
    int counter = 0;
    bool isSwingRight = false;
    bool isSwingLeft = false;
    bool isSwingUp = false;
    bool isSwingDown = false;

    [SerializeField] TMP_Text debugText;

    private void Awake()
    {
        if (inputInstance == null)
            inputInstance = this;
        else
            Destroy(this);

        gameInput = new GameInput();        
        
    }
    private void OnEnable()
    {
        gameInput.Enable();

        if (StepCounter.current != null)
        {
            InputSystem.EnableDevice(StepCounter.current);
           // debugText.text = "YES";
        }
        else
        {
            Debug.LogWarning("No Step Counter Censor");
            //debugText.text = "NO!";
        }
        if(LinearAccelerationSensor.current != null)
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        else
            Debug.LogWarning("No Accelerometer Censor");
        //InputSystem.EnableDevice(AttitudeSensor.current);
        //InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }

    private void OnDisable()
    {
        if(StepCounter.current != null)
            InputSystem.DisableDevice(StepCounter.current);
        if (LinearAccelerationSensor.current != null)
            InputSystem.DisableDevice(LinearAccelerationSensor.current);
        //InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);
        //InputSystem.DisableDevice(AttitudeSensor.current);
        gameInput.Disable();
    }

    void Update()
    {
        StepCounting();
        RotatingBody(); 
    }

    void StepCounting()
    {
        debugText.text = StepCount().ToString();
        
        if (RotateBody().y <= -0.22f)
        {
            if (runningPower >= 5)
                runningPower = 5;
            else
                runningPower += 0.25f;
        }
        else if (counter != StepCount())
        {
            if (runningPower >= 5)
                runningPower = 5;
            else
                runningPower += 0.4f;

            counter = StepCount();
        }

        //debugText.text = gameInput.InGame.StepCounter.ReadValue<int>().ToString();

        if (runningPower > 0)
            runningPower -= Time.fixedDeltaTime;
        else
            runningPower = 0;
    }

    void RotatingBody()
    {
        if (RotateBody().x >= 0.5f && RotateBody().z <= -0.5f)
        {
            //accelText.text = "Right";
            if (!isSwingRight)
            {
                onSwingRight?.Invoke();
                isSwingRight = true;
            }
            isSwingLeft = false;

        }
        else if (RotateBody().x <= -0.5f && RotateBody().z <= -0.5f)
        {
            //accelText.text = "Left";
            if (!isSwingLeft)
            {
                onSwingLeft?.Invoke();
                isSwingLeft = true;
            }
            isSwingRight = false;
        }
        else if (RotateBody().y >= 0.9f)
        {
            //accelText.text = "Down!";
            if (!isSwingDown)
            {
                onSwingDown?.Invoke();
                isSwingDown = true;
            }
            isSwingUp = false;
        }
        else if (RotateBody().y <= -0.9f)
        {
            // accelText.text = "Up!";
            if (!isSwingUp)
            {
                onSwingUp?.Invoke();
                isSwingUp = true;
            }
            isSwingDown = false;
        }
        //attitudeText.text = RotateBody().ToString();
        ResetSwingInput();
    }

    public int StepCount()
    {
        return gameInput.InGame.StepCounter.ReadValue<int>();
    }

    public Vector3 RotateBody()
    {
        return gameInput.InGame.AccelerationCounter.ReadValue<Vector3>();
    }

    public void ResetSwingInput()
    {
        isSwingRight = false;
        isSwingLeft = false;
        isSwingUp = false;
        isSwingDown = false;
    }
}
