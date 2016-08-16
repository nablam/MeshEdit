using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class nabFileWriter {

   public static void writeMesh(Mesh aMesh)
    {
        string[] TriTitle = { "_triangles :",aMesh.triangles.Length.ToString() };
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
}
