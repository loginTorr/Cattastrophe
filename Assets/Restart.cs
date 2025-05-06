using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void RestartClicked(){
        Debug.Log("Clicked");
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/GameScene");
    }
}
