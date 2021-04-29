using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // return true if source object can move after activation
    public virtual bool Activate(GameObject sourceObject = null) {
        Debug.Log(gameObject.ToString() + ": Activated");
        return true;
    }

	// function that will be ran once the player exit the case
	public virtual void OnLeave(){
		return;
	}
}
