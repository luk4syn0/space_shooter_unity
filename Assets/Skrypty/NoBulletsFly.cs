using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBulletsFly : MonoBehaviour
{
    private Rigidbody2D bullet;
    private GameObject thisObject;
    // public LayerMask ignoreThisLayer; //Ignorowanie tej warstwy (gracza)
    
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        thisObject = gameObject;
        
        // wallOfAnnihilation = GameObject.Find("GraniceMapy").GetComponent<Collider>();
        bullet.AddForce(bullet.transform.up * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        //Zbyt szybkie pociski dla fizyki, test raycastu
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, bullet.velocity.normalized, bullet.velocity.magnitude * Time.deltaTime, ignoreThisLayer);
        //
        // if (hit.collider != null)
        // {
        //     Destroy(thisObject);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(thisObject);
        print("Collision with" + other.ToString());
        
    }
}
