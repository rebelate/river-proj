using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour
{

    public Transform catcher;
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Start(){
        line.SetPosition(1, catcher.localPosition);
    }

    void Update()
    {
        line.SetPosition(1, catcher.localPosition);
    }

}
