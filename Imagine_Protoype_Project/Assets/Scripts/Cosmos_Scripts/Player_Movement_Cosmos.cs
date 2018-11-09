﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement_Cosmos : MonoBehaviour {

    public bool PC;
    public bool JourneyStarted;

    public bool trail;
    public float trailMod;

    float acc;

    float playerHeight;
    float playerStartHeight;
    public float cameraZoomStart;
    public float risingSpeed = 0;

    public float cameraZoomSpeed;
    public float tiltSpeed;

    private bool loadingScene;

    public float timeToSwitch;
    float timer;

    Camera cam;

    Rigidbody2D rb;


	void Start () {
        playerStartHeight = transform.position.y;
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        playerHeight = transform.position.y;

        if (JourneyStarted) {
            if (PC)
            {
                print(rb.velocity.y);
                if(trail == false){
                    float hAxis = Input.GetAxis("Horizontal");
                    if(acc + risingSpeed > risingSpeed){
                        acc -= Time.fixedDeltaTime * 0.75f;
                    }
                    rb.velocity = new Vector2(hAxis * tiltSpeed, risingSpeed + acc);
                }else{
                    float hAxis = Input.GetAxis("Horizontal");
                    if(acc + risingSpeed < risingSpeed * trailMod){
                        acc += Time.fixedDeltaTime * 0.75f;
                    }
                    rb.velocity = new Vector2(hAxis * tiltSpeed * trailMod, risingSpeed + acc);
                }
                
            }
            else
            {
                rb.velocity = new Vector2(Input.acceleration.x * tiltSpeed, risingSpeed);
            }

            if (cameraZoomStart <= playerHeight - playerStartHeight)
            {
                cam.orthographicSize += cameraZoomSpeed;
            }

            //changed to fixedDeltaTime because you are using fixed update. 
            //timer += Time.fixedDeltaTime;

            if (timer > timeToSwitch) {
                if (!loadingScene)
                {
                    
                    //SceneManager.LoadScene("Map_Scene");
                    Game_Manager.Instance.LoadScene("Map_Scene");

                    loadingScene = true;
                }

            }


        }
	}
}
