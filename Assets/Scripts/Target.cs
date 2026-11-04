using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Target : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private Vector3 startingLocation;
    [FormerlySerializedAs("moveToLocation")] public Vector3 moveToOffset;
    public float speed = 5f;
    private bool movingTo = true;

    void Start()
    {
        currentHealth = maxHealth;
        startingLocation = transform.position;
    }

    private void Update()
    {
        if (movingTo)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingLocation + moveToOffset, speed * Time.deltaTime);
            if (transform.position == startingLocation + moveToOffset)
                movingTo = !movingTo;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startingLocation, speed * Time.deltaTime);
            if (transform.position == startingLocation)
                movingTo = !movingTo;
        }
    }

    public void Hit()
    {
        if (currentHealth <= 0) return;
        currentHealth--;
        if (currentHealth > 0)
            transform.localScale *= 0.9f;
        else
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}