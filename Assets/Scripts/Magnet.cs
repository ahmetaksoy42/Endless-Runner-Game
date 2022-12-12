using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    // Mıknatıs etkisinin ne kadar uzakta bulunan nesneleri çektiğini belirleyen bir değer
    public float magnetRange = 8.0f;

    // Mıknatısın ne kadar güçlü olduğunu belirleyen bir değer
    public float magnetForce = 20.0f;

    // Mıknatısın kaç saniye çalışacağını belirleyen bir değer
    public float magnetDuration = 10.0f;

    // Mıknatısın kaç saniye çalıştığını tutacak bir değişken
    private float elapsedTime = 0.0f;

    public static bool isMagnetActive= false;

    void Update()
    {
        if (isMagnetActive== true)
        {
            if (elapsedTime < magnetDuration)
            {
                // Mıknatısın etkisi altında bulunan nesneleri belirlemek için bir daire oluşturun
                var colliders = Physics.OverlapSphere(transform.position, magnetRange);

                // Tüm etkisi altındaki nesneler için döngü oluşturun
                foreach (var col in colliders)
                {
                    // Etkisi altındaki nesne bir Coin ise onu çek
                    if (col.CompareTag("Coin"))
                    {
                        // Coin'in konumundan karakterin konumuna doğru bir vector oluşturun
                        var direction = (transform.position - col.transform.position).normalized;

                        // Coin'in konumunu karakterin konumuna doğru güncelleyin
                        col.transform.position = Vector3.Lerp(col.transform.position, transform.position, Time.deltaTime * magnetForce);

                        Destroy(col.gameObject, 1f);
                    }
                }
                elapsedTime += Time.deltaTime;
                if (elapsedTime>=10)
                {
                    isMagnetActive = false;
                    elapsedTime = 0;

                }

            }
            
        }
        
    }
}
