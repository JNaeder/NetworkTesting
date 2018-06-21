using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {



	public float speed;
	public float damage;

    ParticleSystem pS;
    SpriteRenderer sP;


	// Use this for initialization
	void Start () {

        pS = GetComponent<ParticleSystem>();
        sP = GetComponent<SpriteRenderer>();

        Color spriteColor = sP.color;
        pS.startColor = spriteColor;
	}
	
	// Update is called once per frame
	void Update () {
		//rb.AddForce(transform.right * force, ForceMode2D.Impulse);
		transform.position += transform.right * speed;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    
    {
		if(collision.gameObject.tag == "Player"){
			GuyController guy = collision.gameObject.GetComponent<GuyController>();
			guy.TakeDamage(damage);

		}

		Destroy(gameObject);
    }
}
