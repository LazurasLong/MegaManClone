using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blader : MonoBehaviour {

  public int health = 6;
  public float speed = 12.0f;
  public int damage = 4;
  public Sprite frame1;
  public Sprite frame2;  
 
  private Transform _transform;
  private Rigidbody2D _rigidbody; 
  private SpriteRenderer _spriteRenderer;   
  private float animationCounter;
  private float animationRate;
  private float movementCounter;
  private float movementModifierY;
  private float movementCounter2;
  private float movementModifierX;  
  
	// Use this for initialization
	void Start () {
    _transform = GetComponent(typeof (Transform)) as Transform;
    //_rigidbody = GetComponent(typeof (Rigidbody2D)) as Rigidbody2D;  
    _spriteRenderer = GetComponent(typeof (SpriteRenderer)) as SpriteRenderer;
    animationRate = 0.2f;  
    animationCounter = animationRate;
    movementCounter = 1.0f;
    movementModifierY = 1.0f;
    movementCounter2 = 4.0f;
    movementModifierX = -1.0f;    
	}
	
	// Update is called once per frame
	void Update () {
		Move();
    Animate();
	}
  
  void Move(){
    float translateY = movementModifierY * speed * (movementCounter - 0.5f) * Time.deltaTime;
    float translateX = movementModifierX * speed * Time.deltaTime;
    
    if (movementCounter > 0.0f)
      movementCounter -= Time.deltaTime;
    else {
      movementCounter = 1.0f;
      movementModifierY = -movementModifierY;
    }
      
    if(movementCounter2 > 0.0f) {
      movementCounter2 -= Time.deltaTime;
    }else{
      movementCounter2 = 4.0f;
      movementModifierX = -movementModifierX;
    }
    
    _transform.Translate(translateX,translateY,0);
  }
  
  void Animate(){
    // handle animation timer
    if (animationCounter > 0.0f)
      animationCounter -= Time.deltaTime;
    else
      animationCounter = animationRate;
    
    //set frames
    if(animationCounter >= animationRate * 0.5f)
      _spriteRenderer.sprite = frame1;
    else
      _spriteRenderer.sprite = frame2; 
    
    if(movementModifierX < 0.0f){
      Vector3 theScale = transform.localScale;
      theScale.x = Math.Abs(theScale.x);
      transform.localScale = theScale;      
    }else{
      Vector3 theScale = transform.localScale;
      theScale.x = -1 * Math.Abs(theScale.x);
      transform.localScale = theScale;   
    }    
  }
  
  public void Damage(int dmg){
    health -= dmg;
    if(health <= 0)
      Destroy(gameObject);
  }
  
  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
      collision.collider.gameObject.GetComponent<PlayerHealthHandler>().Damage(damage);
  }
}
