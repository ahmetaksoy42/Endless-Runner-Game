using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScriptFixed : MonoBehaviour
{

    public Transform target;

    private CoinManager coinManager;

    // Mıknatısın etkisi altındaki nesnelere çekme kuvvetini ayarlayacağımız bir değişken
    public float pullForce = 100f;

    // Mıknatısın sabit bir noktasını belirleyeceğimiz bir değişken
    public Transform fixedPoint;
    private void Start()
    {
        coinManager = CoinManager.Instance;

    }

    // Update metodu, her bir kare (frame) için bir kez çalışır
    void Update()
    {
        // Etkisi altındaki nesnelere çekme kuvvetini uygulayalım
        foreach (Collider collider in Physics.OverlapSphere(fixedPoint.position, 8f))
        {
            
            // Eğer nesne bir Rigidbody component'i içermiyorsa devam et
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb == null)
            {
                continue;
            }

            // Nesneye çekme kuvvetini doğru yönde ve güçlü bir şekilde uygulayalım
            if (collider.gameObject.CompareTag("Coin"))
            {
                Vector3 direction = (fixedPoint.position - rb.position).normalized;
                rb.AddForce(direction * pullForce, ForceMode.Force);

            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject, 1f);
            coinManager.coinCount++;
        }
    }
}


