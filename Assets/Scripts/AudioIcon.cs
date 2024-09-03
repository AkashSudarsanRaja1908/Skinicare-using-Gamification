using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioIcon : MonoBehaviour {
    public Sprite onSprite, offSprite;

    private Image _image;

	// Use this for initialization
	void Awake () {
        _image = GetComponent<Image>();
	}

    public void SpriteSwitch()
    {
        if (_image.sprite == onSprite)
        {
            _image.sprite = offSprite;
        }
        else
        {
            _image.sprite = onSprite;
        }
    }
}
