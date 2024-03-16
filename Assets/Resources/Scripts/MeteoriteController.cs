using UnityEngine;
using UnityEngine.XR;

public abstract class MeteoriteController : MonoBehaviour
{
    public float floatSpeed;
    Camera mainCamera;
    protected Vector2 randomDirection;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        randomDirection = Random.insideUnitCircle.normalized;
        
    }

    protected virtual void Update()
    {
        Move();
        WrapAroundScreen();
    }

    protected void Move()
    {
        rb.velocity = randomDirection * floatSpeed;
    }

    protected void WrapAroundScreen()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        // If the object is outside of the viewport, wrap it to the opposite side
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Vector3 newPosition = transform.position;

            // Wrap horizontally
            if (viewPos.x < 0)
                newPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(1, viewPos.y, viewPos.z)).x;
            else if (viewPos.x > 1)
                newPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(0, viewPos.y, viewPos.z)).x;

            // Wrap vertically
            if (viewPos.y < 0)
                newPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(viewPos.x, 1, viewPos.z)).y;
            else if (viewPos.y > 1)
                newPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(viewPos.x, 0, viewPos.z)).y;

            transform.position = newPosition;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Handle collision with bullet
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}