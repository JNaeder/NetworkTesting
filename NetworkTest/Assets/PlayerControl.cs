using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {


	float h,v;

	public float speed;
    [SyncVar]
    public string playerName = "blank";
    [SyncVar(hook ="UpdateHealth")]
    public int health = 5;

    public TextMesh titleName, healthNumber;
    [SyncVar]
    public Color startColor;

	Rigidbody2D rB;
    SpriteRenderer sP;

	// Use this for initialization
	void Start () {
        //rB.GetComponent<Rigidbody2D>();
        sP = GetComponent<SpriteRenderer>();
        sP.color = startColor;
        titleName.text = playerName;
        print(playerName);
	}
	
	// Update is called once per frame
	void Update () {
		if (hasAuthority)
		{
			Movement();

		}
        healthNumber.text = health.ToString();

    }


    void UpdateHealth(int newHealth) {
        health = newHealth;
    }


	void Movement(){
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        transform.position += new Vector3(h * speed * Time.deltaTime, v * Time.deltaTime * speed, 0);
        

	}

    [Command]
    void CmdTakeDamage() {
        health--;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasAuthority)
        {
            CmdTakeDamage();
        }
    }
}
