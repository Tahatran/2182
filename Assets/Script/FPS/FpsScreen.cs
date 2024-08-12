using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Frame Rate")]
    [SerializeField] bool setFrameRateAutomatically = false;

    [Space]
    [SerializeField] AllowedFrameRates defaultFrameRate = AllowedFrameRates.Rate120;
    [SerializeField] AllowedFrameRates batterySaveFrameRate = AllowedFrameRates.Rate60;

    [Header("Sleep")]
    [SerializeField] int sleepTimeout = -1;

    void Start()
    {
        Initialise();
    }

    public void Initialise()
    {
        Screen.sleepTimeout = sleepTimeout;

        if (setFrameRateAutomatically)
        {
            uint numerator = Screen.currentResolution.refreshRateRatio.numerator;
            uint denominator = Screen.currentResolution.refreshRateRatio.denominator;

            if (numerator != 0 && denominator != 0)
            {
                Application.targetFrameRate = Mathf.RoundToInt(numerator / denominator);
            }
            else
            {
                Application.targetFrameRate = (int)defaultFrameRate;
            }
        }
        else
        {
#if UNITY_IOS
                        if(UnityEngine.iOS.Device.lowPowerModeEnabled)
                        {
                            Application.targetFrameRate = (int)batterySaveFrameRate;
                        }
                        else
                        {
                            Application.targetFrameRate = (int)defaultFrameRate;
                        }    
#else
            Application.targetFrameRate = (int)defaultFrameRate;
#endif
        }
        // Application.targetFrameRate = 60;
    }

    private enum AllowedFrameRates
    {
        Rate30 = 30,
        Rate60 = 60,
        Rate90 = 90,
        Rate120 = 120,
    }
}
