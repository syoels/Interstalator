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
    private List<GameObject> aliensList; 
    private GameObject aliensParent; 

    // Animation related
    private Animator anim; 
    private int speedParamInt; 
    private int shipDriveParamInt; 
    private int shipStopParamInt; 
    private int alienLeaveParamInt; 

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
            } 
            // Ship Accelerate
            else if (prevSpeed <= speedThreshold && speed > speedThreshold){
                anim.SetTrigger(shipDriveParamInt);
            }
        }
    }


    void Awake(){
        anim = GetComponent<Animator>();
        speedParamInt = Animator.StringToHash("speed");
        alienLeaveParamInt = Animator.StringToHash("leave");
        shipDriveParamInt = Animator.StringToHash("drive");
        shipStopParamInt = Animator.StringToHash("stop");
        aliensParent = transform.Find("Graphics").gameObject;
    }

    private void AddAlien(){
        if (aliens >= maxAliens) {
            return; 
        }
        var clone = Instantiate(alienPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        clone.transform.parent = aliensParent.transform;
        aliensList.Add(clone);
        aliens++; 
    }

    private void RemoveAlien(){
        if (aliens <= 0) {
            return;
        }
        GameObject removeAlien = aliensList[0];
        Animator alienAnimator = removeAlien.transform.GetComponent<Animator>();
        alienAnimator.SetTrigger(alienLeaveParamInt);
        aliensList.RemoveAt(0);
        //Destroy(removeAlien); //TODO: replace with object pool? or maybe fixed number of aliens disabled & re-enabled ? 
        aliens--;
    }


    //TODO: test functions, delete
    public void increaseSpeed(){ Speed += 0.1f; }
    public void decreaseSpeed(){ Speed -= 0.1f; }




}
}

