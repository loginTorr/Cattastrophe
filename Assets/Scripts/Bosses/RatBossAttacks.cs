using System.Collections.Generic;
using UnityEngine;

public class RatBossAttacks : MonoBehaviour
{
    public bool canDealDamage = false;
    private HashSet<Collider> hitPlayers = new HashSet<Collider>();

    public void EnableDamage()
    {
        canDealDamage = true;
        hitPlayers.Clear();
    }
    public void DisableDamage()
    {
        canDealDamage = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"canDealDamage={canDealDamage} other.tag={other.tag} hitPlayersContains={hitPlayers.Contains(other)}");

        if (canDealDamage && other.CompareTag("Player") && !hitPlayers.Contains(other))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            Debug.Log(player);
            if (player != null)
            {
                //Debug.Log("DamageDone");
                player.TakeDamage(15);
                hitPlayers.Add(other);
            }
        }
    }
}
