using UnityEngine;

public class SmallMeteoriteController : MeteoriteController
{
    public float maxSpeed = 4f;
    private CircleCollider2D circleCollider;
    private GameManager gameManagerScript;
    private bool alreadyHit = false;
    public AudioClip explosionSound;
    private GameObject bullet;
    bool isDestroyed = false;

    void Awake()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        bullet = GameObject.FindWithTag("Bullet");
        Invoke("EnableCollider", 0.2f);
    }

    void EnableCollider()
    {
        circleCollider.enabled = true;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // Call the base method first for common collision handling

        if (!isDestroyed && !alreadyHit)
        {
            GameObject collidedObject = collision.gameObject;

            if (collidedObject.CompareTag("Bullet"))
            {
                DestroyMeteorite();
                gameManagerScript.IncrementScore(200);
                alreadyHit = true;
            }
            else if (collidedObject.CompareTag("BigMeteorite") || collidedObject.CompareTag("SmallMeteorite"))
            {
                LimitSpeed();
            }
        }
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
    }
}