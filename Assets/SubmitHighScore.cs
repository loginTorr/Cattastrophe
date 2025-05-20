using TMPro;
using UnityEngine;

public class SubmitHighScore : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject name;
    public GameObject home;
    public GameObject restart;
    public GameObject exit;
    public GameObject username;

    private void Start()
    {
        inputField.characterLimit = 15;
        inputField.onSubmit.AddListener(SubmitText);
    }

    private void SubmitText(string text)
    {
        Debug.Log("Submitted: " + text);
        name.SetActive(false);
        username.SetActive(false);
        exit.SetActive(true);
        restart.SetActive(true);
        home.SetActive(true);
    }
}
