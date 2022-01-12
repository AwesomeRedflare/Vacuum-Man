using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    public Transform pivot;
    public float maxDistance;

    public LayerMask ground;

    public GameObject player;
    public float force;
    private float power;

    public GameObject vacuumEffect;
    public GameObject suckEffect;

    public ParticleSystem vacuumPartciles;
    public ParticleSystem suckPartciles;
    private ParticleSystem.EmissionModule vacuumEmission;
    private ParticleSystem.EmissionModule suckEmission;
    public float emissionRate;


    public float divider;

    private void Start()
    {
        vacuumEmission = vacuumPartciles.emission;
        suckEmission = suckPartciles.emission;

        transform.position = new Vector2(transform.position.x, -maxDistance);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(pivot.position, -pivot.up, maxDistance, ground);

        if (Input.GetMouseButton(0))
        {
            vacuumEmission.rateOverTime = emissionRate;

            Vector2 direction = player.transform.position - transform.position;

            if(hit.distance != 0)
            {
                power = -hit.distance + maxDistance;
            }
            else
            {
                power = 0;
            }

            power = power / maxDistance;

            //Debug.Log(power);

            if (hit.distance != 0 && hit.collider.GetComponent<Rigidbody2D>() != null)
            {
                hit.transform.GetComponent<Rigidbody2D>().AddForce(-direction * (power * force));
            }

            player.GetComponent<Rigidbody2D>().AddForce(direction * (power * force));
        }
        else
        {
            vacuumEmission.rateOverTime = 0f;
        }

        if (Input.GetMouseButton(1))
        {
            suckEmission.rateOverTime = emissionRate;

            Vector2 direction = player.transform.position - transform.position;

            if (hit.distance != 0)
            {
                power = -hit.distance + maxDistance;
            }
            else
            {
                power = 0;
            }

            power = power / maxDistance;

            //Debug.Log(power);

            if (hit.distance != 0 && hit.collider.GetComponent<Rigidbody2D>() != null)
            {
                hit.transform.GetComponent<Rigidbody2D>().AddForce(direction * (power * force));
            }

            player.GetComponent<Rigidbody2D>().AddForce(-direction * (power * force));
        }
        else
        {
            suckEmission.rateOverTime = 0f;
        }

        if (hit.distance != 0)
        {
            transform.localPosition = new Vector2(0, -hit.distance);
            vacuumEffect.transform.localScale = new Vector3(1f, 1f, (hit.distance / divider) -.05f);
            suckEffect.transform.localScale = new Vector3(1f, 1f, (hit.distance / divider) - .05f);
        }
        else
        {
            transform.localPosition = new Vector2(0, -maxDistance);
            vacuumEffect.transform.localScale = new Vector3(1f, 1f, (maxDistance/ divider) - .05f);
            suckEffect.transform.localScale = new Vector3(1f, 1f, (maxDistance / divider) - .05f);
        }

        //Debug.Log(hit.distance);

        Debug.DrawRay(pivot.position, -pivot.up, Color.red);
    }
}
