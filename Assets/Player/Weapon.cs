using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  public GameObject bullet;

  private PlayerMovement playerMovement;
  private bool hasShot = false;
  private float originOffset = 0.4f;
  
	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
    if(playerMovement.attacking > playerMovement.attackRate && !hasShot){
      DoAttack();
      hasShot = true;
    }else if (playerMovement.attacking < playerMovement.attackRate && hasShot)
      hasShot = false;
	}
  
  void DoAttack(){
    if (playerMovement.isOnGround)
      originOffset = 0.5f;
    else
      originOffset = 0.0f;
    Vector3 weaponPosition = new Vector3 ( gameObject.transform.position.x,  gameObject.transform.position.y - originOffset,  gameObject.transform.position.z);
    var tBullet = Instantiate(bullet, weaponPosition, bullet.transform.rotation) as GameObject;
    tBullet.GetComponent<Bullet>().bulletDirection = playerMovement.PlayerDirection;
  }
}
