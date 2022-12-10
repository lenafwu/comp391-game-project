using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public void OpenLink()
    {
        Application.OpenURL("https://github.com/lenafwu/comp391-game-project");
    }
}
