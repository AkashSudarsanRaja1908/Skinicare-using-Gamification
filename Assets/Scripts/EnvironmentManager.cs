using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

    public GameObject newStreet;
    public GameObject currentStreet;
    
    private GameObject _oldStreet;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Start")
        {
            if (_oldStreet != null)
            {
                Destroy(_oldStreet);
            }
            else
            {
                Destroy(GameObject.Find("MainScene"));
            }
        }
        else if (other.tag == "Middle")
        {
            SpawnLevel();
        }
    }
    void SpawnLevel()
    {
        _oldStreet = currentStreet;
        currentStreet = (GameObject)Instantiate(newStreet, currentStreet.transform.GetChild(11).position, Quaternion.identity);

    }
}
