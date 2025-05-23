using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEPuzzle : MonoBehaviour
{
    [Header("References")]
    public GameObject MidHouse, LeftHouse, RightHouse;
    public int PuzzleCount = 0;
    public GameObject CursorController, EECursor;
    public MidHouse MidHouseScript;
    public LeftHouse LeftHouseScript;
    public RightHouse RightHouseScript;
    [Header("Misc")]
    private bool CanChange = true;

    void Awake() {
        CursorController = GameObject.Find("CursorController");
        EECursor = GameObject.Find("EECursor");
        EECursor.SetActive(false);

        if (GameObject.Find("MidHouse") != null) {
            MidHouseScript = GameObject.Find("MidHouse").GetComponent<MidHouse>();
            LeftHouseScript = GameObject.Find("LeftHouse").GetComponent<LeftHouse>();
            RightHouseScript = GameObject.Find("RightHouse").GetComponent<RightHouse>();
        }
    }

    void Update()
    {
        if (PuzzleCount == 3 && CanChange) {
            StartCoroutine(ChangeCursor());
        }
    }

    public void MidHouseFunc() {
        PuzzleCount = 1;
        MidHouseScript.ShowText();
    }

    public void LeftHouseFunc() {
        if (PuzzleCount == 1) { PuzzleCount = 2; LeftHouseScript.ShowText(); }
        else { PuzzleCount = 0; }
    }

    public void RightHouseFunc() {
        if (PuzzleCount == 2) { PuzzleCount = 3; RightHouseScript.ShowText(); }
        else { PuzzleCount = 0; }
    }

    IEnumerator ChangeCursor() {
        CanChange = false;
        yield return null;
        Debug.Log("puzzle done");
        CursorController.SetActive(false);
        EECursor.SetActive(true);
    }
}
