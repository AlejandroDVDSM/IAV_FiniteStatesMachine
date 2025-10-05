using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private int damage = 25;
	
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Robot"))
			col.gameObject.GetComponent<RobotAI>().ReceiveDamage(damage);
		
    	Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
