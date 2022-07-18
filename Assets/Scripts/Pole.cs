using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public Rigidbody2D player;
    public Animator poleAnimator;
    public Animator playerAnimator;
    public Rigidbody2D poleBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground & Walls" && PlayerMovement.state != PlayerState.pole_thrown)
        {
            StartCoroutine(jabTimer());  //give a small delay
            player.bodyType = RigidbodyType2D.Static;
            poleAnimator.speed = 0;
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.Play("player_stuck", 0);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject;
            enemy.SetActive(false);
        }

        
    }

    IEnumerator jabTimer()
    {
        
        yield return new WaitForSeconds(0.2f);
        PlayerMovement.state = PlayerState.stuck;
        
    }
    
}

