using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootBullet : MonoBehaviour

{   InputAction attackShootBullet;

    public Transform shootingPoint;

    public GameObject bulletPrefab;
    
    public float shootingSpeed;
    
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (shootingSpeed == 0f)
        {
            shootingSpeed = 0.75f;
        }

        timer = 2f;
        
        attackShootBullet = InputSystem.actions.FindAction("Shoot"); //Do poprawy przy missilach
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (attackShootBullet.IsPressed())
        {
            if(timer >= shootingSpeed)
            {
                timer = 0f;
                //strzelaj
                Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            }
            
        }
    }
}
