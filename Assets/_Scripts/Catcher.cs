using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public float hookTravelDuration;
    public float returnHookDuration;

    public Action HookReturned;
    public Action HookTouchedGarbage;

    Collider2D catcherCollider;

    void Start()
    {
        catcherCollider = GetComponent<Collider2D>();
        transform.position = Vector3.zero;
    }

    void Update()
    {
        
    }

    public void StartThrow(Vector2 direction, float throwPower){
        StopAllCoroutines();
        StartCoroutine(MoveTheCatcher(direction * throwPower));
    }

    IEnumerator MoveTheCatcher(Vector2 destination){
        float elapsedTime = 0;
        Vector2 currentPosition = transform.position;
        while(elapsedTime < hookTravelDuration){
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.Lerp(currentPosition, destination, elapsedTime/hookTravelDuration);
            yield return null;
        }
        HookTouchedGarbage?.Invoke();
        StartCoroutine(BringBackTheCatcher(currentPosition));
    }

    IEnumerator BringBackTheCatcher(Vector2 destination){
        catcherCollider.enabled = true;

        float elapsedTime = 0;
        Vector2 currentPosition = transform.position;
        while(elapsedTime < returnHookDuration){
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.Lerp(currentPosition, destination, elapsedTime/returnHookDuration);
            yield return null;
        }
        catcherCollider.enabled = false;
        HookReturned?.Invoke();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.enabled = false;
        Destroy(collision.collider.GetComponent<ConstantForce2D>());
        collision.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        StartCoroutine(GrabGarbage(collision.transform));
    }

    IEnumerator GrabGarbage(Transform garbage){
        float time = 3f;
        Vector3 lastPosition = transform.position;

        //temporary rule
        while(time > 0){
            time -= Time.deltaTime;
            Vector3 deltaPosition = transform.position - lastPosition;
            garbage.position += deltaPosition;

            lastPosition = transform.position;
            yield return null;
        }
    }
}
