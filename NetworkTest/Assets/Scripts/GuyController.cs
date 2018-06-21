using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GuyController : NetworkBehaviour {
	
	[SyncVar]
	public string playerName = "I'm Too Lazy To Change My Name";

	[SyncVar(hook = "UpdateHealth")]
	public float health = 5.0f;


	Vector2 direction;

    [SyncVar]
	public Color startColor = Color.cyan;

	Transform arm;
	public Transform muzzle;

	public float speed, jumpStrength, fallMult, upMult;
    public int maxJumpNum;


	public TextMesh nameDisplay, healthDisplay;
    public SpriteRenderer healthBarGreen;
	public GameObject fireBall;
	public PlayerObject playerObject;


    SpriteRenderer sP;
    Rigidbody2D rB;


    float healthBarStartSize, healthBarNewSize, startHealth;


    int jumpNum;
	// Use this for initialization
	void Start () {
		arm = GetComponentInChildren<Arm>().transform;
		sP = GetComponentInChildren<SpriteRenderer>();
        rB = GetComponent<Rigidbody2D>();

        startHealth = health;
        healthBarStartSize = healthBarGreen.transform.localScale.x;

		sP.color = startColor;
        if (hasAuthority)
		{
			
           // sP.color = startColor;
			CmdSetColor(startColor);
		}

		nameDisplay.text = playerName;
	}
	
	// Update is called once per frame
	void Update () {  

        //healthDisplay.text = health.ToString();
		nameDisplay.text = playerName;
        UpdateHealthBar();
        


		if(!hasAuthority){
			return;
		}

		Movement();
		ArmAiming();
		Shooting();
        
	}


	[Command]
	void CmdSetColor(Color newColor){
		sP.color = newColor;
		RpcSetColor(newColor);
	}

	[ClientRpc]
	void RpcSetColor(Color newColor){

		sP.color = newColor;
	}

	[Command]
	public void CmdFlipSpriteRenderX(bool isfacingLeft){
		sP.flipX = isfacingLeft;
		RpcFlipSpriteRenderX(isfacingLeft);
	}

	[ClientRpc]
	public void RpcFlipSpriteRenderX(bool isFacingLeft){
		sP.flipX = isFacingLeft;

	}

    
	void ArmAiming(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = new Vector2(mousePos.x - arm.position.x, mousePos.y - arm.position.y);
		arm.right = direction;
		      
	}

    void UpdateHealthBar() {
        float healthBarPerc = health / startHealth;
        Vector3 healthBarScale = healthBarGreen.transform.localScale;
        healthBarScale.x = healthBarStartSize * healthBarPerc;
        healthBarGreen.transform.localScale = healthBarScale;
    }
    


	void Movement(){
		float h = Input.GetAxis("Horizontal");


        transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);


        if (h < 0)
        {
            sP.flipX = true;
			CmdFlipSpriteRenderX(true);
        }
        else if (h > 0)
        {
            sP.flipX = false;
			CmdFlipSpriteRenderX(false);
        }

        if (rB.velocity.y < 0)
        {
            rB.velocity += Physics2D.gravity.y * Time.deltaTime * (fallMult - 1) * Vector2.up;
        }
        else if (rB.velocity.y > 0)
        {
            rB.velocity += Physics2D.gravity.y * Time.deltaTime * (upMult - 1) * Vector2.up;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpNum < maxJumpNum)
            {
                rB.AddForce(Vector2.up * jumpStrength);
                jumpNum++;
            }

        }

	}


    /// <summary>
    /// Shooting
    /// </summary>
	void Shooting(){

		if(Input.GetButtonDown("Fire1")){
			CmdFireShotOnServer();
		}

	}


	[Command]
	public void CmdFireShotOnServer(){
		GameObject fireB = Instantiate(fireBall, muzzle.position, muzzle.rotation);
		SpriteRenderer fireBSP = fireB.GetComponent<SpriteRenderer>();
		fireBSP.color = startColor;
        
        NetworkServer.Spawn(fireB);
        RpcSetShotColor(fireB, startColor);

    }

    [ClientRpc]
    void RpcSetShotColor(GameObject fireB, Color ballColor) {
        SpriteRenderer fireBSP = fireB.GetComponent<SpriteRenderer>();
        fireBSP.color = ballColor;

    }


    /// <summary>
    /// Damage and Health
    /// </summary>
    [Command]
    public void CmdTakeDamage(string name, float damage) {
        //Debug.Log(name + " Took " + damage + " damage");

		health -= damage;
		if(health <= 0){
			Debug.Log("PlayerDied");
			playerObject.PlayerGuyDeath();
		}
    }

	public void TakeDamage(float damage){;
		//health -= damage;
        CmdTakeDamage(gameObject.name, damage);
	}

    void UpdateHealth(float newHealth)
    {
        health = newHealth;
        healthDisplay.text = health.ToString();

        if (newHealth <= 0)
		{
            RpcPlayerDeath();
            Destroy(gameObject);
        }
    }
    
    [ClientRpc]
	void RpcPlayerDeath() {
        if (hasAuthority)
        {
            playerObject.PlayerGuyDeath();
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpNum = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
