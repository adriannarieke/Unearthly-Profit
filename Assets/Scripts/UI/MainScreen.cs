using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    int crystalsCollected = 0;
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

    [SerializeField] TMP_Text crystalsCollectedText;
    [SerializeField] Image oxygenMeter;
    [SerializeField] Timer oxygenTimer = new Timer(60f);
    [SerializeField, Range(0f, 1f)] float lowOxygenPercentageThreshold = 0.25f;

    static MainScreen instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        oxygenTimer.Update(Time.deltaTime);
        oxygenMeter.fillAmount = oxygenTimer.FractionOfTimeRemaining;
        if (oxygenTimer.TimeRemaining < lowOxygenPercentageThreshold)
        {

        }
        if (oxygenTimer.Laps > 0)
        {
            // Oxygen is out
        }
    }

    void OnDestroy()
    {
        instance = null;
    }
}
