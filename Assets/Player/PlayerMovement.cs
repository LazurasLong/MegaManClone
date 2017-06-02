using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {LEFT, RIGHT};
public class PlayerMovement : MonoBehaviour {

  public float speed = 4.0f;
  public float jumpPower = 6.0f;
  public float attacking = 0.0f;
  public float attackRate = 0.6f;
  
  public bool isOnGround = false;
  public bool isOnLeftContact = false;
  public bool isOnRightContact = false;
  public bool isOnUpContact = false;  
  
  private Transform _transform;
  private Rigidbody2D _rigidbody;
  private PlayerHealthHandler playerHealthHandler;
  private Weapon playerWeapon;
  private Direction playerDirection = Direction.RIGHT;

  
  public Direction PlayerDirection {
    get {
      return playerDirection;
    }
  }
  
	// Use this for initialization
	void Start () {
		_transform = GetComponent(typeof (Transform)) as Transform;
    _rigidbody = GetComponent(typeof (Rigidbody2D)) as Rigidbody2D; 
    playerHealthHandler = GetComponent<PlayerHealthHandler>();  
    playerWeapon = GetComponent<Weapon>();    
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer();
    Jump();
    Attack();
	}
  
  void MovePlayer(){
    bool isDamaged = playerHealthHandler.cooldownTimer > 0.0f;
    float translate;
    
    //only move if not in damage cooldown
    if (!isDamaged){
      translate = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
      if(translate > 0){
        playerDirection = Direction.RIGHT;
        isOnLeftContact = false;
        //don't try to move right if there is a contact point
        if(isOnRightContact)
          translate = 0;
      }else if(translate < 0){
        playerDirection = Direction.LEFT;
        isOnRightContact = false;
        //don't try to move left if there is a contact point
        if(isOnLeftContact)
          translate = 0;
      }

      _transform.Translate(translate,0,0);
      
    }else{
      translate = playerHealthHandler.cooldownTimer * 2 * Time.deltaTime;
      if(playerDirection == Direction.LEFT)
        _transform.Translate(translate,0,0);
      else
        _transform.Translate(-translate,0,0);
    }

  }
  
  void Jump() {
    bool isDamaged = playerHealthHandler.cooldownTimer > 0.0f;
    
    if(Input.GetButtonDown("Fire2") && (isOnGround && !isDamaged)) {
      _rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
      isOnGround = false;
    }
  }
  
  void Attack() {
    bool isDamaged = playerHealthHandler.cooldownTimer > 0.0f;
    
    if(Input.GetButtonDown("Fire1") && !isDamaged){
      if (playerWeapon.CanAttack()){
        attacking = 1.0f;
      }
    }
    if (attacking > 0.0f){
      attacking -= Time.deltaTime;
    }else
      attacking = 0.0f;
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Solid Tiles")){
      foreach (ContactPoint2D coll in collision.contacts){
        // check for ground collision
        if ((coll.normal.y == 1.0f) && (coll.normal.x == 0.0f))
          isOnGround = true;
        if ((coll.normal.y == 0.0f) && (coll.normal.x == 1.0f))
          isOnLeftContact = true;
        if ((coll.normal.y == 0.0f) && (coll.normal.x == -1.0f))
          isOnRightContact = true;
      }
    }
  }
  
  void OnCollisionStay2D(Collision2D collision) {
    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Solid Tiles")){
      foreach (ContactPoint2D coll in collision.contacts){
        // check for ground collision
        if ((coll.normal.y == 1.0f) && (coll.normal.x == 0.0f))
          isOnGround = true;
        if ((coll.normal.y == 0.0f) && (coll.normal.x == 1.0f))
          isOnLeftContact = true;
        if ((coll.normal.y == 0.0f) && (coll.normal.x == -1.0f))
          isOnRightContact = true;
      }
    }
  } 
  
  void OnCollisionExit2D(Collision2D collision) {
    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Solid Tiles")){
      isOnGround = false;
      isOnLeftContact = false;
      isOnRightContact = false;
    }
  }
  
}
