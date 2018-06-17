using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GuyController : NetworkBehaviour {
	
	[SyncVar]
	public string playerName = "Blank";

	public float speed, jumpStrength, fallMult, upMult;
    public int maxJumpNum;

	public TextMesh nameDisplay;
	public Transform arm;

    SpriteRenderer sP;
    Rigidbody2D rB;

    int jumpNum;


	Vector3 newDashPos;

	// Use this for initialization
	void Start () {
		sP = GetComponentInChildren<SpriteRenderer>();
        rB = GetComponent<Rigidbody2D>();

		nameDisplay.text = playerName;
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasAuthority){
			return;
		}

		Movement();
		ArmAiming();
        
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
		Vector2 direction = new Vector2(mousePos.x - arm.position.x, mousePos.y - arm.position.y);
		CmdArmRotation(direction);
		arm.right = direction;

	}

	[Command]
	public void CmdArmRotation(Vector2 rot){
		
		RpcArmRoation(rot);
		if(hasAuthority){
			arm.right = rot;
		}
	}

	[ClientRpc]
	public void RpcArmRoation(Vector2 rot){
		if (hasAuthority)
		{
			arm.right = rot;
		}

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



    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpNum = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
