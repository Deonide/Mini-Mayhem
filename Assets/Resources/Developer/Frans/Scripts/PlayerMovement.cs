using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;

    [SerializeField] 
    private int m_voteCount = 1;

    [CanBeNull]
    [SerializeField] 
    private GameObject m_portals, m_bomb, m_bombSpawnPoint;


    //variablen voor het stemmen op je gameMode
    [SerializeField] private bool m_canVote = false;


    //Rigidbody
    Rigidbody rb;

    private Vector2 movementInput = Vector2.zero;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    #region Input
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Scene scene = SceneManager.GetActiveScene();
            int index = scene.buildIndex;

            switch (index)
            {
                case 1:
                    Vote();
                    break;
                case 2:
                    BomberDuck();
                    break;
                case 3:
                    BumperDucks();
                    break;
                case 4:
                    DuckLordSays();
                    break;
                case 5:
                    QuickDucks();
                    break;
                case 6:
                    FallingPlatforms();
                    break;
                case 7:
                    SinkingPlatforms();
                    break;
            }
        }
    }
    #endregion
    #region Actions
    private void Vote()
    {
        if (m_portals != null && m_voteCount == 1 && m_canVote)
        {
            m_portals.GetComponent<Portals>().m_AmountOfVotes++;
        }
        m_voteCount--;
    }

    private void BomberDuck()
    {
        Instantiate(m_bomb, m_bombSpawnPoint.transform.position, Quaternion.identity);
    }

    private void BumperDucks()
    {

    }

    private void DuckLordSays()
    {

    }
    
    private void QuickDucks()
    {

    }

    private void FallingPlatforms()
    {

    }

    private void SinkingPlatforms()
    {

    }
    #endregion

    #region Jump
    //Variables voor de player movement/jump stats
    /*    

        [SerializeField] 
        private float gravityValue = 2f;

        [SerializeField] 
        private float jumpHeight = 1.0f;*/

    /*    private Vector3 playerVelocity;
    private bool groundedPlayer = false;
    private bool jumped = false;*/

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            m_canVote = true;
            if(m_portals == null)
            {
                m_portals = collision.gameObject;
            }  

        }
        else
        {
            m_portals = null;
            m_canVote = false;
        }
    }
    public void OnInteracte(InputAction.CallbackContext context)
    {
        if (context.performed && m_voteCount != 0 && m_canVote)
        {
            if (m_portals != null)
            {
                m_portals.GetComponent<Portals>().m_AmountOfVotes++;
            }
            m_voteCount--;
        }

    }
