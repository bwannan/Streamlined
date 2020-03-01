using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOffscreen : MonoBehaviour
{
    float boundsX = 10f;
    float boundsY = 10f;
    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if(pos.x < -1*boundsX || pos.x > boundsX || pos.y < -1*boundsY || pos.y > boundsY)
        {
            Destroy(this);
        }
    }
}
