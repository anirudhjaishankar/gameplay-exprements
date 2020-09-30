using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour
{
    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalAxisInputValue = Input.GetAxis("Vertical");
        float horizontalAxisInputValue = Input.GetAxis("Horizontal");
        bool runInputPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpInputPressed = Input.GetKeyDown(KeyCode.Space);
        bool dashInputPressed = Input.GetKeyDown(KeyCode.LeftControl);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isDashing", false);

        if (verticalAxisInputValue == 0)
        {
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isRunning", false);

        }
        if (verticalAxisInputValue != 0 || horizontalAxisInputValue != 0)
        {
            playerAnimator.SetBool("isWalking", true);
            if (runInputPressed)
            {
               playerAnimator.SetBool("isRunning", true);
               if (dashInputPressed)
               {
                    playerAnimator.SetBool("isDashing", true);
                }
                else
                {
                    playerAnimator.SetBool("isDashing", false);
                }
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
            }
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
        if (jumpInputPressed)
        {
            playerAnimator.SetBool("isJumping", true);
        }
        
    }
}
