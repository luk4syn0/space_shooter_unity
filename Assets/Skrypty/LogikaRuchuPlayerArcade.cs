using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LogikaRuchuPlayerArcade : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Wartość przyspieszenia")]
    [Range(0.0f,30.0f)]
    public float boost = 0.0f;
    
    private GameObject Starship;
    private Rigidbody2D rb;
    
    InputAction moveActionUp;
    InputAction moveActionDown;

    InputAction moveActionLeft;
    InputAction moveActionRight;

    
    
    Vector3 mouse_pos;
    Transform target;
    Vector3 object_pos;
    float angle;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Starship = GameObject.Find("Starship");
        target = Starship.transform;
        
        rb = GetComponent<Rigidbody2D>();
        
        moveActionUp = InputSystem.actions.FindAction("MoveUp");
        moveActionDown = InputSystem.actions.FindAction("MoveDown");
        moveActionLeft = InputSystem.actions.FindAction("MoveLeft");
        moveActionRight = InputSystem.actions.FindAction("MoveRight");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RuchGracza();

        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f;
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x -= object_pos.x;
        mouse_pos.y -= object_pos.y;
        // angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
        // transform.rotation = Quaternion.Euler(0, 0, angle);
        // Obecny kąt rotacji
        
        float targetAngle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
        Quaternion currentRotation = transform.rotation;

        // Docelowy kąt rotacji
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Płynna rotacja z prędkością rotacji
        float rotationSpeed = 200f; 
        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void RuchGracza()
    {
        if (moveActionUp.IsPressed())
        {
            rb.AddForce(Vector2.up * boost);
        }
        
        if (moveActionDown.IsPressed())
        {
            rb.AddForce(Vector2.down * boost);
        }

        if (moveActionLeft.IsPressed())
        {
            rb.AddForce(Vector2.left * boost);
        }
        
        if (moveActionRight.IsPressed())
        {
            rb.AddForce(Vector2.right * boost);
        }
        
    }
}
