using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float ForceMultiplier = 3f;
    public float maxVelocity = 3f;
    public ParticleSystem deathParticles;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

    // Start is called before the first frame update


     void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 0;
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }


    // Update is called once per frame
    void Update()
    {

        if(GameManager.Instance == null)
        {
            return;
        }

        var horizontal = Input.GetAxis("Horizontal");
        if(rb.velocity.magnitude <= maxVelocity){
            rb.AddForce(new Vector3(horizontal * ForceMultiplier * Time.deltaTime,0,0));
        }
        GameObject obj;
        obj = GameObject.Find("Player");//pega o nome do obj
        if(obj.transform.position.x >= 9)
        {
            Destroy(gameObject);
        }
        else if(obj.transform.position.x <= -8)
        {
            Destroy(gameObject);
        }

        Debug.Log(obj.transform.position);
    }

    private void OnEnable()
    {
        transform.transform.position = new Vector3(0.01f, 2.06f, 0);
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Hazard")){
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();


        }
    }
}
