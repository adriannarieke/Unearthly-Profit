using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Assign these in the inspector
    [SerializeField] InputActionAsset input;
    readonly Dictionary<string, (Sprite, Sprite)> sprites = new Dictionary<string, (Sprite, Sprite)>();
    readonly Dictionary<string, InputActionMap> actionMaps = new Dictionary<string, InputActionMap>();

    static InputManager instance;

    public static InputActionAsset Input => instance.input;
    public static InputActionMap PlayerInput => instance.actionMaps["Player"];
    public static InputActionMap UIInput => instance.actionMaps["UI"];
    public static Vector2 CursorPosition { get; private set; } = Vector2.zero;
    public static Vector2 CursorPositionDelta { get; private set; } = Vector2.zero;
    public static bool CursorDown { get; private set; } = false;

    public static InputAction PrimaryTouchAction { get; private set; }
    public static InputAction ClickAction { get; private set; }

    void Awake()
    {
        instance = this;
        foreach (InputActionMap actionMap in input.actionMaps)
        {
            actionMaps.Add(actionMap.name, actionMap);
        }

        PrimaryTouchAction = GetAction("PrimaryTouch");
        ClickAction = GetAction("Click");
    }

    void Update()
    {
        CursorDown = ClickAction.IsPressed();

        Vector2 newCursorPosition = PrimaryTouchAction.ReadValue<Vector2>();

        if (newCursorPosition.x < 0f || newCursorPosition.y < 0f ||
            newCursorPosition.x > Screen.width || newCursorPosition.y > Screen.height)
        {
            newCursorPosition.x = Mathf.Clamp(newCursorPosition.x, 0f, Screen.width);
            newCursorPosition.y = Mathf.Clamp(newCursorPosition.y, 0f, Screen.height);
            CursorPositionDelta = Vector2.zero;
            CursorPosition = newCursorPosition;
        }
        else
        {
            CursorPositionDelta = newCursorPosition - CursorPosition;
            CursorPosition = newCursorPosition;
        }
    }

    void OnDestroy()
    {
        instance = null;
        PrimaryTouchAction = null;
        ClickAction = null;
        CursorPosition = Vector2.zero;
        CursorDown = false;
    }

    public static bool GetSprite(string key, out (Sprite, Sprite) sprite)
    {
        return instance.sprites.TryGetValue(key, out sprite);
    }

    public static InputActionMap GetActionMap(string actionMapName)
    {
        return instance.actionMaps[actionMapName];
    }

    public static InputAction GetAction(string actionName)
    {
        foreach (InputActionMap actionMap in instance.actionMaps.Values)
        {
            InputAction foundAction = actionMap.FindAction(actionName);
            if (foundAction != null)
            {
                return foundAction;
            }
        }

        return null;
    }
}
