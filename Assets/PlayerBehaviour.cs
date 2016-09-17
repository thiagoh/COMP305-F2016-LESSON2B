using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    // movement modifier applied to directional movement
    public float playerSpeed = 4.0f;

    // what the current speed of our player is
    private float currentSpeed = 0.0f;

    // the last movement that we've made
    private Vector3 lastMovement = new Vector3();

    // Use this for initialization
    void Start() {
        Debug.Log("started");

        this.currentSpeed = 0.0f;
        this.lastMovement = new Vector3();
    }

    // Update is called once per frame
    void Update() {

        Rotation();
        Movement();
    }

    void Rotation() {

        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;

        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        // add ship's rotation
        this.transform.rotation = rotation;
    }

    void Movement() {

        Vector3 movement = new Vector3();
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        movement.Normalize();

        if (movement.magnitude > 0) {

            currentSpeed = playerSpeed;
            transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;

        } else {

            transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
            currentSpeed *= 0.9f;
        }
        
    }
}
