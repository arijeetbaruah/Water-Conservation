using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceExplorationPlayerController : MonoBehaviour
{
    public Transform rocketPack;
    public float forceMagnitude = 30;
    public float horizontalForceMagnitude = 5;
    public GameObject explosion;
    
    private Camera camera;
    private ParticleSystem particleSystem;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") != 0)
        {
            rb.AddExplosionForce(forceMagnitude, rocketPack.position, 30);
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.AddForce(- transform.right * horizontalForceMagnitude * Input.GetAxis("Horizontal"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Untouchable"))
        {
            GameObject explosionObj = Instantiate<GameObject>(explosion);
            explosionObj.transform.position = transform.position;

            camera.transform.SetParent(null);
            SpaceExplorationGameManager.manager.SpawnShip();
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("Goal"))
        {
            SpaceExplorationGameManager.manager.winText.gameObject.SetActive(true);
            SpaceExplorationGameManager.manager.GameWon();
        }
    }
}
