using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public Rigidbody projectile;
    public float projectileSpeed = 0;

    private Rigidbody playerBall;
    private float movementX;
    private float movementY;
    private int count;
    private float destroyDelay = .5f;

    // Start is called before the first frame update
    void Start()
    {
        playerBall = GetComponent<Rigidbody>();
        count = 0;

        winTextObject.SetActive(false);
        SetCountText();
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Rigidbody shoot = Instantiate(projectile, transform.position, transform.rotation);
            shoot.velocity = transform.forward * projectileSpeed;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        playerBall.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count;

        if (count >= 14) winTextObject.SetActive(true);
    }

}
