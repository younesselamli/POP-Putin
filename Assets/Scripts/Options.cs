using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
    public void LinkHelpEMAIL()
    {
        string t =
        "mailto:YounesDevHelp@gmail.com?subject=Question%20on%20Awesome%20PopPutin";
        Application.OpenURL(t);
    }


}
