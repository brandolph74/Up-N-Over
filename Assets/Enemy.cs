using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyGO;
    public GameObject playerGO;
    
    public Rigidbody2D rb;

    public BoxCollider2D boxCollider;

    public float engageDistance;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void FixedUpdate()
    {
        float distance = Vector2.Distance(enemyGO.transform.position, playerGO.transform.position);      //get the distance between enemy and player 
        
        if (distance < engageDistance)  //if the distance between player and enemy is less than the set engagement distance
        {
            
            var horizontalDistance = new Vector2(playerGO.transform.position.x, enemyGO.transform.position.y);

            //move towares the player
            enemyGO.transform.position = Vector2.MoveTowards(enemyGO.transform.position, horizontalDistance, movementSpeed * Time.deltaTime);
        }
    }
    
}
