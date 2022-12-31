using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;

    private Vector3 currentPosition;
    public Vector3 CurrentPosition => currentPosition;

    private bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (!gameStarted && transform.hasChanged)
        {
            currentPosition = transform.position;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + currentPosition, 0.5f);

            if (i < points.Length - 1)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(points[i] + currentPosition, points[i + 1] + currentPosition);
            }
        }
    }
}
