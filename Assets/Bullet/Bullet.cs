using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public Direction bulletDirection = Direction.RIGHT;
  public float speed = 16.0f;
  public int damage = 2;
  public Weapon attacker;
  
  private Transform _transform;
  
	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
    Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
    if (
      (screenPosition.y > Screen.height || screenPosition.y < 0) || 
      (screenPosition.x > Screen.width || screenPosition.x < 0)
      ){
      Destroy(this.gameObject);
      if (attacker != null)
        attacker.BulletGone();    
    } 
		MoveBullet();
    
	}
  
  void MoveBullet () {
    int moveDirection = bulletDirection == Direction.LEFT ? -1 : 1;
    Vector3 theScale = transform.localScale;
    theScale.x = moveDirection * Math.Abs(theScale.x);
    float translate = moveDirection * speed * Time.deltaTime;
    _transform.localScale = theScale;
    _transform.Translate(translate, 0 ,0);
  }
  
  void OnCollisionEnter2D(Collision2D collision){
    if(collision.collider.tag == "Enemy") {
      collision.collider.gameObject.GetComponent<Blader>().Damage(damage);
    }
    //_transform.Translate(0, -1 ,0);
    Destroy(gameObject);
    if (attacker != null)
      attacker.BulletGone();
  }
}
