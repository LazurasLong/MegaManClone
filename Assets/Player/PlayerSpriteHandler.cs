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
  
  private SpriteRenderer _spriteRenderer;  
  private PlayerMovement playerMovement;
  private float blinkCounter = 1.0f;
  private float blinkRate = 2.0f;
  private float blinkSpeed = 0.1f;
  
  private Sprite currentStand;
  
	// Use this for initialization
	void Start () {
    playerMovement = GetComponent<PlayerMovement>();
    _spriteRenderer = GetComponent(typeof (SpriteRenderer)) as SpriteRenderer;
    currentStand = stand;
	}
	
	// Update is called once per frame
	void Update () {
		SetSprite();
    if (blinkCounter > 0.0f)
      blinkCounter -= Time.deltaTime;
    else 
      blinkCounter = blinkRate;
	}
  
  void SetSprite(){
    //is standing
    if(blinkCounter < blinkSpeed)
      currentStand = blink;
    else
      currentStand = stand;
    
    //is shooting
    if(playerMovement.attacking >= playerMovement.attackRate){
      if(playerMovement.isOnGround)
        _spriteRenderer.sprite = attack;
      else
        _spriteRenderer.sprite = jumpAttack;
      
    //is not shooting
    }else{
      if(playerMovement.isOnGround)
        _spriteRenderer.sprite = currentStand;
      else
        _spriteRenderer.sprite = jump;
    }
    
    //is moving
    if(Math.Abs(Input.GetAxis("Horizontal")) > 0.2)
      //is running
    //else if()
      //is barely moving
    //else
      //is not moving
    
    //player direction
    if(playerMovement.PlayerDirection == Direction.RIGHT){
      Vector3 theScale = transform.localScale;
      theScale.x = Math.Abs(theScale.x);
      transform.localScale = theScale;      
    }else if (playerMovement.PlayerDirection == Direction.LEFT){
      Vector3 theScale = transform.localScale;
      theScale.x = -1 * Math.Abs(theScale.x);
      transform.localScale = theScale;   
    }
  }
}
