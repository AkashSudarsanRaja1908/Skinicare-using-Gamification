using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            if (!other.collider.GetComponent<PlayerController>().IsDead)
            {
                LevelManager.instance.KillPlayer(gameObject);
            }
        }
    }
}
