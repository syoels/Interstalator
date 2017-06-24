using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class SpaceshipWindow : MonoBehaviour {

    // Aliens related
    [SerializeField] int aliens = 0; 
    [SerializeField] int maxAliens = 4; 
    public GameObject alienPrefab; 
    [SerializeField] List<GameObject> aliensList; 
    private GameObject aliensParent; 

    // Animation related
    private Animator anim; 
    private int speedParamInt; 
    private int shipDriveParamInt; 
    private int shipStopParamInt; 
    private int alienLeaveParamInt; 
    private float windowSize; 
    private float aliensXDistance;
    private float alienSpawnInterval = 4f;
    Vector3 firstAlienPosition; 

    // Speed
    private float maxSpeed = 1f; 
    private float minSpeed = 0f; 
    [SerializeField] float speed = 0f; 
    private float prevSpeed = 0f; 
    private float speedThreshold = 0.2f;
    public float Speed{
        get{ 
            return speed;
        }
        set{ 
            prevSpeed = speed;
            speed = Mathf.Clamp(value, minSpeed, maxSpeed);
            anim.SetFloat(speedParamInt, speed);
            // Ship stop
            if (prevSpeed > speedThreshold && speed <= speedThreshold) {
                anim.SetTrigger(shipStopParamInt);
                for (int i = 1; i <= maxAliens; i++) {
                    Invoke("AddAlien", i * alienSpawnInterval);
                }
            } 
            // Ship Accelerate
            else if (prevSpeed <= speedThreshold && speed > speedThreshold){
                anim.SetTrigger(shipDriveParamInt);
                CancelInvoke();
                RemoveAliens();
            }
        }
    }


    /// <summary>
    /// Initialize all relavant variables: 
    /// Animator, animator ints, aliens parent object etc. 
    /// </summary>
    void Awake(){
        anim = GetComponent<Animator>();
        speedParamInt = Animator.StringToHash("speed");
        aliensList = new List<GameObject>();
        alienLeaveParamInt = Animator.StringToHash("leave");
        shipDriveParamInt = Animator.StringToHash("drive");
        shipStopParamInt = Animator.StringToHash("stop");
        aliensParent = transform.Find("Graphics").gameObject;
        windowSize = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        aliensXDistance = windowSize / maxAliens - 0.75f; // -0.75 just  in case 
        firstAlienPosition = transform.position - new Vector3(5f, 1f, 0f);
    }


    /// <summary>
    /// Adds an "Alien" child to "Graphics". 
    /// Alien is a prefab with it's own animator. 
    /// </summary>
    private void AddAlien(){
        if (aliens >= maxAliens || speed > speedThreshold) {
            return; 
        }
        GameObject clone = Instantiate(alienPrefab, firstAlienPosition + new Vector3(aliens * aliensXDistance, 0f, 0f), Quaternion.identity) as GameObject;
        clone.transform.parent = aliensParent.transform;
        aliensList.Add(clone);
        aliens++; 
    }


    /// <summary>
    /// removes all aliens from the window
    /// </summary>
    private void RemoveAliens(){
        if (aliens <= 0 || aliensList.Count == 0) {
            return;
        }
        foreach (GameObject alienObj in aliensList) {
            Animator alienAnimator = alienObj.transform.GetComponent<Animator>();
            alienAnimator.SetTrigger(alienLeaveParamInt);
            aliens--;
        }
        aliensList.Clear();
    }


    //TODO: test functions, delete
    public void increaseSpeed(){ Speed += 0.1f; }
    public void decreaseSpeed(){ Speed -= 0.1f; }




}
}

