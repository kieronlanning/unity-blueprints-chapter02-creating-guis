using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
    // Movement modifier applied to directional movement.
    public float playerSpeed = 2.0f;

    // The laser we will be shooting.
    public Transform laser;

    // How far from the center of the ship should the laser be.
    public float laserDistance = .2f;

    // How much time in seconds we should wait before we can fire again.
    public float timeBetweenFires = .3f;

    // If value is less than or equal to 0, we can fire.
    float timeTillNextFire = 0.0f;

    public List<KeyCode> shootButton;

    // What the current speed of our player is.
    float currentSpeed = 0.0f;

    /*
        Allows us to have multiple inputs and supports
        keyboard, joystick, etc.
    */
    public List<KeyCode> upButton;
    public List<KeyCode> downButton;
    public List<KeyCode> leftButton;
    public List<KeyCode> rightButton;

    // What sound to play when we're shooting.
    public AudioClip shootSound;

    // The last movement that we've made.
    Vector3 lastMovement = new Vector3();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            // If false becomes true and vice/versa.
            PauseMenu.isPaused = !PauseMenu.isPaused;
        }

        if (PauseMenu.isPaused)
            return;

        // Roate player to face the mouse.
        Rotation();

        // Move the player's body.
        Movement();

        foreach (KeyCode keyCode in shootButton)
        {
            if (Input.GetKey(keyCode) && timeTillNextFire < 0.0f)
            {
                timeTillNextFire = timeBetweenFires;
                ShootLaser();
                break;
            }
        }

        timeTillNextFire -= Time.deltaTime;
    }

    // Creates a laser and gives it an inital position in front of the ship.
    void ShootLaser()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound);

        // Calculate the position right in front of the ship's
        // position - laserDistance away from the ship.
        float posX = this.transform.position.x +
                     (Mathf.Cos((transform.localEulerAngles.z - 90)
                                *Mathf.Deg2Rad)*-laserDistance);
        float posY = this.transform.position.y +
                     (Mathf.Sin((transform.localEulerAngles.z - 90)
                                *Mathf.Deg2Rad)*-laserDistance);

        Instantiate(laser, new Vector3(posX, posY, 0), this.transform.rotation);
    }

    // Will move the player based off of the keys pressed.
    void Movement()
    {
        // The movement that needs to occur this frame.
        Vector3 movement = new Vector3();

        // Check for input.
        movement += MoveIfPressed(upButton, Vector3.up);
        movement += MoveIfPressed(downButton, Vector3.down);
        movement += MoveIfPressed(leftButton, Vector3.left);
        movement += MoveIfPressed(rightButton, Vector3.right);

        /*
            If we pressed multiple buttons, make sure we're only moving the
            same length.
        */
        movement.Normalize();

        // Check to see if anything was pressed.
        if (movement.magnitude > 0)
        {
            // If we did, move in that direction.
            currentSpeed = playerSpeed;
            this.transform.Translate(lastMovement*Time.deltaTime*playerSpeed, Space.World);

            lastMovement = movement;
        }
        else
        {
            // Otherwise, move in the direction we were going.
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);

            // Slow down over time.
            currentSpeed *= 0.9f;
        }
    }

    /*
        Will return the movememnt if any of the keys are pressed,
        otherwise it will return (0, 0, 0).
    */
    Vector3 MoveIfPressed(List<KeyCode> keys, Vector3 movememnt)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKey(key))
            {
                /*
                    It was pressed, so we leave the function with the movement applied.
                */
                return movememnt;
            }
        }

        // None of the keys were pressed.
        return Vector3.zero;
    }

    // Will rotate the ship to face the mouse.
    void Rotation()
    {
        // We need to tell where the mouse is relative to the player.
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        // Get the differences from each axis (stands for deltaX and deltaY).
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;

        // Get the angle between the two objects.
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        /*
            The transform's rotation property users a Quaternion
            so we need to convert the angle in a Vector
            (The Z axis is for rotation for 2D).
        */

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        // Assign the ships rotation.
        this.transform.rotation = rot;
    }
}
