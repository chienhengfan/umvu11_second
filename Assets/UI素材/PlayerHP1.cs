using Movementsystem;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float playeMaxHp;
    public float playerCurrentHp;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playeMaxHp = player.GetComponent<ThirdPersonController>().playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentHp = player.GetComponent<ThirdPersonController>().playerHP;

        this.transform.localPosition = new Vector3((-460 + 460 * (playerCurrentHp / playeMaxHp)), 0.0f, 0.0f);
    }
}
