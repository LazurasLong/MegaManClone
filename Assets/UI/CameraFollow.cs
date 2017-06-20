using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

  public Transform target;
  public float distance = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    Debug.Log(target.position);
		transform.position = new Vector3(target.position.x,target.position.y,target.position.z - distance);
	}
}
