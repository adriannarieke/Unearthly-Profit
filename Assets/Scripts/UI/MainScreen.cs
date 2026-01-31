using TMPro;
using UnityEngine;

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

    static MainScreen instance;

    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        instance = null;
    }
}
