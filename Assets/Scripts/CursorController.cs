using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    [Header("Cursors")]
    public Texture2D CursorDefault, CursorClick;
    [Header("Other")]
    public Vector2 ClickPosition = Vector2.zero;
    public float Duration = 0.5f;
    public bool IsClicking = false;

    void Start() {
        Cursor.SetCursor(CursorDefault, ClickPosition, CursorMode.Auto);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && !IsClicking) {
            StartCoroutine(ChangeCursor());
        }
    }

    IEnumerator ChangeCursor() { 
            IsClicking = true;
            Cursor.SetCursor(CursorClick, ClickPosition, CursorMode.Auto);
            yield return new WaitForSeconds(Duration);
            IsClicking = false;
            Cursor.SetCursor(CursorDefault, ClickPosition, CursorMode.Auto);
        }
}
