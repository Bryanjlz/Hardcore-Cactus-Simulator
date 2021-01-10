using UnityEngine;

public class MouseHover: MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;

    public void OnMouseEnter() {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }

    public void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}