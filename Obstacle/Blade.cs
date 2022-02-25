using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject blade;
    [SerializeField] private float speed;
    

    public void SpawnBlade()
    {
        
        GameObject temp = Instantiate(blade);
        temp.transform.position = new Vector3(-9.4f, transform.position.y, 0f);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
        SoundMannager.PlaySound(SoundMannager.songs_name.Blade);
        Destroy(temp, 1f);
    }
}
