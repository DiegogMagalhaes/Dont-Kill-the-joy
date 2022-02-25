using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallScene : MonoBehaviour
{
    [SerializeField] UIManager UIManager;

    void Update()
    {
        if (Input.anyKeyDown)
            UIManager.Play();
    }
}
