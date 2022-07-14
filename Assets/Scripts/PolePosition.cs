using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolePosition : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D solidSpearCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Pole Axis Base (Player)");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.state != PlayerState.stuck && PlayerMovement.state != PlayerState.pole_thrown)
        {
            transform.position = player.transform.position;
        }

    }
}
