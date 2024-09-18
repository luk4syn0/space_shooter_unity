using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class LogikaRuchuPlayer : MonoBehaviour
{
    [FormerlySerializedAs("predkoscRuchu")]
    [SerializeField]
    [Tooltip("Wartość przepustnicy")]
    [Range(-1.0f,3.0f)]
    public float throttle = 0.0f;

    public float speed = 0.0f;
    

    private GameObject Starship;
    private Rigidbody2D rb;
    
    //Limity prędkości
    public float minSpeed = -2.0f;
    public float maxSpeed = 2.0f;
    public float boost = 20f;
    
    //Obrót samolotu wokół osi Z
    public float minRotation = 1.5f;
    public float maxRotation = 0.01f;
    public float currentRotation;
    public float drag;
    public float minDrag = 0.1f;
    public float maxDrag = 10.0f;
    

    
    
    
    InputAction moveActionFaster;
    InputAction moveActionSlower;

    InputAction rotateActionLeft;
    InputAction rotateActionRight;

    InputAction engineOff;

    private Vector2 direction;
    private void Start()
    {
        Starship = GameObject.Find("Starship");
        rb = GetComponent<Rigidbody2D>();
        drag = rb.drag;
        
        moveActionFaster = InputSystem.actions.FindAction("SpeedUp");
        moveActionSlower = InputSystem.actions.FindAction("SpeedDown");
        rotateActionLeft = InputSystem.actions.FindAction("RotateLeft");
        rotateActionRight = InputSystem.actions.FindAction("RotateRight");
        engineOff = InputSystem.actions.FindAction("EngineOff");


    }

    private void Update()
    {
        
        RuchPostaci();
        
        print(rb.transform.eulerAngles.z + rb.velocity.ToString());
        
    }

    void FixedUpdate()
    {
        
        // Kąt obrotu wokół osi Z w radianach
        float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        
        // Wektor kierunku wynikający z kąta obrotu, ale wzdłuż osi Y
        direction = new Vector2(Mathf.Sin(angleRad), Mathf.Cos(angleRad));
        
        
        /// Przesunięcie obiektu w kierunku, w którym patrzy wzdłuż osi Y
        // rb.velocity = direction * predkoscRuchu;
        // rb.AddRelativeForce(direction * throttle * 20 * Time.deltaTime);
        speed = rb.velocity.magnitude;
        currentRotation = Mathf.Lerp(minRotation, maxRotation, speed / maxSpeed);

        rb.drag = Mathf.Lerp(minDrag, maxDrag, (speed / maxSpeed * speed / maxSpeed * speed/maxSpeed));
        drag = rb.drag;
        // rb.drag = Mathf.Lerp(minDrag, maxDrag, speed / maxSpeed);
        
        
        
        //Dodaje gazu!
        if (speed < maxSpeed || speed > minSpeed)
        {
            rb.AddForce(direction * throttle * boost * Time.deltaTime);
        }
        
        
        
        
    }

    private void RuchPostaci()
    {
        if (moveActionFaster.IsPressed())
        {
            if (throttle < 3.0f)
                throttle += 0.03f;
        }
        
        if (moveActionSlower.IsPressed())
        {
            if (throttle > -1.0f)
                throttle -= 0.07f;
        }

        if (rotateActionLeft.IsPressed())
        {
            Starship.transform.Rotate(0.0f, 0.0f, -currentRotation, Space.Self);
        }
        
        if (rotateActionRight.IsPressed())
        {
            Starship.transform.Rotate(0.0f, 0.0f, +currentRotation, Space.Self);
        }

        if (engineOff.IsPressed())
        {
            throttle = 0.0f;
        }
    }
}
