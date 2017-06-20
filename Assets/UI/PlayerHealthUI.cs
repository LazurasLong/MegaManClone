using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {

  public GameObject player;
 
  private PlayerHealthHandler playerHealthHandler;
  private Text text;
  
	// Use this for initialization
	void Start () {
		playerHealthHandler = player.GetComponent<PlayerHealthHandler>();
    text = GetComponent<Text>();
    
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "HP:"+playerHealthHandler.health.ToString();
	}
}
