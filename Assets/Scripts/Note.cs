using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    void Start()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Hit();
        }
    }

    void Hit()
    {
        ScoreManager.Instance.Hit();
        Destroy(gameObject);
    }
}