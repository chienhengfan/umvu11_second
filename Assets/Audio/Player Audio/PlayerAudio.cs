using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip playerUltimateSkill;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            AudioSource.PlayClipAtPoint(playerUltimateSkill, transform.position);
        }
    }
}
