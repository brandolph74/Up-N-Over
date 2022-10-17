using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyGO;
    public GameObject playerGO;
    
    public Rigidbody2D rb;
    public Rigidbody2D enemyRB;

    public BoxCollider2D boxCollider;

    public float engageDistance;
    public float movementSpeed;

<<<<<<< Updated upstream
=======
    public int moveStep = 1;

    public bool canJump = true;
    public bool isGrounded = true;

    public Vector2 spawnPosition;

>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void FixedUpdate()
    {
        float distance = Vector2.Distance(enemyGO.transform.position, playerGO.transform.position);      //get the distance between enemy and player 
        
<<<<<<< Updated upstream
        if (distance < engageDistance)  //if the distance between player and enemy is less than the set engagement distance
        {
            
            var horizontalDistance = new Vector2(playerGO.transform.position.x, enemyGO.transform.position.y);

            //move towares the player
            enemyGO.transform.position = Vector2.MoveTowards(enemyGO.transform.position, horizontalDistance, movementSpeed * Time.deltaTime);
        }
=======
            float distance = Vector2.Distance(enemyGO.transform.position, playerGO.transform.position);      //get the distance between enemy and player
            
            float spawnDistance = Vector2.Distance(spawnPosition, this.transform.position);

        if (distance < engageDistance)  //if the distance between player and enemy is less than the set engagement distance
            {

            
             
            
                var horizontalDistance = new Vector2(playerGO.transform.position.x, enemyGO.transform.position.y);

                if (playerGO.transform.position.x < enemyGO.transform.position.x)
                {
                    enemyRB.velocity = new Vector2(movementSpeed * -1f, 0f);
                }
                else
                {
                    enemyRB.velocity = new Vector2(movementSpeed, 0f);
                }

                //move towares the player
                //enemyGO.transform.position = Vector2.MoveTowards(enemyGO.transform.position, horizontalDistance, movementSpeed * Time.deltaTime);

            }

        
        
        
        
>>>>>>> Stashed changes
    }
    
    
    IEnumerator enemyJump()
    {
        canJump = false;
        
        var playerPosition = enemyGO.transform.position.x - playerGO.transform.position.x;
        enemyRB.AddForce(new Vector2(playerPosition, 5f) * 100f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(5f);
        while(!isGrounded)
        {
            yield return null;
        }
        canJump = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
    }

}
