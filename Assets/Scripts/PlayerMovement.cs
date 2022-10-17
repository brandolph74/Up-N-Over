using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {free, stuck, pole_thrown}

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 40f;

    float horizontalMove = 0f;

    Vector2 mousePosition;
    public Rigidbody2D polePivotPoint;
    public Animator poleAnimator;
    public CapsuleCollider2D spearCollider;
    Rigidbody2D playerRB;
    public Animator playerAnimator;

    public float jabSpeed;
    public float stuckJumpForce;
    public GameObject pole;


    public static PlayerControls playerControls;
    float jump;
    float crouch;
    public Vector2 inputVector;
    public bool usingMouse;
    public float jab;
    bool canJab = true;
    public float throwing;
    public Rigidbody2D poleBase;
    public GameObject poleBasePlayer;
    public BoxCollider2D solidSpearCollider;

    public BoxCollider2D playerColliderTrigger;

    public static PlayerState state;

    public static PlayerMovement playerMovement;

    public static bool canReturnSpear = false;

    public static bool returnSpearIsRunning = false;

    //public free freeStateScript = new free();

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this;
        state = PlayerState.free;
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerRB = GetComponent<Rigidbody2D>();

        Application.targetFrameRate = 120;

    }

    // Update is called once per frame
    void Update()
    {
        //inputs first

        horizontalMove = playerControls.Gameplay.Movement.ReadValue<float>() * speed;

        jump = playerControls.Gameplay.Jump.ReadValue<float>();

        crouch = playerControls.Gameplay.Crouch.ReadValue<float>();

        jab = playerControls.Gameplay.Jab.ReadValue<float>();

        throwing = playerControls.Gameplay.Throw.ReadValue<float>();



        switch (state)
        {
            case PlayerState.free:
                free_Update();
                break;
            case PlayerState.stuck:
                stuck_Update();
                break;
            case PlayerState.pole_thrown:
                pole_thrown_Update();
                break;
        }
    }

    void FixedUpdate()
    {

        switch (state)
        {
            case PlayerState.free:
                free_FixedUpdate();
                break;
            case PlayerState.stuck:
                stuck_FixedUpdate();
                break;
            case PlayerState.pole_thrown:
                pole_thrown_FixedUpdate();
                break;
        }

    }

    /// <summary>
    /// method called in FixedUpdate when the player state is pole_thrown
    /// </summary>
    void pole_thrown_FixedUpdate()
    {
        //can still move while in pole_thrown state

        bool isCrouching = false;
        bool isJumping = false;

        if (jump > 0)
        {
            isJumping = true;
        }
        if (crouch > 0)
        {
            isCrouching = true;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    }

    /// <summary>
    /// method called in Update when the player state is pole_thrown
    /// </summary>
    void pole_thrown_Update()
    {


        if (playerRB.bodyType == RigidbodyType2D.Dynamic)
        {

            if (horizontalMove != 0 && CharacterController.m_Grounded)
            {
                playerAnimator.SetBool("isRunning", true);
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
            }


            if (jump == 1)
            {
                playerAnimator.Play("player_jump", 0);
                playerAnimator.SetBool("isJumping", true);
            }

            if (playerAnimator.GetBool("isJumping") == false && CharacterController.m_Grounded == false)
            {
                playerAnimator.Play("player_fall", 0);
            }

            if (CharacterController.m_Grounded == true)
            {
                playerAnimator.SetBool("isGrounded", true);
            }
            else
            {
                playerAnimator.SetBool("isGrounded", false);
            }


            //logic that is disabled when the player is in the pole_thrown state

            /*
            if (usingMouse == false)  //if a controller is connected
            {
                inputVector = playerControls.Gameplay.PoleRotation.ReadValue<Vector2>();
                Cursor.lockState = CursorLockMode.Locked;

            }
            else
            {
                inputVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //if no controller and using mouse
                inputVector = inputVector - polePivotPoint.position;
                Cursor.lockState = CursorLockMode.None;
            }

            float aimAngle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg - 90f;

            if (inputVector.x == 0 && inputVector.y == 0)
            {

            }
            else
            {
                polePivotPoint.rotation = aimAngle;
            }
            

            if (jab > 0 && canJab == true)
            {
                poleAnimator.Play("jab", 0);
                canJab = false;
                spearCollider.enabled = true;
                StartCoroutine(jabCooldown());
            } */

            if (throwing == 1 && canReturnSpear == true)
            {
                //poleAnimator.Play("throw", 0);
                if (returnSpearIsRunning == false)
                {
                    StartCoroutine(returnSpear());
                }
                
                

            }

        }

    }

    IEnumerator returnSpear()
    {
        returnSpearIsRunning = true;
        poleBase.velocity = Vector2.zero;
        poleBase.bodyType = RigidbodyType2D.Kinematic;
        while (true)
        {
            if (Vector2.Distance(poleBase.transform.position, poleBasePlayer.transform.position) < 1f)
            {
                solidSpearCollider.enabled = false;
                state = PlayerState.free;
                break;
            }
            poleBase.transform.position = Vector2.MoveTowards(poleBase.position, poleBasePlayer.transform.position, 125f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        returnSpearIsRunning = false;
    }

    /// <summary>
    /// method called in Update when the player state is stuck
    /// </summary>
    void stuck_Update()
    {
        //jab = playerControls.Gameplay.Jab.ReadValue<float>();

        if (jab == 1)  //if jab is clicked again while stuck
        {
            StartCoroutine(stuckCooldown());
            spearCollider.enabled = false;
            playerRB.bodyType = RigidbodyType2D.Dynamic;
            playerRB.AddForce(transform.up * stuckJumpForce);
            poleAnimator.speed = 1;
            state = PlayerState.free;
            playerAnimator.Play("player_jump", 0);
        }
    }
    
    IEnumerator stuckCooldown()
    {
        canJab = false;
        yield return new WaitForSeconds(0.7f);
        canJab = true;
    }

    void stuck_FixedUpdate()
    {
        
    }

    /// <summary>
    /// method called in Update when the player state is free
    /// </summary>
    void free_Update()
    {
        
        if (playerRB.bodyType == RigidbodyType2D.Dynamic)
        {

            if (horizontalMove != 0 && CharacterController.m_Grounded)
            {
                playerAnimator.SetBool("isRunning", true);
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
            }


            if (jump == 1)
            {
                playerAnimator.Play("player_jump", 0);
                playerAnimator.SetBool("isJumping", true);
            }

            if (playerAnimator.GetBool("isJumping") == false && CharacterController.m_Grounded == false)
            {
                playerAnimator.Play("player_fall", 0);
            }

            if (CharacterController.m_Grounded == true)
            {
                playerAnimator.SetBool("isGrounded", true);
            }
            else 
            { 
                playerAnimator.SetBool("isGrounded", false); 
            }





            if (usingMouse == false)  //if a controller is connected
            {
                inputVector = playerControls.Gameplay.PoleRotation.ReadValue<Vector2>();
                Cursor.lockState = CursorLockMode.Locked;

            }
            else
            {
                inputVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //if no controller and using mouse
                inputVector = inputVector - polePivotPoint.position;
                Cursor.lockState = CursorLockMode.None;
            }

            float aimAngle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg - 90f;

            if (inputVector.x == 0 && inputVector.y == 0)
            {

            }
            else
            {
                polePivotPoint.rotation = aimAngle;
            }

            if (jab > 0 && canJab == true)
            {
                poleAnimator.Play("jab", 0);
                canJab = false;
                spearCollider.enabled = true;
                StartCoroutine(jabCooldown());
            }

            if (throwing > 0)
            {
                //poleAnimator.Play("throw", 0);
                poleBase.bodyType = RigidbodyType2D.Dynamic;
                state = PlayerState.pole_thrown;
                poleBase.AddRelativeForce(transform.up  * 3000f);
                StartCoroutine(throwCooldown());
                StartCoroutine(ColliderDetection());
                //Vector2.MoveTowards(poleBase.position, pole.transform.position, 100f * Time.deltaTime);
                
            }

        }
    }

    IEnumerator throwCooldown()
    {
        yield return new WaitForSeconds(1f);
        canReturnSpear = true;
    }

    IEnumerator ColliderDetection()
    {
       
        while (true)
        {
            if (solidSpearCollider.IsTouching(playerColliderTrigger))
            {
                solidSpearCollider.enabled = false;
                
            }
            else
            {
                solidSpearCollider.enabled = true;
                break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// state machine method called in fixed update if the current player state is free
    /// </summary>
    void free_FixedUpdate()
    {
        bool isCrouching = false;
        bool isJumping = false;

       if (jump > 0)
        {
            isJumping = true;
        }
        if (crouch > 0)
        {
            isCrouching = true;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    }

    IEnumerator jabCooldown()
    {
        yield return new WaitForSeconds(jabSpeed);
        canJab = true;
        spearCollider.enabled = false;

    }


    public void OnLand()
     {
         playerAnimator.SetBool("isJumping", false);
     }



  }

