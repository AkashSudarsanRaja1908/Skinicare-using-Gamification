using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;

    private Vector3 _target = Vector3.zero;

	
	// Update is called once per frame
	void Update () {
        _target = new Vector3(player.position.x, (player.position.y + 10) / 1.5f, player.position.z - 15);
        transform.position = Vector3.Lerp(transform.position, _target, 1f);
 
	}
}
