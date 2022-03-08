using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    [Range(0, 40000)]
    public float force = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Rigidbody2D>().angularVelocity -= force;
        Debug.Log("yes");
    }
}
