using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {

        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("so rb=" + rb);
    }
    public void EnableMoving(bool b)
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
        if (b)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {

            rb.bodyType = RigidbodyType2D.Static;
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision=" + col.gameObject.name);
        Game.Repaint(col.gameObject);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            AddForceToBall(Input.mousePosition);
    }

    private void AddForceToBall(Vector3 mousePosition)
    {
        Vector3 ballScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 force = mousePosition - ballScreenPos;
        rb.AddForce(force);

    }
/*
    void OnBecameInvisible()
    {
        Game.StopAndMenu();
    }
    */
}