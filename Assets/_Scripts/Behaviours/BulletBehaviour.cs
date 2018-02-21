using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public GameObject bulletHole;


    private Rigidbody rigidbody;
    
    public Rigidbody Rigidbody
    {
        get
        {
            rigidbody = GetComponent<Rigidbody>();
            return rigidbody;
        }

        set
        {
            rigidbody = value;
        }
    }

    private Vector3 acceleration;

    private float mass;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        mass = rigidbody.mass;
    }

    void FixedUpdate()
    {
        ApplyForce(GameEnvironment.GRAVITY*0.02f);
        ApplyForce(GameEnvironment.windZone/mass);

        rigidbody.velocity += acceleration;
        acceleration = Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        rigidbody.velocity += force;
    }

    public void ResetBullet()
    {
        rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameObject.activeSelf&&transform.position.y<-2000)
        {
            ResetBullet();
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        Quaternion rot = Quaternion.Euler(other.contacts[0].normal);
        Instantiate(bulletHole, other.contacts[0].point, rot);
        ResetBullet();
    }
}
