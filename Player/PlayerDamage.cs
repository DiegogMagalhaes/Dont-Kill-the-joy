using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] UIManager UIManager;
    [SerializeField] GameManager gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DamageSource")
            kill();            
    }

    private void kill()
    {
        UIManager.SwitchGameOver();
        gm.StopGame();
        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(-4.5f, -0.04f, 0f);
        GameObject.Find("Timer").gameObject.SetActive(false);
        SoundMannager.PlaySound(SoundMannager.songs_name.Hit);

        Debug.Log("kill");
    }
}
