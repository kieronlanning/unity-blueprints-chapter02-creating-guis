using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Transform enemy;

    // We want to delay our code at certain times.
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;

    int currentNumberOfEnemies = 0;

    // The values we'll be printing.
    int score = 0;
    int waveNumber = 0;

    // The GUI Text game objects.
    public GUIText scoreText;
    public GUIText waveText;

    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    // Allows classes outside of GameController to say when we kill an enemy.
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(SpawnEnemies());
	}

    // Coroutine to span enemies.
    IEnumerator SpawnEnemies()
    {
        // Give the player time before we start the game.
        yield return new WaitForSeconds(timeBeforeSpawning);

        // After timeBeforeSpawning has elapsed, we will enter this loop.
        while (true)
        {
            // Don't spawn anything new until all the previous wave's enemies are dead.
            if (currentNumberOfEnemies <= 0)
            {
                waveText.text = "Wave: " + ++waveNumber;

                // Spawn 10 enemies in a random position.
                for (var i = 0; i < enemiesPerWave; i++)
                {
                    // We want the enemies to be off screen
                    // (Random.Range gives us a number between
                    // the first and second parameters)
                    var randDistance = Random.Range(10, 25);

                    // Enemies can come from any direction.
                    var randDirection = Random.Range(0, 360);

                    // Using the distance and direction we set the position.
                    var posX = transform.position.x
                               + (Mathf.Cos(randDirection*Mathf.Deg2Rad)*randDistance);
                    var posY = transform.position.y
                               + (Mathf.Sin(randDirection*Mathf.Deg2Rad)*randDistance);

                    // Spawn the enemy and increment the number of enemies spawned.

                    // Spawn the enemy and increment the number of enemies spawned.
                    // Instansiate makes a clone of the first parameter and
                    // places it at the second with a rotation of the third.
                    Instantiate(enemy, new Vector3(posX, posY, 0), transform.rotation);

                    currentNumberOfEnemies++;

                    yield return  new WaitForSeconds(timeBetweenEnemies);
                }
            }

            // How much time to wait before checking if we need to spanwn another way.
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    // Update is called once per frame
	void Update () {
	
	}
}
