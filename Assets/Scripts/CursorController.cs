using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    [SerializeField] private Texture2D CursorTextureDefault;
    [SerializeField] private Vector2 ClickPosition = Vector2.zero;

    void Start() {
        Cursor.SetCursor(CursorTextureDefault, ClickPosition, CursorMode.Auto);
    }
}
