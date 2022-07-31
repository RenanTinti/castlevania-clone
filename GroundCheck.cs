using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Player player;
    void Awake()
    {
        player = gameObject.transform.parent.gameObject.GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
            player.canMove = true;
            player.anim.SetBool("Jump", false);
            player.onGround = true;
            player.isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
            player.onGround = false;
        }
    }
}
