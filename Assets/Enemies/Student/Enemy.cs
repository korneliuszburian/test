using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public Rigidbody2D rb;
    public float health;
    public HealthBar healthBar;
    public Player player;
    public float moveSpeed;
    public float maxRandomDistance;
    public float randomDestinationTimeOutSec;

    private bool isOnRandomDestination = false;
    private float randomDestinationTime;
    private Vector2 randomDestination;

    public void takeDamage(float damage) {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void EscapeFromPlayer() {
        Vector2 playerPosition = player.transform.position;
        Vector2 enemyPosition = rb.position;
        Vector2 direction = enemyPosition - playerPosition;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed).normalized;
    }

    void createRandomDestination() {
        isOnRandomDestination = true;
        randomDestinationTime = Time.time;

        float randomX = Random.Range(-maxRandomDistance, maxRandomDistance);
        float randomY = Random.Range(-maxRandomDistance, maxRandomDistance);
        randomDestination = new Vector2(rb.position.x + randomX, rb.position.y + randomY);
    }

    void handleRandomDestination() {
        if (Vector2.Distance(rb.position, randomDestination) < 0.1f) {
            // reached destination
            isOnRandomDestination = false;
            return;
        }

        if (Time.time - randomDestinationTime > randomDestinationTimeOutSec) {
            // random destination timeout
            isOnRandomDestination = false;
            return;
        }

        Vector2 enemyPosition = rb.position;
        Vector2 direction = randomDestination - enemyPosition;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed).normalized;
    }

    void FixedUpdate()
    {
        if (isOnRandomDestination) {
            handleRandomDestination();
            return;
        }

        if (Random.Range(0, 100) == 1) {
            createRandomDestination();
            return;
        }

        EscapeFromPlayer();
    }

}
