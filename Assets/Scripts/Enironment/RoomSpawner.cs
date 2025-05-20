using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour {
    [Header("Rooms To Spawn")]
    public List<GameObject> SmallRooms;
    public List<GameObject> MediumRooms;
    public List<GameObject> BigRooms;

    public GameObject lastRoom;
    public GameObject MiniBossRoom;
    public GameObject BossRoom;

    [Header("Room Bools for EnemySpawn Script")]
    public bool isSmall;
    public bool isMedium;
    public bool isBig;

    [Header("Spawn Chances")]
    [Range(0, 100)] private float SmallChance = 75;
    [Range(0, 100)] private float MediumChance = 15;
    [Range(0, 100)] private float BigChance = 10;

    [Header("Other")]
    private int roomsSpawned = 0;
    private GameObject Player;
    private PlayerMovement PlayerMovementScript;
    private GameObject PlayerSpawn;
    private MusicManager musicManager;



    void Start() {
        musicManager = MusicManager.Instance;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();

    }

    
    void Update() {

    }

    public IEnumerator SpawnNextRoom()
    {
        Game.InteractUI.SetActive(false);
        // Optional: destroy previous room
        if (lastRoom != null)
            Destroy(lastRoom);

        GameObject prefabToSpawn;

        // Boss at #12
        if (roomsSpawned == 3)
        {
            prefabToSpawn = MiniBossRoom;

            if (musicManager != null)
            {
                musicManager.PlayBossMusic();
            }
        }

        else if (roomsSpawned == 7)
        {
            prefabToSpawn = BossRoom;

            if (musicManager != null)
            {
                musicManager.PlayBossMusic();
            }
        }

        else if (roomsSpawned == 8)
        {
            prefabToSpawn = null;
            SceneManager.LoadScene("Victory");
        }
        else
        {
            int roll = Random.Range(1, 101);

            if (roll <= MediumChance + SmallChance && roll >= SmallChance)
            {
                prefabToSpawn = MediumRooms[Random.Range(0, MediumRooms.Count)];
            }
            else
            {
                prefabToSpawn = SmallRooms[Random.Range(0, SmallRooms.Count)];
                // bump med & large odds by 1% each
                SmallChance -= 1;
                MediumChance += 1;
                BigChance += 1;
            }

            if (musicManager != null)
            {
                musicManager.PlayAmbientMusic();
            }
        }

        Room roomScript = prefabToSpawn.GetComponent<Room>();

        if (roomScript == null || roomScript.EntryPoint == null)
        {
            Debug.LogError("Prefab missing Room+EntryPoint!");
        }

        // local position of the entry marker
        //Vector3 entryLocal = roomScript.EntryPoint.transform.position;

        // do the spawn
        lastRoom = Instantiate(prefabToSpawn, new Vector3(0,0,0), Quaternion.identity);
        PlayerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        //Player.transform.position = entryLocal;
        Player.transform.position = new Vector3(0, 0, 0);

        roomsSpawned++;

        yield return new WaitForSeconds(1.3f);
        CameraFade.fadeInstance.FadeIn();
        yield return new WaitForSeconds(0.5f);
        PlayerMovementScript.paused = false;
    }
}


