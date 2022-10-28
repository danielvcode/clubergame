using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class hazard : MonoBehaviour
{
     Vector3 rotation;
    public  ParticleSystem breakingEffect;
    private CinemachineImpulseSource cinemachineImpulseSource;
    private Player player;

    private void Start() {

        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindObjectOfType<Player>();

        var xRotation = Random.Range(90f, 180f);
       rotation = new Vector3(-xRotation,0);
 
    }

    private void Update(){
        transform.Rotate(rotation * Time.deltaTime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hazard"))
        {
        if (!collision.gameObject.CompareTag("escada"))
        {
                Destroy(gameObject);
                Instantiate(breakingEffect, transform.position,Quaternion.identity);

                if(player != null)
                {
                    var distance = Vector3.Distance(transform.position, player.transform.position);
                    var force = 1.2f / distance;

                    cinemachineImpulseSource.GenerateImpulse(force);
                }


            }
        }
    }


}
