using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float moveSpeed = 15;

    public float jumpPower;

    public int laneIndex=1;

    public float horizontalSpeed;

    public bool solverBool;

    public bool isDead;

    public GameObject player;

    public GameObject gameOverPanel;

    public GameObject[] lanes;

    public Rigidbody rb;

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
        coinManager = CoinManager.Instance;

        distance = Distance.Instance;

        Time.timeScale = 1;

        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, lanes[laneIndex].transform.position, horizontalSpeed*Time.fixedDeltaTime);

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


        if (transform.position.y > 1.8f)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //rb.velocity = Vector3.down * jumpPower * Time.deltaTime;
                rb.AddForce(0, -700, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            if (laneIndex>0)
            {
                laneIndex--;
            }
            /*
           if(desiredLane == -3)
           {
               desiredLane = -3;
           }
           else
           {
               desiredLane -= 3;
           }

             player.transform.position = Vector3.Lerp(transform.position, new Vector3(desiredLane, transform.position.y, transform.position.z), 5f);
            */
            //  player.transform.position = new Vector3(desiredLane, transform.position.y, transform.position.z);

            /*
            if(this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
               // transform.position = Vector3.Lerp(transform.position, new Vector3(-40f, transform.position.y, transform.position.z), Time.deltaTime);
            }
            */
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (laneIndex <2)
            {
                laneIndex++;
            }

            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
               // rb.velocity = Vector3.up * jumpPower* Time.deltaTime;
                rb.AddForce(0, jumpPower, 0);
            }
        }
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Engel"))
        {
            deadCount++;

            if(coinManager.coinCount >=20*deadCount)
            {
                Destroy(collision.gameObject);
                coinManager.coinCount -= 20*deadCount;
            }
            else
            {
                onDeath();
                //SceneManager.LoadScene("SampleScene");
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinManager.coinCount++;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("MagnetActivator"))
        {
            Destroy(other.gameObject);
           // other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Magnet.isMagnetActive = true;
            /*
            StartCoroutine(ActivateMagnet());
            Magnet.isMagnetActive = false;*/
        }
    }
    public IEnumerator ActivateMagnet()
    {
        yield return new WaitForSecondsRealtime(8f);
    }
    public void onDeath()
    {
        isDead = true;

        Time.timeScale = 0;

        gameOverPanel.SetActive(true);

    }
}
