using TMPro;
using UnityEngine;
using HighScore;

public class SubmitHighScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TMP_InputField inputField;
    public GameObject name;
    public GameObject home;
    public GameObject restart;
    public GameObject exit;
    public GameObject username;
    public int score;

    private void Start()
    {
        if (ScoreSave.instance != null)
        {
            score = ScoreSave.instance.score;
            scoreText.text = "High Score:  " + score;
        }
        else
        {
            Debug.LogWarning("ScoreSave.instance is null");
            scoreText.text = "High Score:  0";
        }
        inputField.characterLimit = 15;
        inputField.onSubmit.AddListener(SubmitText);
    }

    private void SubmitText(string text)
    {
        Debug.Log("Submitted: " + text);
        HS.SubmitHighScore(this, text, score);
        name.SetActive(false);
        username.SetActive(false);
        exit.SetActive(true);
        restart.SetActive(true);
        home.SetActive(true);
    }
}
