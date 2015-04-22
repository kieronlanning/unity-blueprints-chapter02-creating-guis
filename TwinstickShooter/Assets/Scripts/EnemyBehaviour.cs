using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    // How many times should I be hit before I die?
    public int health = 2;

    // When the enemy dies, we play an explosion.
    public Transform explosion;

    // What sound to play when we're hit.
    public AudioClip hitSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Uncomment this line to check for collision.
        //Debug.Log("Hit: " + collision.gameObject.name);

        // This line looks for "laser" in the names of
        // anything collided.
        if (collision.gameObject.name.Contains("laser"))
        {
            var laser = (LaserBehaviour) collision.gameObject.GetComponent("LaserBehaviour");
            health -= laser.damage;

            // Destroy the laser.
            Destroy(collision.gameObject);

            // Plays a sound from this object's AudioSource.
            var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(hitSound);

            var controller = (GameController)GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent("GameController");

            controller.KilledEnemy();
            controller.IncreaseScore(10);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);

            // Check to see if the explosion was set.
            if (explosion)
            {
                var exploder = ((Transform) Instantiate(explosion, transform.position, transform.rotation)).gameObject;

                var audioSource = explosion.GetComponent<AudioSource>();
                audioSource.Play();

                Destroy(exploder, 2.0f);
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
