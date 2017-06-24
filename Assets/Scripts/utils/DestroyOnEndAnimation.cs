using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class DestroyOnEndAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroySelf(){
        Object.Destroy(this.gameObject);
    }

}

}