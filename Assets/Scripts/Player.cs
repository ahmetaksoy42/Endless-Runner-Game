using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem runParticle;

    [SerializeField] private ParticleSystem immortalParticle;

    public bool isGameStarted;

    public bool isUntouchable;

    public AudioSource coinFX;

    public float moveSpeed = 15;

    public float jumpPower;

    public int laneIndex=1;

    public float horizontalSpeed;

    public bool solverBool;

    public bool isDead;

    private bool isJumping;

    private bool wantsToJump;

   // public GameObject player;

    public GameObject gameOverPanel;

    public GameObject[] lanes;

    public Rigidbody rb;

    public Animator animator; 

    public bool immortalMode;

    public static Player Instance;

    private Distance distance;

    private CoinManager coinManager;

    private int deadCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");

        isGameStarted = false;

        Magnet.isMagnetActive = false;

        coinManager = CoinManager.Instance;

        distance = Distance.Instance;

        Time.timeScale = 1;

        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameStarted==false)
            return;

        transform.position = Vector3.Lerp(transform.position, lanes[laneIndex].transform.position, horizontalSpeed*Time.deltaTime);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);

        if (distance.score % 100 == 0 && !solverBool)
        {
            moveSpeed +=0.5f ;
            distance.addDistanceTime -= 0.01f;
            solverBool = true;
        }

        if (solverBool == true && distance.score %100 != 0)
        {
            solverBool = false;
        }

        if (coinManager.coinCount >= 10 * deadCount)
        {
            if ((deadCount == 0 && coinManager.coinCount >= 10) || (deadCount > 0 && coinManager.coinCount > (deadCount + 1) * 10))
            {
                immortalMode = true;
            }               
        }
        else
        {
            immortalMode = false;
        }

        if (immortalMode)
        {
            immortalParticle.Play();
            Debug.Log("You are immortal");
        }
        else
        {
            immortalParticle.Stop();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (laneIndex>0)
            {
                laneIndex--; 
            } 
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (laneIndex <2)
            {
                laneIndex++;
            }
        }

        if (transform.position.y > 1.8f)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(Fall());
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // rb.velocity = Vector3.up * jumpPower* Time.deltaTime;

            wantsToJump = true;

            //StartCoroutine(Jump());
            // Invoke("Fall", 0.5f);
        }

        
    }
    private void FixedUpdate()
    {
        if (wantsToJump)
        {
            if (!isJumping)
            {
                isJumping = true;
                StartCoroutine(Jump());
            }
            wantsToJump = false;

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            /*
            if (Input.GetKey(KeyCode.UpArrow))
            {
                // rb.velocity = Vector3.up * jumpPower* Time.deltaTime;

                isJumping = false;

                StartCoroutine(Jump());
                // Invoke("Fall", 0.5f);
            }
            */
        }
        
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            if (isUntouchable == true)
            {
                Physics.IgnoreCollision(collision.collider.GetComponent<Collider>(), GetComponent<Collider>());
            }

            else
            {
                deadCount++;

                if (coinManager.coinCount >= 10 * deadCount)
                {
                    Destroy(collision.gameObject);
                    coinManager.coinCount -= 10 * deadCount;
                }
                else
                {
                   StartCoroutine(onDeath());
                    //SceneManager.LoadScene("SampleScene");
                }
            }

            
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinFX.Play();
            coinManager.coinCount++;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("MagnetActivator"))
        {
            var playerTransform = new Vector3(transform.position.x,transform.position.y + 2,transform.position.z);
            other.transform.parent = transform;
            other.transform.localScale = other.transform.localScale / 2;
            other.transform.position = playerTransform;
            Magnet.isMagnetActive = true;
            Destroy(other.gameObject,15f);
           // other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            
            /*
            StartCoroutine(ActivateMagnet());
            Magnet.isMagnetActive = false;*/
        }
    }
    public IEnumerator MagnetTime()
    {
        yield return new WaitForSeconds(8f);
    }
    public IEnumerator onDeath()
    {
        horizontalSpeed = 0;
        moveSpeed = 0;
        isDead = true;
        runParticle.Stop();
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public IEnumerator Jump()
    {
        animator.SetBool("isJumping", true);
        rb.AddForce(Vector3.up * jumpPower*Time.deltaTime,ForceMode.Impulse);
        // rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);

        runParticle.Stop();
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isJumping", false);
        yield return new WaitForSeconds(1.2f);
        runParticle.Play();
    }
    public IEnumerator Fall()
    {
        rb.AddForce(0, -jumpPower*3*Time.deltaTime, 0,ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isJumping", false);
    }
}
