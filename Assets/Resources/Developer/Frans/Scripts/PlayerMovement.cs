using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public class PlayerScript : MonoBehaviour
{
    //Variables voor de player movement/jump stats
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = 2f;
    [SerializeField] private float jumpHeight = 1.0f;

    private Vector3 playerVelocity;
    private bool groundedPlayer = false;
    private bool jumped = false;


    private bool m_hasVoted,m_CanVote = false;
    private int m_voteIndex;

    //Rigidbody
    Rigidbody rb;

    private Vector2 movementInput = Vector2.zero;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }
    #region Jump
    /*    public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && groundedPlayer)
            {
                jumped = true;
            }
        }*/

    /*    void Update()
        {
            groundedPlayer = IsGrounded();

            if (jumped && groundedPlayer)
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                jumped = false;
            }

        }*/
    /*        if (!groundedPlayer)
        {
            rb.AddForce(Vector3.down * gravityValue * Physics.gravity.y, ForceMode.Acceleration);
        }*/
    /*    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }*/
    #endregion

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized * playerSpeed;
        Vector3 newPosition = rb.position + move * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }


    private void Update()
    {
        if(m_hasVoted)
        {
            m_hasVoted = false;
        }
    }

    public void OnInteracte(InputAction.CallbackContext context)
    {
        if (m_CanVote)
        {
            m_hasVoted = true;
            m_CanVote = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            m_CanVote = true;
        }
    }
}