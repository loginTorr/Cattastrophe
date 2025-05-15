using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighScore;

public class PlayerHealth : MonoBehaviour
{
    [Header("SoundFX")]
    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private AudioClip deathSound;

    [Header("Sound Settings")]
    [SerializeField] private float minDamageInterval = 0.2f;
    [SerializeField] private float lowHealthThreshold = 0.3f;

    public HealthBar healthBar;
    public GameObject player;
    public GameObject gameOverScreen;

    private PlayerMovement PlayerMovmentScript;
    private float currentHealth;
    private float lastLowHealthSoundTime;
    private Coroutine lowHealthCoroutine;
    private float lastDamageTime;
    private SoundFXManager soundManager;

    private GameHighScore gameHighScoreScript;


    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundFXManager.Instance;
        PlayerMovmentScript = GetComponent<PlayerMovement>();

        currentHealth = PlayerMovmentScript.MaxHealth;
        healthBar.SetMaxHealth(PlayerMovmentScript.MaxHealth);
        
        gameHighScoreScript = GameObject.Find("Player").GetComponent<GameHighScore>();
        print("High Score Script is " + gameHighScoreScript);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            PlayDamageSound();
        }
        currentHealth -= damage;
        PlayerMovmentScript.CurHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 0){
            PlayDeathSound();
            print("lol you died");
            GameOver();
        }
    }

    public void OnTriggerStay(Collider collider){
        if(collider.gameObject.CompareTag("Bed")){
            if(Input.GetKey(KeyCode.E)){
                healthBar.SetHealth(PlayerMovmentScript.MaxHealth);
                currentHealth = PlayerMovmentScript.MaxHealth;
            }
        }
    }
    
    public void GameOver(){
        print("game over");
        HS.SubmitHighScore(this, "testName", gameHighScoreScript.Score);
        gameOverScreen.SetActive(true);
        Destroy(player);
    }

    public void PlayDamageSound()
    {
        if (Time.time - lastDamageTime < minDamageInterval)
            return;

        lastDamageTime = Time.time;

        soundManager.PlayRandomSound(damageSounds, transform.position, SoundFXManager.SoundCategory.SFX, 1f, 1f);

    }

    public void PlayDeathSound()
    {
        if (lowHealthCoroutine != null)
        {
            StopCoroutine(lowHealthCoroutine);
            lowHealthCoroutine = null;
        }

        if (deathSound != null && soundManager != null)
        {
            soundManager.PlaySound(
                deathSound,
                transform.position,
                SoundFXManager.SoundCategory.SFX,
                1.0f,
                1.0f
            );
        }
    }
}
