using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  
    public Vector3 offset;    

    void LateUpdate()
    {
        if (player != null)
        {
            // Solo se actualizan los ejes X e Y, manteniendo la profundidad Z fija
            Vector3 newPosition = new Vector3(player.position.x, player.position.y, offset.z);
            transform.position = newPosition + offset;
        }
    }
}
