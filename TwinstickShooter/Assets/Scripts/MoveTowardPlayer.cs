using UnityEngine;
using System.Collections;

public class MoveTowardPlayer : MonoBehaviour
{
    Transform player;

    public float speed = 2.0f;

	// Use this for initialization
	void Start ()
	{
	    player = GameObject.Find("playerShip").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var delta = player.position - transform.position;
        delta.Normalize();

	    var moveSpeed = speed*Time.deltaTime;
	    transform.position = transform.position + (delta*moveSpeed);
	}
}
