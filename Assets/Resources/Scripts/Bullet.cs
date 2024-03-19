using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 1.0f;
    
    void Start()
    {
        Invoke("DestroyBullet", destroyDelay);
    }
   
   void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
