using UnityEngine;

public class RatBossAttackController : MonoBehaviour
{
    public RatBossAttacks leftFist;
    public RatBossAttacks rightFist;

    // Animation event calls this at the correct moment for the left attack
    public void EnableLeftFistDamage() { leftFist.EnableDamage(); }
    public void DisableLeftFistDamage() { leftFist.DisableDamage(); }

    // Animation event calls this at the correct moment for the right attack
    public void EnableRightFistDamage() { rightFist.EnableDamage(); }
    public void DisableRightFistDamage() { rightFist.DisableDamage(); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
