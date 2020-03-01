using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : NetworkBehaviour
{
    GameObject target;

    Transform leftBoundary;
    Transform rightBoundary;

    public GameObject projectile;

    public GameObject spritePivot;
    public GameObject cannonTip;

    public float startVelocity;
    public float shotSpeed;

    float velocity;

    private void Start()
    {
        leftBoundary = GameObject.Find("Left Boundary").transform;
        rightBoundary = GameObject.Find("Right Boundary").transform;

        InvokeRepeating("FindTarget", 0.0f, 3.0f);
        velocity = startVelocity;
    }

    void FindTarget()
    {
        var playerList = GameObject.FindGameObjectsWithTag("Player");
        if(playerList.Length > 0)
        {
            target = playerList[Random.Range(0, playerList.Length - 1)];
        }
        else
        {
            target = null;
        }
    }

    public void Shoot()
    {
        if (target == null)
            return;
        GameObject shot = Instantiate(projectile, cannonTip.transform.position, Quaternion.identity);

        NetworkServer.Spawn(shot);

        shot.GetComponent<Rigidbody2D>().velocity = (target.transform.position - cannonTip.transform.position).normalized * shotSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(velocity * Time.fixedDeltaTime, 0, 0));

        if(transform.position.x > rightBoundary.position.x || transform.position.x < leftBoundary.position.x)
        {
            velocity *= -1;
        }

        if (target == null)
            return;
        spritePivot.transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - target.transform.position);
    }
}
