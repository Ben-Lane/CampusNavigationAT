using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement : MonoBehaviour
{

    public InputActionReference move_action;
    
    // Player Movement Variables
    private Vector2 movement_direction;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement_direction = move_action.action.ReadValue<Vector2>();
        print(movement_direction);
    }

    private void FixedUpdate()
    {
        //Update Position
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x + (movement_direction.x * speed * Time.deltaTime), 
            GetComponent<Transform>().position.y, 
            GetComponent<Transform>().position.z + (movement_direction.y * speed * Time.deltaTime));
    }
}
