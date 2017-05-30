using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {LEFT, RIGHT};
public class PlayerMovement : MonoBehaviour {

  public float speed = 4.0f;
  public bool isOnGround = false;
  public float jumpPower = 6.0f;
  public float attacking = 0.0f;
  public float attackRate = 0.6f;
  
  private Transform _transform;
  private Rigidbody2D _rigidbody;
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
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer();
    Jump();
    Attack();
	}
  
  void MovePlayer(){
    float translate = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    _transform.Translate(translate,0,0);
    
    if(translate > 0){
      playerDirection = Direction.RIGHT;
    }else if(translate < 0){
      playerDirection = Direction.LEFT;
    }
  }
  
  void Jump() {
    if(Input.GetButtonDown("Fire2") && isOnGround) {
      _rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
      isOnGround = false;
    }
  }
  
  void Attack() {
    if(Input.GetButtonDown("Fire1")){
      if (attacking <= attackRate){
        attacking = 1.0f;
      }
    }
    if (attacking > 0.0f){
      attacking -= Time.deltaTime;
    }else
      attacking = 0.0f;
  }
  
  void OnCollisionEnter2D(Collision2D collision) {
    //if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Solid Tiles"))
      isOnGround = true;
  }
  
  void OnCollisionExit2D(Collision2D collision) {
    //isOnGround = false;
  }
  
}
