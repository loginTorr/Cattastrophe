using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHouse : MonoBehaviour
{
    [Header("References")]
    private EEPuzzle Puzzle;
    public GameObject RightHouseText;
    [Header("Misc")]
    private bool PlayerInRadius = false;

    void Awake() {
        Puzzle = FindObjectOfType<EEPuzzle>();
        RightHouseText = GameObject.Find("RightHouseText");
        RightHouseText.SetActive(false);
    }

    private void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = true; }
    }

    private void OnTriggerExit(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = false; }
    }

    void Update() {
        if (PlayerInRadius && Input.GetKeyDown(KeyCode.E)) {
            Puzzle.RightHouseFunc();
            StartCoroutine(RightHouseTextFunc());
        }
    }

    IEnumerator RightHouseTextFunc() {
        RightHouseText.SetActive(true);
        yield return new WaitForSeconds(4);
        RightHouseText.SetActive(false);
    }
}
