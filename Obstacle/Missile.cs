using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private float LaunchTime;

    private Rigidbody2D MissileRb;
    private Animator animMissile;
    private bool Launched = false;
   
    void Start()
    {
        MissileRb = gameObject.GetComponent<Rigidbody2D>();
        animMissile = gameObject.GetComponent<Animator>();

        Destroy(gameObject, 20f);
    }

    private void Launch()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
        animMissile.SetBool("Launch", true);
        MissileRb.velocity = new Vector2(0,speed);
        Launched = true;
    }

    private void Update()
    {
        if (LaunchTime <= 0 && !Launched)
        {
            Launch();
        }
        else if(!Launched)LaunchTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Launched)
        {
            SoundMannager.PlaySound(SoundMannager.songs_name.Explosion);
            Instantiate(Explosion).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
