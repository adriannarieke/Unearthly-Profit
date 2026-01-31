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
        cam.projectionMatrix = Camera.main.projectionMatrix;
    }

    void OnDestroy()
    {
        instance = null;
    }
}
