using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{

    public Vector3 movingSpeed;
    public Vector3 secondObjectOffset;
    SpriteRenderer[] meshRenderers;

    void Start()
    {
        meshRenderers = new SpriteRenderer[2];
        meshRenderers[0] = this.GetComponentInChildren<SpriteRenderer>();
        meshRenderers[1] = Instantiate(meshRenderers[0], this.transform);
        meshRenderers[1].transform.position = transform.position + secondObjectOffset;
    }

    void LateUpdate()
    {
        meshRenderers[0].transform.position += movingSpeed;
        meshRenderers[1].transform.position += movingSpeed;

        if(meshRenderers[0].transform.position.y <= 0){
            meshRenderers[1].transform.position = meshRenderers[0].transform.position + secondObjectOffset;
        }

        if(meshRenderers[1].transform.position.y <= 0){
            meshRenderers[0].transform.position = meshRenderers[1].transform.position + secondObjectOffset;
        }
    }
}
