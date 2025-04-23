using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    [Header("Rooms To Spawn")]
    public List<GameObject> RoomList = new List<GameObject>();

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
        int RandIndex = Random.Range(0, RoomList.Count);
        GameObject NextRoom = RoomList[RandIndex];
        GameObject Room = Instantiate(NextRoom, transform.position, Quaternion.identity);
        RoomList.RemoveAt(RandIndex);
        // for testing
        yield return new WaitForSeconds(3);
        Destroy(Room);

    }
}
