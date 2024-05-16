using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    // References
    private Text speedText;

    private void Awake()
    {
        // Subscribe Events to methods
        PlayerAgent.PlayerSpeedEvent += UpdateSpeedUI;

        // Set references
        speedText = GetComponentInChildren<Text>();
    }

    private void UpdateSpeedUI(float currentSpeed)
    {
        // Round the speed to 2 decimal points
        currentSpeed = (float)Math.Round(currentSpeed, 1);

        // Change the text on the UI
        speedText.text = $"Speed: {currentSpeed}";
    }
}
