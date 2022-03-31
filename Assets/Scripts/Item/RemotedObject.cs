using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotedObject : Item
{
    // Start is called before the first frame update
    public enum RemoteType
    {
    LIGHT,
    ANIMATION
    }
    public RemoteType object_remotetype;

    public override void interaction()
    {
        switch (object_remotetype)
        {
            case RemoteType.LIGHT:
                GetComponent<Lighting>().on_off = true;
                GetComponent<Lighting>().Light_Update();
                break;
            case RemoteType.ANIMATION:

                break;
        }

    }
}
