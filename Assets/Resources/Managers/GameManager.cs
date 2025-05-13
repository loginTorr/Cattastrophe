using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance { get; private set; }

    private static GameObject managerPrefab;

    public static GameManager GetInstance()
    {
        if (Instance == null)
        {
            if (managerPrefab != null)
            {
                Instantiate(managerPrefab);
            }
            else
            {
                GameObject prefab = Resources.Load<GameObject>("Managers/GameManager");
                Debug.Log(prefab);
                Debug.Log("GameManager");
                if (prefab != null)
                {
                    managerPrefab = prefab;
                    Instantiate(prefab);
                }
                else
                {
                    GameObject obj = new GameObject("GameManager");
                    obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj);
                }
            }
        }

        return Instance;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
