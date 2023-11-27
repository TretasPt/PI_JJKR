using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Path
{
    private NavMeshPath path;
    private int iterator;

    public Path(Vector3 source, Vector3 destination)
    {
        path = new NavMeshPath();
        //source.y = 0;                                                                               //DELETE
        //destination.y = 0;                                                                          //DELETE
        NavMesh.CalculatePath(source, destination, NavMesh.AllAreas, path);
        iterator = 0;
    }

    public Vector3 getWaypoint(Vector3 currentPosition)
    {
        if (iterator >= path.corners.Length)
            return currentPosition;
        else if ((path.corners[iterator] - currentPosition).magnitude < 1.5 && iterator+1 < path.corners.Length)                             // TODO  Nao fazer hardcoded
            return path.corners[++iterator];
        else
            return path.corners[iterator];
    }
};
