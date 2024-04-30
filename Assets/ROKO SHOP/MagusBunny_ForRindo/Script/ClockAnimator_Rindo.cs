using System;
using UnityEngine;

public class ClockAnimator_Rindo : MonoBehaviour
{
    private Transform hoursTransform, minutesTransform, secondsTransform;
    private const float hoursToDegrees = 30f, minutesToDegrees = 6f, secondsToDegrees = 6f;

    void Start()
    {
        hoursTransform = GameObject.Find("SK_Watch_HourHand").transform;
        minutesTransform = GameObject.Find("SK_Watch_MinuteHand").transform;
        secondsTransform = GameObject.Find("SK_Watch_SecondHand").transform;

        if (hoursTransform == null || minutesTransform == null || secondsTransform == null)
        {
            Debug.LogError("One or more of the clock hands could not be found. Please make sure their names are correct and they exist in the scene.");
        }
    }

    void Update()
    {
        if (hoursTransform != null && minutesTransform != null && secondsTransform != null)
        {
            DateTime time = DateTime.Now;

            // Minute fractions (0-1) are added to the hour to make the hour hand move more smoothly
            float hourAsFloat = time.Hour + time.Minute / 60f;

            hoursTransform.localRotation = Quaternion.Euler(0f, hoursToDegrees * hourAsFloat, 0f);
            minutesTransform.localRotation = Quaternion.Euler(0f, minutesToDegrees * time.Minute, 0f);
            secondsTransform.localRotation = Quaternion.Euler(0f, secondsToDegrees * time.Second, 0f);
        }
    }
}
