using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Developer : MonoBehaviour {
    public bool show = true;
    public Help help;
    public Characters characters;

    public void Show_Hide()
    {
        if (show)
        {
            if (!help.show)
            {
                help.gameObject.GetComponent<Animator>().SetTrigger("Hide");
                help.show = true;
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
