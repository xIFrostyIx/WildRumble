using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=yDNZpkyosrA Tutorial followed by Alex Wolfe

public class Connector : MonoBehaviour
{

    public LineRenderer lineRenderer;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool makeConnection
; 
    public void MakeConnection (Vector3 startPos,Vector3 endPos)
    {
        startPosition = startPos;
        endPosition = endPos;

        makeConnection = true;

    }
    private void Update()
    {
        if (makeConnection) 
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);

        }
    }
}
