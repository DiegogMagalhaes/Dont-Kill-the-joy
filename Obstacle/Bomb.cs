using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject Explosion;

    private Rigidbody2D BombRb;

    void Start()
    {
        BombRb = gameObject.GetComponent<Rigidbody2D>();
        BombRb.velocity = new Vector2(0, -1 * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ground")
        {
            SoundMannager.PlaySound(SoundMannager.songs_name.Explosion);
            Instantiate(Explosion).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
