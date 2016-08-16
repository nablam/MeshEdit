using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using  meshsplit;
public static class nabDrawHelper {


    public static void DrawLine(GameObject P1, List<Triangle> trilist) {
        foreach (Triangle t in trilist) {
            if(t.seg01_crossed)
            Debug.DrawLine(P1.transform.position, t.x_01, Color.green, 5);
            if (t.seg12_crossed)
                Debug.DrawLine(P1.transform.position, t.x_12, Color.green, 5);
            if (t.seg20_crossed)
                Debug.DrawLine(P1.transform.position, t.x_20, Color.green, 5);
        }
    }


    public static void DrawLine(GameObject P1, List<Vector3> vrilist, Color col)
    {
        foreach (Vector3 v in vrilist)
        { 
                Debug.DrawLine(P1.transform.position, v, col, 15);
           
        }
    }
}
