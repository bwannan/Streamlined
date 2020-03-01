using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteControl : NetworkBehaviour
{
    Vector3 mousePos;

    // need to use FixedUpdate for rigidbody
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }
}
