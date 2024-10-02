using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{

    public Transform player;

    Vector2 playerDirection;
    public Vector2 center;
    public Vector2 size;
    public float angle;
    public float beamSize;
    public float width;
    public bool isTracking;

    //public List<Visualisation> objs = new List<Visualisation>();

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            PlayerDirection();
        }

        Test();
    }

    void PlayerDirection() 
    {
        playerDirection = ((Vector2)player.position - (Vector2)transform.position).normalized;
        angle = Vector2.SignedAngle(Vector2.right, playerDirection);
    }


    void Test()
    {
        //objs.Clear();

        center = (Vector2)transform.position + (playerDirection * beamSize);
        size = new Vector2(beamSize * 2, width);

        Collider2D[] cols = Physics2D.OverlapBoxAll(center, size, angle);

        //Collider2D[] cols = Physics2D.OverlapBoxAll(((Vector2)transform.position + (playerDirection * beamSize)), (new Vector2(beamSize * 2, width)), Vector2.SignedAngle(Vector2.right, playerDirection));


        //foreach (Collider2D col in cols)
        //{
        //    Visualisation newColFound = new Visualisation();
        //    newColFound.col = col;
        //    newColFound.distance = Vector2.Distance(transform.position, col.transform.position);
        //    objs.Add(newColFound);
        //}
    }
}

//[System.Serializable]
//public class Visualisation
//{
//    public Collider2D col;
//    public float distance;
//}
