﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float dist = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + dist,
            cam.transform.position.y, transform.position.z);
        float temp = cam.transform.position.x * (1 - parallaxEffect);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
