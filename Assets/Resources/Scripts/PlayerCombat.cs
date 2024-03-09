using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private GameObject bulletPrefab;
    private GameObject bulletClone;
    private float bulletSpeed = 25f;
    
    
    private float yOffset = 0.5f;
    private Transform player;
    private float attackStartTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = GameObject.Find("Bullet");
        player = transform;
    }

    void FireBullet()
    {
        Vector2 bulletOffset = new Vector2(0f, yOffset);
        Vector2 bulletPosition = (Vector2)(transform.position + transform.TransformDirection(bulletOffset));
        bulletClone = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        Rigidbody2D bulletRb = bulletClone.GetComponent<Rigidbody2D>();
        Vector3 bulletDirection = transform.forward;
        bulletRb.velocity = transform.up * bulletSpeed;

        Bullet bulletScript = bulletClone.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.StartDestroyCoroutine();
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && Time.time > attackStartTime)
        {
            FireBullet();
        }
    }
}
