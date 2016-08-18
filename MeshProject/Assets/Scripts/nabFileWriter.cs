using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using meshsplit;

public static class nabFileWriter {

    public static void writeMesh(Mesh aMesh)
    {
        string[] TriTitle = { "_triangles :", aMesh.triangles.Length.ToString() };
        System.IO.File.WriteAllLines(@"C:\UnityLogs\Mesh_trianges.txt", TriTitle);
        using (System.IO.StreamWriter TriWriter =
        new System.IO.StreamWriter(@"C:\UnityLogs\Mesh_trianges.txt", true))
        {
            foreach (string s in aMesh.triangles.Select(t => t.ToString()).ToArray())
                TriWriter.WriteLine(s);
        }


        string[] verteciestitles = { "_verteciess :", aMesh.vertexCount.ToString() };
        System.IO.File.WriteAllLines(@"C:\UnityLogs\Mesh_vertecies.txt", verteciestitles);
        using (System.IO.StreamWriter TriWriter =
        new System.IO.StreamWriter(@"C:\UnityLogs\Mesh_vertecies.txt", true))
        {
            foreach (string s in aMesh.vertices.Select(t => t.ToString()).ToArray())
                TriWriter.WriteLine(s);
        }


    }

    public static void write_vertList(List<Vector3> _aVertList)
    {
        string[] title1 = { "The verts stored :", "\n" };
        System.IO.File.WriteAllLines(@"C:\UnityLogs\List_verts.txt", title1);
        using (System.IO.StreamWriter TriWriter =
        new System.IO.StreamWriter(@"C:\UnityLogs\Mesh_trianges.txt", true))
        {
            foreach (Vector3 v3 in _aVertList)
                TriWriter.WriteLine(v3.ToString());
        }
    }

    public static void GenericWriter(object alist)
    {
        if (alist is List<Vector3>)
        {
            List<Vector3> lv3 = alist as List<Vector3>;

            string[] title1 = { "Verts in this list :", "\n" };
            System.IO.File.WriteAllLines(@"C:\UnityLogs\List_verts.txt", title1);
            using (System.IO.StreamWriter TriWriter =
            new System.IO.StreamWriter(@"C:\UnityLogs\List_verts.txt", true))
            {
                foreach (Vector3 v3 in lv3)
                    TriWriter.WriteLine(v3.ToString());
            }


        }
        else
            if (alist is List<int>)
        {
            List<int> lt = alist as List<int>;

            string[] title1 = { "The Triangles in lis :", "\n" };
            System.IO.File.WriteAllLines(@"C:\UnityLogs\List_triangles.txt", title1);
            using (System.IO.StreamWriter TriWriter =
            new System.IO.StreamWriter(@"C:\UnityLogs\List_triangles.txt", true))
            {
                foreach (int i in lt)
                    TriWriter.WriteLine(i.ToString());
            }

        }


    }

    public static void writeMeshNormals(Mesh aMesh) {

        string[] TriTitle = { "_normals :", aMesh.normals.Length.ToString() };
        System.IO.File.WriteAllLines(@"C:\UnityLogs\Mesh_normals.txt", TriTitle);
        using (System.IO.StreamWriter TriWriter =
        new System.IO.StreamWriter(@"C:\UnityLogs\Mesh_normals.txt", true))
        {
            foreach (string s in aMesh.normals.Select(t => t.ToString()).ToArray())
                TriWriter.WriteLine(s);
        }

    }


    public static void Write_TriInfo(List<Triangle> _triList, string ext) {

        string path = @"C:\UnityLogs\_TRISinfo"+ext+ ".txt";

        string s1 = " ALL TRI : count=" + _triList.Count.ToString();
        string s2 = " ObjL    : count=" + _triList.Where(e => e.obj_L_R_OP_X == 'L').Select(e => e).Count().ToString();
        string s3 = " ObjR    : count=" + _triList.Where(e => e.obj_L_R_OP_X == 'R').Select(e => e).Count().ToString();
        string s4 = " onPath  : count=" + _triList.Where(e => e.obj_L_R_OP_X == 'O').Select(e => e).Count().ToString();
        // string s4 = " onPath  : count=" + _triList.Where(e => e.IsOnCutPath).Select(e => e).Count().ToString();


        string[] Lines = { s1, s2, s3, s4, "\n" };
        System.IO.File.WriteAllLines(path, Lines);
        using (System.IO.StreamWriter TriWriter =
        new System.IO.StreamWriter(path, true))
        {
            foreach (Triangle t in _triList) {
                TriWriter.WriteLine("MAIN TRI="+t.ToString());
                TriWriter.WriteLine("----------------");
                TriWriter.WriteLine(t.T0 == null ? "noT0" : t.T0.ToString());
                TriWriter.WriteLine(t.T1 == null ? "noT1" : t.T1.ToString());
                TriWriter.WriteLine(t.T2 == null ? "noT2" : t.T2.ToString());
                TriWriter.WriteLine("##########################");
                


        }
            // foreach (string s in _triList.Select(t => t.ToString()).ToArray())
            //     TriWriter.WriteLine(s);
        }
    }
}
