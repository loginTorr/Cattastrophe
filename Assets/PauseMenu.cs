using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool enter = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(enter){
                pauseMenu.SetActive(true);
                enter = false;
                Time.timeScale = 0;
            }
            else{
                pauseMenu.SetActive(false);
                enter = true;
                Time.timeScale = 1;
            }
            
            
        }
    }

    public void ButtonPressed(){
        pauseMenu.SetActive(false);
        enter = true;
        Time.timeScale = 1;
    }
}
