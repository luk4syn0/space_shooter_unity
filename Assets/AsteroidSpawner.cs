using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class AsteroidSpawner : MonoBehaviour
{
    public Transform spawnPointTopLeft;
    public Transform spawnPointTopRight;
    public Transform spawnPointBottomLeft;
    public Transform spawnPointBottomRight;

    public GameObject asteroidPrefab;

    private float timer;

    private float xPos;
    private float yPos;
    private Vector3 objectPosition;
    

    private Random rnd;
    
    public int mode;
    /* 0. w trakcie walki z obcymi
     * 1. etap przetrwania
     */
    
    private float spawnSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        rnd = new Random();
        spawnSpeed = mode == 0 ? 10f : 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnSpeed)
        {
            timer = 0f;
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        int side = rnd.Next(1, 4);
        if (side == 1)
        {
            var position = spawnPointTopLeft.position; //powtorzone odwolanie, no ok, można to do start ewentualnie
            xPos = rnd.Next((int)position.x, (int)spawnPointTopRight.position.x);
            objectPosition = new Vector3(xPos, position.y, 0);
            Instantiate(asteroidPrefab, objectPosition, Quaternion.identity);
        }
        else if(side == 2)
        {
            var position = spawnPointBottomLeft.position;
            xPos = rnd.Next((int)position.x, (int)spawnPointBottomRight.position.x);
            objectPosition = new Vector3(xPos, position.y, 0);
            Instantiate(asteroidPrefab, objectPosition, Quaternion.identity);
        }
        else
        {
            var position = spawnPointTopRight.position;
            yPos = rnd.Next((int)spawnPointBottomRight.position.y, (int)position.y);
            objectPosition = new Vector3(position.x, yPos, 0);
            Instantiate(asteroidPrefab, objectPosition, Quaternion.identity);
        }
        
    }
        
}
