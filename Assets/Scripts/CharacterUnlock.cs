using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnlock : MonoBehaviour {

    public Sprite unlocked, active;

    private Image _image;
	// Use this for initialization
	void Awake () {
        _image = GetComponent<Image>();
	}
    public void Unlock()
    {
        _image.sprite = unlocked;
    }
    public void Activate()
    {
        _image.sprite = active;
    }
	
}
