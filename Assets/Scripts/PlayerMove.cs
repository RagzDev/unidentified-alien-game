using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PlayerMove : MonoBehaviour {
    public float speed = 2;
    public float force = 300;
    
    int AttemptCount;
    public AudioClip explosion;  // set the closed door sound clip set the locked door sound clip
    public AudioSource audioSource;
    public AudioSource musicSource;

    
    void Start () {    
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    void Update () {
        if (Input.touchCount > 0) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        }   
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.transform.gameObject.name == "Portal") {
            SceneManager.LoadScene("ModeSelect");
        } else {

            GameObject player = GameObject.Find("Player");
            GameObject explosionParticle = GameObject.Find("explosion");
            ParticleSystem ps = explosionParticle.GetComponent<ParticleSystem>();

            explosionParticle.transform.position = player.transform.position;

            audioSource.PlayOneShot(explosion);
            musicSource.Stop();
            ps.Play();
            player.SetActive(false);
            Invoke("restart", 1);
        }
    }

    void restart() {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}