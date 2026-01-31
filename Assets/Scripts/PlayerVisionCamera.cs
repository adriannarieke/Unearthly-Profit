using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteInEditMode]
public class PlayerVisionCamera : MonoBehaviour
{
    // Assign in the inspector
    [SerializeField] Camera cam;
    public static Camera Cam => instance.cam;

    static PlayerVisionCamera instance;

    void Awake()
    {
        if (Application.isPlaying)
        {
            instance = this;
            cam.projectionMatrix = Camera.main.projectionMatrix;
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        if (cam != null)
        {
            cam.projectionMatrix = Camera.main.projectionMatrix;
        }
    }
#endif

    void OnDestroy()
    {
        instance = null;
    }
}
