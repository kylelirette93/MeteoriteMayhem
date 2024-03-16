using UnityEngine;

public class BigMeteoriteController : MeteoriteController
{
    public float maxSpeed = 2f;
    private GameManager gameManagerScript;
    private bool alreadyHit = false;
    public AudioClip explosionSound;
    public GameObject smallMeteoritePrefab;
    bool isDestroyed = false;

    protected override void Start()
    {
        base.Start(); // Call the base Start method to initialize common variables

        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // Call the base method first for common collision handling

        if (!isDestroyed && collision.gameObject.CompareTag("Bullet"))
        {
            HandleBulletCollision(collision.gameObject);
        }
        else if (!isDestroyed && (collision.gameObject.CompareTag("BigMeteorite") || collision.gameObject.CompareTag("SmallMeteorite")))
        {
            LimitSpeed();
        }
    }

    void HandleBulletCollision(GameObject bullet)
    {
        if (!alreadyHit)
        {
            gameManagerScript.IncrementScore(400);
            alreadyHit = true;
        }

        DestroyMeteorite();
        Destroy(bullet);
    }

    void LimitSpeed()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void DestroyMeteorite()
    {
        Destroy(gameObject);
        isDestroyed = true;

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        InstantiateSmallMeteorites();
    }

    void InstantiateSmallMeteorites()
    {
        if (smallMeteoritePrefab != null)
        {
            Instantiate(smallMeteoritePrefab, transform.position, Quaternion.identity);
            Instantiate(smallMeteoritePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Small meteorite prefab is null!");
        }
    }

    protected override void Update()
    {
        base.Update(); // Call the base Update method for common functionalities
        Move(); // Move the big meteorite
    }
}