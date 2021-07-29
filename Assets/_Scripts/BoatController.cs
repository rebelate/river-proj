using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    public float speed;
    public Transform background;

    float direction;

    bool isMoving;

    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }

    void Movement(){
        if(!isMoving)
            return;

        Vector3 delta = Vector2.right * -direction * speed * Time.deltaTime;
        background.position += delta;
    }

    public void Move(InputAction.CallbackContext context){
        direction = context.ReadValue<float>();
        isMoving = direction != 0;
    }

}
