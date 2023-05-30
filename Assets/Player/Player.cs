using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb; 
    public Weapon weapon; 
    
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update(){
        ProcessInputs();
    }

    void FixedUpdate(){
        Move();
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1")) {
            weapon.Shoot();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle; 
    }

    // handle wall collisions by not allowing the player to move through walls
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            rb.velocity = Vector2.zero;
        }
    }

}
