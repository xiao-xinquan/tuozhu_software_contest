using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_type
{
    public string Name {get; private set;}

    public string Path {get; private set;}

    public UI_type(string path)
    {
        Path = path;
        Name = path.Substring(path.LastIndexOf('/') + 1);
    
    }

}
