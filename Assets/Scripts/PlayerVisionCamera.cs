using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerVisionCamera : MonoBehaviour
{
    // Assign in the inspector
    [SerializeField] Camera cam;
    public static Camera Cam => instance.cam;

    static PlayerVisionCamera instance;

    void Awake()
    {
        instance = this;

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
        cam.targetTexture = renderTexture;
    }

    void OnDestroy()
    {
        instance = null;
    }
}
