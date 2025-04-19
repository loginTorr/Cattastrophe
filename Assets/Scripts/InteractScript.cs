using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class InteractScript : MonoBehaviour
{
    public GameObject InteractUI;
    public GameObject GameHub;

    public PlayerInput playerInput;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay()
    {
        InteractUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            var ObjectName = gameObject.name;

            if (ObjectName == "FirstDungeon")
            {
                StartCoroutine(StartDungeon());
            }

            if (ObjectName == "SecondDungeon")
            {

            }
            GameHub.SetActive(false);
        }
    }

    private void OnTriggerExit()
    {
        InteractUI.SetActive(false);
    }

    IEnumerator StartDungeon()
    {
        playerInput.DeactivateInput();
        yield return new WaitForSeconds(3f);
        playerInput.ActivateInput();

    }
}
