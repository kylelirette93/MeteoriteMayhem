using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float rotationSpeed = 300f;
    private float flySpeed = 6f;
    private Animator anim;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WrapAroundScreen();

        float zRotation = Input.GetAxis("Horizontal");

        float rotationAmount = zRotation * rotationSpeed * Time.deltaTime;

        float yInput = Input.GetAxis("Vertical");



        if (Mathf.Abs(zRotation) > 0)
        {
            transform.Rotate(-Vector3.forward, rotationAmount);
        }

        if (Mathf.Abs(yInput) > 0)
        {
            anim.SetBool("isFlying", true);
            transform.Translate(new Vector3(0, yInput * flySpeed * Time.deltaTime, 0));
        }

        else
        {
            anim.SetBool("isFlying", false);
        }


    }

    void WrapAroundScreen()
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
}
