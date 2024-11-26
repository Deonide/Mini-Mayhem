using JetBrains.Annotations;
using MiniGames.Combat;
using MiniGames.QuickTimeEvent;
using System.Numerics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    #region Variables
    #region Universal Variables
    [SerializeField] public int whichPlayer = 0;
    private float playerSpeed = 2.0f;
    public Scene scene;

    //Rigidbody
    Rigidbody rb;
    private UnityEngine.Vector2 movementInput = UnityEngine.Vector2.zero;
    private int m_health = 1;
    #endregion
    #region Voting
    [CanBeNull]
    [SerializeField]
    private GameObject m_portals;

    //variablen voor het stemmen op je gameMode
    private bool m_canVote = false;
    private int m_voteCount = 1;
    #endregion
    #region Bomberduck
    [CanBeNull]
    [SerializeField]
    private GameObject m_bomb, m_bombSpawnPoint;

    private int m_maxBombs = 1, m_bombsRemaining = 1;
    private float m_bombTimer, m_maxBombTimer = 2f;
    #endregion
    #endregion

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        rb = gameObject.GetComponent<Rigidbody>();
        m_bombsRemaining = m_maxBombs;
        m_bombTimer = m_maxBombTimer;
    }

    #region Input
    public void OnMove(InputAction.CallbackContext context)
    {
        //Wanneer deze actie word uitgevoerd dan beweeegt de speler.
        if (context.performed)
        {
            int index = scene.buildIndex;
            if (index == 3) // (K) als de scene op QTE game is dan word movement weggehaalt
            {
                QTEmanager qTEmanager = FindAnyObjectByType<QTEmanager>();
                UnityEngine.Vector2 currentMove = context.ReadValue<UnityEngine.Vector2>();
                if (currentMove == new UnityEngine.Vector2(0, 1)) qTEmanager.playerChosenInput[1] = 0; //Input Up
                else if (currentMove == new UnityEngine.Vector2(0, -1)) qTEmanager.playerChosenInput[1] = 1; //Input Down
                else if (currentMove == new UnityEngine.Vector2(-1, 0)) qTEmanager.playerChosenInput[1] = 2; //Input Left
                else if (currentMove == new UnityEngine.Vector2(1, 0)) qTEmanager.playerChosenInput[1] = 3; //Input Right
            }
            else
            {
                movementInput = context.ReadValue<UnityEngine.Vector2>();
            }
        }        
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        //Wanener deze actionmap word uitgevoerd dan gebeurt de bij behorende actie
        if (context.performed)
        {
            int index = scene.buildIndex;
            if (index == 1)
            {
                Vote();
            }

            if (index == 2)
            {
                /*                if ()*/
                SpawnBomb.SpawningBombs(m_bomb, m_bombSpawnPoint.transform.position);
            }
        }
    }
    #endregion

    private void Vote()
    {
        //Als de speler colission heeft met een object dat de Portal tag heeft en de speler nog kan stemmen dan stemt de speler op een van de portals.
        //En neemt de hoeveelheid stemmen dat de speler heeft af.
        if (m_portals != null && m_voteCount == 1 && m_canVote)
        {
            m_portals.GetComponent<Portals>().m_AmountOfVotes++;
        }
        m_voteCount--;
    }

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
        UnityEngine.Vector3 move = new UnityEngine.Vector3(movementInput.x, 0, movementInput.y).normalized * playerSpeed;
        UnityEngine.Vector3 newPosition = rb.position + move * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        //Als de speler niet de maximale hoeveelheid bommen heeft dan loopt er een timer af als die op 0 staat krijgt de speler er een bom bij.
        if (m_bombsRemaining < m_maxBombs)
        {
            m_bombTimer -= Time.fixedDeltaTime;
            if(m_bombTimer <= 0)
            {
                m_bombTimer = m_maxBombTimer;
                m_bombsRemaining++;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Checkt of de speler collision heeft met een object dat de portal tag heeft.
        if (collision.gameObject.CompareTag("Portal"))
        {
            m_canVote = true;
            if (m_portals == null)
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


    //De spelers health variabel neemt af met 1.
    public void TakeDamage()
    {
        m_health--;

        //Als de speler geen health meer over heeft gaat die dood.
        if (m_health == 0)
        {
            Destroy(gameObject);
        }
    }

}
