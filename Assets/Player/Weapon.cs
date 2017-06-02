using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  public GameObject bullet;
  public int shotsAllowedOnScreen = 3;
  private float shotRate = 0.9f;
  
  private int shotsOnScreen;
  private PlayerMovement playerMovement;
  private bool hasShot = false;
  private float originOffset;
  
  
	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovement>();
    shotsOnScreen = 0;
	}
	
	// Update is called once per frame
	void Update () {
    if(playerMovement.attacking > shotRate && !hasShot){
      DoAttack();
      hasShot = true;
    }else if (playerMovement.attacking < shotRate && hasShot)
      hasShot = false;
	}
  
  void DoAttack(){
    if (playerMovement.isOnGround)
      originOffset = 0.5f;
    else
      originOffset = 0.0f;
    
    if(CanAttack()){
      Vector3 weaponPosition = new Vector3 ( gameObject.transform.position.x,  gameObject.transform.position.y - originOffset,  gameObject.transform.position.z);
      var tBullet = Instantiate(bullet, weaponPosition, bullet.transform.rotation) as GameObject;
      tBullet.GetComponent<Bullet>().bulletDirection = playerMovement.PlayerDirection;
      tBullet.GetComponent<Bullet>().attacker = this;
      shotsOnScreen += 1;
    }
  }
  
  public bool CanAttack(){
    return shotsOnScreen < shotsAllowedOnScreen;
  }
  
  public void BulletGone(){
    shotsOnScreen -= 1;
  }
  
}
