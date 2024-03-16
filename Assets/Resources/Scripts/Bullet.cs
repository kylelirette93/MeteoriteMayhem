using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", destroyDelay);
    }

    // Update is called once per frame
   void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
