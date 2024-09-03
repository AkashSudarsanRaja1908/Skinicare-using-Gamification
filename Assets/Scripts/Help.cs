using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour {
    public bool show = true;
    public Developer dev;
    public Characters characters;

    public void Show_Hide()
    {
        if (show)
        {
            if (!dev.show)
            {
                dev.gameObject.GetComponent<Animator>().SetTrigger("Hide");
                dev.show = true;
            }
            else if (!characters.show)
            {
                characters.gameObject.GetComponent<Animator>().SetTrigger("Hide");
                characters.show = true;
            }
                GetComponent<Animator>().SetTrigger("Show");
                show = false;
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Hide");
            show = true;
        }
    }
}
