using UnityEngine;

public class RatMiniBossAttackController : MonoBehaviour
{
    private RatMiniBossAttack leftFist;
    private RatMiniBossAttack rightFist;

    // Animation event calls this at the correct moment for the left attack
    public void EnableLeftFistDamage() { leftFist.EnableDamage(); }
    public void DisableLeftFistDamage() { leftFist.DisableDamage(); }

    // Animation event calls this at the correct moment for the right attack
    public void EnableRightFistDamage() { rightFist.EnableDamage(); }
    public void DisableRightFistDamage() { rightFist.DisableDamage(); }

    // Start is called before the first frame update
    void Start()
    {
        leftFist = GameObject.Find("RightArm").GetComponent<RatMiniBossAttack>();
        rightFist = GameObject.Find("LeftArm").GetComponent<RatMiniBossAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
