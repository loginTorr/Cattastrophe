using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHouse : MonoBehaviour
{
    [Header("References")]
    private EEPuzzle Puzzle;
    public GameObject LeftHouseText;
    [Header("Misc")]
    private bool PlayerInRadius = false;

    void Awake() {
        Puzzle = FindObjectOfType<EEPuzzle>();
        LeftHouseText = GameObject.Find("LeftHouseText");
        LeftHouseText.SetActive(false);
    }

    private void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = true; }
    }

    private void OnTriggerExit(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = false; }
    }

    void Update() {
        if (PlayerInRadius && Input.GetKeyDown(KeyCode.E)) {
            Puzzle.LeftHouseFunc();
        }
    }

    public void ShowText() {
        StartCoroutine(LeftHouseTextFunc());
    }

    IEnumerator LeftHouseTextFunc() {
        LeftHouseText.SetActive(true);
        yield return new WaitForSeconds(5);
        LeftHouseText.SetActive(false);
    }
}
