using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootBullet : MonoBehaviour

{   InputAction attackShootBullet;

    public Transform shootingPoint;

    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        attackShootBullet = InputSystem.actions.FindAction("Shoot"); //Do poprawy przy missilach
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (attackShootBullet.IsPressed())
        {
            //strzelaj
            Instantiate(bulletPrefab, shootingPoint.position,transform.rotation);
            
        }
    }
}
