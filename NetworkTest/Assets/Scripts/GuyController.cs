using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GuyController : NetworkBehaviour {
	
	[SyncVar]
	public string playerName = "Blank";

	[SyncVar(hook = "UpdateHealth")]
	public float health = 5.0f;


	Vector2 direction;


	Transform arm;
	public Transform muzzle;

	public float speed, jumpStrength, fallMult, upMult;
    public int maxJumpNum;


	public TextMesh nameDisplay, healthDisplay;
	public GameObject fireBall;
	public PlayerObject playerObject;


    SpriteRenderer sP;
    Rigidbody2D rB;

    int jumpNum;

	// Use this for initialization
	void Start () {
		arm = GetComponentInChildren<Arm>().transform;
		sP = GetComponentInChildren<SpriteRenderer>();
        rB = GetComponent<Rigidbody2D>();

		nameDisplay.text = playerName;
	}
	
	// Update is called once per frame
	void Update () {  

        healthDisplay.text = health.ToString();
		nameDisplay.text = playerName;
		if(!hasAuthority){
			return;
		}

		Movement();
		ArmAiming();
		Shooting();
        
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
        NetworkServer.Spawn(fireB);

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
			//Debug.Log("Death 1 from " + playerName + "'s Guy");
            //playerObject.PlayerGuyDeath();
            //Destroy(gameObject);
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
