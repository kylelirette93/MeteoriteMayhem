using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float rotationSpeed = 250f;
    private float flySpeed = 5f;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float zRotation = Input.GetAxisRaw("Horizontal");

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
}
