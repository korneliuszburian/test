using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    public float maxHealth;
    public float height;

    private float health;

    void Start()
    {
        health = maxHealth;
        bar.localScale = new Vector2(1f, height);
    }

    public void SetSize(float size) {
        bar.localScale = new Vector2(size, height);
    }

    public void SetHealth(float health) {
        this.health = health;
    }

    void FixedUpdate()
    {
        SetSize(health / maxHealth);
    }
}
