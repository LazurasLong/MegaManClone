using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHandler : MonoBehaviour {
  public Sprite stand;
  public Sprite blink;
  public Sprite attack;
  public Sprite jump;
  public Sprite jumpAttack; 
  public Sprite run1;
  public Sprite run2;
  public Sprite run3;
  public Sprite run4;
  public Sprite runShoot1;
  public Sprite runShoot2;
  public Sprite runShoot3;
  public Sprite runShoot4;
  public Sprite damaged;
  
  private SpriteRenderer _spriteRenderer;  
  private PlayerMovement playerMovement;
  private PlayerHealthHandler playerHealthHandler;
  private float blinkCounter;
  private float blinkRate;
  private float blinkSpeed;
  private float animationCounter;
  private float animationRate;
  
  private Sprite currentStand;
  
	// Use this for initialization
	void Start () {
    playerMovement = GetComponent<PlayerMovement>();
    playerHealthHandler = GetComponent<PlayerHealthHandler>();
    _spriteRenderer = GetComponent(typeof (SpriteRenderer)) as SpriteRenderer;
    currentStand = stand;
    blinkCounter = 1.0f;
    blinkRate = 2.0f;
    blinkSpeed = 0.1f;
    animationRate = 0.6f;   
    animationCounter = animationRate;  
	}
	
	// Update is called once per frame
	void Update () {
    SetSprite();
	}
  
  void SetSprite(){
    // get animation factors
    bool isAttacking = playerMovement.attacking >= playerMovement.attackRate;
    bool isJumping = !playerMovement.isOnGround;
    bool isRunning = Math.Abs(Input.GetAxis("Horizontal")) > 0.2 && playerMovement.isOnGround;
    bool isFacingRight = playerMovement.PlayerDirection == Direction.RIGHT;
    bool isFacingLeft = playerMovement.PlayerDirection == Direction.LEFT;
    bool isDamaged = playerHealthHandler.cooldownTimer > 0.0f;
    
    //update animation counters
    if (blinkCounter > 0.0f)
      blinkCounter -= Time.deltaTime;
    else 
      blinkCounter = blinkRate;
    if (animationCounter > 0.0f)
      animationCounter -= Time.deltaTime;
    else
      animationCounter = animationRate;
    
    //set standing sprite, this enables blinking
    if(blinkCounter < blinkSpeed)
      currentStand = blink;
    else
      currentStand = stand;
    
    //is shooting, is jumping, is moving, is standing
    if(isAttacking){
      if(isJumping)
        _spriteRenderer.sprite = jumpAttack;
      else if(isRunning){
        if(animationCounter >= animationRate * 0.75f)
          _spriteRenderer.sprite = runShoot1;
        else if(animationCounter >= animationRate * 0.5f)
          _spriteRenderer.sprite = runShoot2;
        else if(animationCounter >= animationRate * 0.25f)
          _spriteRenderer.sprite = runShoot3;
        else
          _spriteRenderer.sprite = runShoot4;        
      }      
      else
        _spriteRenderer.sprite = attack; 
    //is not shooting
    }else{
      if(isJumping)
        _spriteRenderer.sprite = jump;
      else if (isRunning){
        if(animationCounter >= animationRate * 0.75f)
          _spriteRenderer.sprite = run1;
        else if(animationCounter >= animationRate * 0.5f)
          _spriteRenderer.sprite = run2;
        else if(animationCounter >= animationRate * 0.25f)
          _spriteRenderer.sprite = run3;
        else
          _spriteRenderer.sprite = run4;        
      }
      else
        _spriteRenderer.sprite = currentStand;
    }
      
    // is damaged
    if (isDamaged){
      _spriteRenderer.sprite = damaged;
    }
    
    //player direction
    if(isFacingRight){
      Vector3 theScale = transform.localScale;
      theScale.x = Math.Abs(theScale.x);
      transform.localScale = theScale;      
    }else if (isFacingLeft){
      Vector3 theScale = transform.localScale;
      theScale.x = -1 * Math.Abs(theScale.x);
      transform.localScale = theScale;   
    }
  }
}
