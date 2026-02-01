using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    int crystalsCollected = 0;
    float moneyEarned = 0f;
    public static int CrystalsCollected
    {
        get
        {
            return instance.crystalsCollected;
        }
        set
        {
            instance.crystalsCollected = value;
            instance.crystalsCollectedText.text = value.ToString();
        }
    }
    public static float MoneyEarned
    {
        get
        {
            return instance.moneyEarned;
        }
        set
        {
            instance.moneyEarned = value;
            instance.moneyEarnedText.text = "$" + value.ToString("n2");
        }
    }

    [SerializeField] TMP_Text crystalsCollectedText;
    [SerializeField] TMP_Text moneyEarnedText;
    [SerializeField] Image oxygenMeter;
    [SerializeField] Timer oxygenTimer = new Timer(60f);
    [SerializeField, Range(0f, 1f)] float lowOxygenPercentageThreshold = 0.25f;

    public static bool oxygenMeterIsActive = true;

    static MainScreen instance;

    public static TimeSpan GameSpan { get; private set; }

    void Awake()
    {
        instance = this;
        GameSpan = new TimeSpan();
    }

    void Update()
    {
        if (oxygenMeterIsActive)
        {
            oxygenTimer.Update(Time.deltaTime);
            if (oxygenTimer.TimeRemaining < lowOxygenPercentageThreshold)
            {

            }
            if (oxygenTimer.Laps > 0)
            {
                // Oxygen is out
                GameOverScreen.Enable(false);
            }
        }
        GameSpan = GameSpan.Add(TimeSpan.FromSeconds(Time.deltaTime));
        oxygenMeter.fillAmount = oxygenTimer.FractionOfTimeRemaining;
    }

    public static void AddOxygen(float oxygen)
    {
        instance.oxygenTimer.Update(-oxygen);
    }

    void OnDestroy()
    {
        instance = null;
        oxygenMeterIsActive = true;
        GameSpan = new TimeSpan();
    }
}
