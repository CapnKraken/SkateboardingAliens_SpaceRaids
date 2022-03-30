using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//attaches to prefabs for the buildings
public class PlaceableObject : MonoBehaviour
{
    //Donovan Lott
    public bool Placed { get; private set; }

    public Vector3Int Size { get; private set; }
    public Vector3[] Vertices;

    private void GetColliderVertexPositionsLocal()
    {
        //gets the corners of the bottom of the box collider on the object
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }
    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildMode.current.gridLayout.WorldToCell(worldPos);
        }
        Size = new Vector3Int(Math.Abs((vertices[0] - vertices[1]).x), Math.Abs((vertices[0] - vertices[3]).y), 1); //gets the absolute value of the coordinates
    }

    public Vector3 GetStartPosition() 
    {
        return transform.TransformPoint(Vertices[0]);
    }

    public void Start()
    {
        if (gameObject.GetComponent<BoxCollider>().enabled)
        {
            GetColliderVertexPositionsLocal();
            CalculateSizeInCells();
        }

    }

    public virtual void Place()
    {
        Placed = true;
    }
}
