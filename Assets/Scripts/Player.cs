using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem runParticle;

    [SerializeField] private ParticleSystem immortalParticle;

    [SerializeField] private bool isJumping;

    [SerializeField] private bool wantsToJump;

    public bool isGameStarted;

    public bool isUntouchable;

    public AudioSource coinFX;

    public float moveSpeed = 15;

    public float jumpPower;

    public int laneIndex=1;

    public float horizontalSpeed;

    public bool solverBool;

    public bool isDead;

    public GameObject gameOverPanel;

    public GameObject[] lanes;

    public Rigidbody rb;

    public Animator animator;

    public bool immortalMode;

    public int jumpCount = 0;

    public static Player Instance;

   // public GameObject player; 

    private Distance distance;

    private CoinManager coinManager;

    private int deadCount = 0;

    private bool isGrounded;

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

        runParticle.transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);

        if (distance.score % 100 == 0 && !solverBool)
        {
            moveSpeed +=1f ;
            distance.addDistanceTime -= 0.01f;
            solverBool = true;
        }

        if (solverBool == true && distance.score %100 != 0)
        {
            solverBool = false;
        }

        if (coinManager.coinCount >=20)
        {
            if ((deadCount == 0 && coinManager.coinCount >= 20) || (deadCount > 0 && coinManager.coinCount > (deadCount + 1) * 20))
            {
                immortalMode = true;
            }
            else
                immortalMode = false;
        }
        else
        {
            immortalMode = false;
        }

        if (immortalMode)
        {
            immortalParticle.Play();
        }
        else
        {
            immortalParticle.Stop();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.left)
        {
            MoveLeft();
            SwipeManager.left = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)||SwipeManager.right)
        {
            MoveRight();
            SwipeManager.right = false;
        }

        if (transform.position.y > 1.8f)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)|| SwipeManager.down)
            {
                StartCoroutine(Fall());
                SwipeManager.down = false;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow)||SwipeManager.jump)
        {
            // rb.velocity = Vector3.up * jumpPower* Time.deltaTime;
            wantsToJump = true;
            SwipeManager.jump = false;
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
                //jumpCount++;
                StartCoroutine(Jump());
                isJumping = true;

            }
            wantsToJump = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
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

                if (immortalMode) //coinManager.coinCount >= 10 * deadCount)
                {
                    Destroy(collision.gameObject);
                    coinManager.coinCount -= 20 * deadCount;
                }
                else
                {
                   StartCoroutine(onDeath());
                    //SceneManager.LoadScene("SampleScene");
                }
            }  
        }
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            // isGrounded = true;

            // runParticle.Play();
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
        rb.AddForce(0, -jumpPower*2*Time.deltaTime, 0,ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isJumping", false);
    }

    private void MoveLeft()
    {
        if (laneIndex > 0)
        {
            laneIndex--;
        }
    }
    private void MoveRight()
    {
        if (laneIndex < 2)
        {
            laneIndex++;
        }
    }
}
