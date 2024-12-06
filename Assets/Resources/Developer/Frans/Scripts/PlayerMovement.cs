using JetBrains.Annotations;
using MiniGames.Combat;
using MiniGames.QuickTimeEvent;
using System.Numerics;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using System.Linq;


public class PlayerMovement : MonoBehaviour
{
    #region Variables
    #region Universal Variables
    [SerializeField] 
    public int m_whichPlayer = 0;

    [SerializeField]
    private GameObject[] m_DuckChild;

    [SerializeField]
    public int m_playerID;
    public int m_health = 3;
    public bool m_playerOut;

    //<-- Movement -->
    private UnityEngine.Vector2 m_movementInput = UnityEngine.Vector2.zero;
    private float m_rotateDirection;
    private float m_playerSpeed = 20f;
    private float m_rotationSpeed = 50f;
    //<- End Movement ->


    //Rigidbody
    Rigidbody m_rb;
    #endregion
    #region Voting
    [CanBeNull]
    [SerializeField]
    private GameObject m_portals;

    //variablen voor het stemmen op je gameMode
    private bool m_canVote = false;
    [SerializeField]
    private int m_voteCount = 1;
    private Voting m_voting;
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
        m_voting = FindObjectOfType<Voting>();
        m_rb = gameObject.GetComponent<Rigidbody>();
        m_bombsRemaining = m_maxBombs;
        m_bombTimer = m_maxBombTimer;
        DontDestroyOnLoad(gameObject);
    }

    #region Input
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed && !m_playerOut)
        {
            GameManager.Instance.CheckScene();
            if (GameManager.Instance.m_index == 3) // (K) als de scene op QTE game is dan word movement weggehaalt
            {
                QTEmanager qTEmanager = FindAnyObjectByType<QTEmanager>();

                UnityEngine.Vector2 currentMove = context.ReadValue<UnityEngine.Vector2>();
                if (currentMove == new UnityEngine.Vector2(0, 1)) qTEmanager.playerChosenInput[m_whichPlayer] = 0; //Input Up
                else if (currentMove == new UnityEngine.Vector2(0, -1)) qTEmanager.playerChosenInput[m_whichPlayer] = 1; //Input Down
                else if (currentMove == new UnityEngine.Vector2(-1, 0)) qTEmanager.playerChosenInput[m_whichPlayer] = 2; //Input Left
                else if (currentMove == new UnityEngine.Vector2(1, 0)) qTEmanager.playerChosenInput[m_whichPlayer] = 3; //Input Right
            }
            else
            {
                m_movementInput = context.ReadValue<UnityEngine.Vector2>();
            }
        }        
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        GameManager.Instance.CheckScene();
        if (GameManager.Instance.m_index == 3)
        {

        }

        else if (context.performed && !m_playerOut)
        {
            m_rotateDirection = context.ReadValue<float>();
        }
        else if (!context.performed)
        {
            m_rotateDirection = 0;
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && !m_playerOut)
        {
            GameManager.Instance.CheckScene();
            if (GameManager.Instance.m_index == 1)
            {
                Vote();
            }

            else if (GameManager.Instance.m_index == 2)
            {
                if(m_bombsRemaining > 0)
                {
                    SpawnBomb.SpawningBombs(m_bomb, m_bombSpawnPoint.transform.position);
                    m_bombsRemaining--;
                }
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
            m_voting.g_totalVotes++;
            m_portals.GetComponent<Portals>().m_AmountOfVotes++;
            m_voteCount--;
        }
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
        PlayerMove();
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

    private void PlayerMove()
    {
        UnityEngine.Vector3 move = new UnityEngine.Vector3(m_movementInput.x, 0, m_movementInput.y).normalized * m_playerSpeed;
        UnityEngine.Vector3 newPosition = m_rb.position + move * Time.fixedDeltaTime;
        m_rb.MovePosition(newPosition);
        transform.Rotate(UnityEngine.Vector3.up * Time.deltaTime * m_rotationSpeed * m_rotateDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region PlayerHub
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
        #endregion
    }

    #region Reusable functions
    public void PlayerOff()
    {
        m_DuckChild[m_DuckChild.Length - 1].SetActive(false);
    }

    public void PlayerOn()
    {
        for (int i = 0; i < 10; i++)
        {
            m_DuckChild[m_DuckChild.Length - 1].SetActive(true);
        }
        m_playerOut = false;
    }

    //De spelers health variabel neemt af met 1.
    public void TakeDamage()
    {
        m_health--;
        //Als de speler geen health meer over heeft gaat die dood.
        if (m_health == 0)
        {
            GameManager.Instance.PlayerEliminated();
        }

        else if(m_health > 0)
        {
            StartCoroutine(HealthFlash());
        }
    }

    private IEnumerator HealthFlash()
    {
        for (int i = 0; i < 10; i++)
        {
            m_DuckChild[m_DuckChild.Length -1].SetActive(false);
            yield return new WaitForSeconds(0.1f);
            m_DuckChild[m_DuckChild.Length -1].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion
}
