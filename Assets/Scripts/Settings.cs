using System;
using UnityEngine;

public class Settings : Reset
{
    public static float BackgroundSpeed = 0.252f;
    public static float Speed = 0.7f;

    public static bool OnGround = true;
    public static bool GameOver = true;

    public static int TimesIncreased = 1;
    private void Start()
    {
        InvokeRepeating(nameof(IncreaseSpeed), 5.0f, 5.0f);
    }

    private void Update()
    {
        if (!GameOver) return;
        if(Input.GetMouseButton(1))
            Application.Quit();
    }

    private void IncreaseSpeed()
    {
        Speed += 0.05f;
        //BackgroundSpeed += 0.0036f; //36% Difference between speed and background speed
        TimesIncreased++;
    }

    public override void ResetSettings()
    {
        BackgroundSpeed = 0.252f;
        Speed = 0.7f;
        TimesIncreased = 1;
    }
}
