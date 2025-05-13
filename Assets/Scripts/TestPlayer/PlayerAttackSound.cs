using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip[] attackSounds;
    private SoundFXManager soundManager;

    public void PlayAttackSound(int soundIndex)
    {
        if (attackSounds != null && soundIndex < attackSounds.Length && soundManager != null)
        {
            soundManager.PlaySound(
                attackSounds[soundIndex],
                transform.position,
                SoundFXManager.SoundCategory.SFX,
                1.0f, // volume
                1.0f, // pitch
                0.1f, // pitch randomness
                0.1f  // volume randomness
            );
        }
    }
}
