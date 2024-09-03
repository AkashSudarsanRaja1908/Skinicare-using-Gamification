using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour {
    public bool show = true;
    public Developer dev;
    public Help help;

    public void Show_Hide()
    {
        if (show)
        {
            if (!dev.show)
            {
                dev.gameObject.GetComponent<Animator>().SetTrigger("Hide");
                dev.show = true;
            }
            else if (!help.show)
            {
                help.gameObject.GetComponent<Animator>().SetTrigger("Hide");
                help.show = true;
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
