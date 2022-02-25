using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public void ADestroy()
    {
        Destroy(gameObject);    
    }

    public void ADesable()
    {
        gameObject.SetActive(false);
    }
}
