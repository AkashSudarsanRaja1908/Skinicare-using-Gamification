using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour {
    public Sprite onSprite, offSprite;
    public GameObject achievementMenu;

    private Image _image;

	// Use this for initialization
	void Awake () {
        _image = GetComponent<Image>();
	}

    public void Switch()
    {
        if (_image.sprite == onSprite)
        {
            _image.sprite = offSprite;
            achievementMenu.SetActive(false);
        }
        else
        {
            _image.sprite = onSprite;
            achievementMenu.SetActive(true);
        }
    }
}
