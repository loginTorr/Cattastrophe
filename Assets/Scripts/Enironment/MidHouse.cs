using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidHouse : MonoBehaviour
{
    [Header("References")]
    private EEPuzzle Puzzle;
    public GameObject MidHouseText;
    [Header("Misc")]
    private bool PlayerInRadius = false;

    void Awake() {
        Puzzle = FindObjectOfType<EEPuzzle>();
        MidHouseText = GameObject.Find("MidHouseText");
        MidHouseText.SetActive(false);
    }

    private void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = true; }
    }

    private void OnTriggerExit(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = false; }
    }

    void Update() {
        if (PlayerInRadius && Input.GetKeyDown(KeyCode.E)) {
            Puzzle.MidHouseFunc();
            StartCoroutine(MidHouseTextFunc());
        }
    }

    IEnumerator MidHouseTextFunc() {
        MidHouseText.SetActive(true);
        yield return new WaitForSeconds(4);
        MidHouseText.SetActive(false);
    }
}
