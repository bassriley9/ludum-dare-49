using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;
    public float jumpPower;
    public GameObject ClosedDoor;

    private void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }




void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        //relative direction based on location and direction
        Vector3 direction = transform.right * x + transform.forward * Z; ;
        direction *= moveSpeed;

        //gravity
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }
    void Jump()
    {
   
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        
    }
    //checking grounded using a raycast

    bool canJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 0.5f))
        {
            return hit.collider != null;
        }
        else
            return false;
    }

}
