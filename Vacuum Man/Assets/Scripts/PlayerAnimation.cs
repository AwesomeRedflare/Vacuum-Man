using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private GoodPlatformerController player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<GoodPlatformerController>();
    }

    private void Update()
    {
        if (player.moveInput != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
