using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour {

  public int health = 28;  
  public float cooldownTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    
		if (cooldownTimer > 0.0f)
      cooldownTimer -= Time.deltaTime;
    else
      cooldownTimer = 0.0f;
	}
  
  public void Damage(int dmg){
    if (cooldownTimer == 0.0f){
      health -= dmg;
      cooldownTimer = dmg/4 * 1.0f;
      if(health <= 0)
        Destroy(gameObject);
    }
  }
}
