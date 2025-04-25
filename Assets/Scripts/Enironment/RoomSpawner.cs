using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    [Header("Rooms To Spawn")]
    public List<GameObject> RoomList = new List<GameObject>();
    [Header("Other")]
    public float HeightOffset = 100f;
    // for tping in this script
    public Transform Player;

    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) { SpawnRoom(); }
    }

    void SpawnRoom() {
        if (RoomList.Count == 0) {
            // could spawn boss or shop or something like that
            Debug.Log("out of rooms in list");
            return;
        }

        StartCoroutine(ChooseAndSpawn());
    }

    IEnumerator ChooseAndSpawn() {
        // maybe could lock player controls this coroutine starts so it wont mess with the tp or room spawning
        int RandIndex = Random.Range(0, RoomList.Count);
        Vector3 SpawnPosition = transform.position + Vector3.up * HeightOffset;
        GameObject NextRoom = RoomList[RandIndex];
        GameObject Room = Instantiate(NextRoom, SpawnPosition, Quaternion.identity);
        RoomList.RemoveAt(RandIndex);
        // tps player but we can add this to rooms themselves later
        yield return new WaitForSeconds(1);
        Player.position += Vector3.up * HeightOffset;
        

    }
}
