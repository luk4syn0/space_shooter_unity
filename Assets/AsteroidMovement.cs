using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AsteroidMovement : MonoBehaviour
{
    /*
    -1 Dokleić punkty w których asteroidy dzielą się na mniejsze.
    -2 Jeżeli asteroida mniejsza niż x to znika po kontakcie z pociskiem, 
        a jeżeli dotyka granicy świata to znika niezależnie od rozmiaru.
    */

    
    private Rigidbody2D asteroid;
    private GameObject thisObject;
    private Vector3 direction;
    private Random rnd;
    private int destinationPointY;
    private Vector3 destinationPoint;
    
    private Transform destinationPointTop;
    private Transform destinationPointBottom;
    
    private SceneHandler sceneHandler;

    public int durability;
    
    // Start is called before the first frame update
    void Start()
    {
        asteroid = GetComponent<Rigidbody2D>();
        thisObject = gameObject;
        rnd = new Random();
        
        sceneHandler = GameObject.FindWithTag("EventSystem").GetComponent<SceneHandler>();

        durability = 2; //Ile pocisków przyjmie
        
        destinationPointTop = GameObject.Find("DestinationPointTop").transform;
        destinationPointBottom = GameObject.Find("DestinationPointBottom").transform;

        var position = destinationPointBottom.position; //Usunięcie nadmiarowych podstawień itd.
        destinationPointY = rnd.Next((int)position.y, (int)destinationPointTop.position.y);
        destinationPoint = new Vector3(position.x, destinationPointY,0);

        var asteroidTransform = asteroid.transform;
        var transformPosition = asteroidTransform.position;
        asteroidTransform.right = destinationPoint - transformPosition;
        direction = (destinationPoint - transformPosition).normalized;
        asteroid.AddRelativeForce(direction * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * 16f * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapEdgesAsteroids"))
        {
            Destroy(thisObject);
        }
        
        if (other.CompareTag("PlayerBullet"))
        {
            if (durability == 2)
            {
                durability = 1;
            }
            else
            {
                Destroy(thisObject);
                sceneHandler.combatPoints += 100;
            }
            
        }
    }
}
