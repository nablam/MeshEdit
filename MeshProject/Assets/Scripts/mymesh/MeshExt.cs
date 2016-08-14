using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace mymesh {
    public class MeshExt : MonoBehaviour
    {
        public GameObject P1;
        PLANE pscript;
        public void OnBtn1() {
           Debug.Log("click1");
            Removeone(); RepaintMesh();      
            }
        public void OnBtn2()
        {
            Debug.Log("click2");
            triMangr.DrawAllNormals();

        }
        public void OnBtn3() {
            // BreakupTri(0);
            DrawIntersectionLine(P1);
                }

        Mesh _mesh;
        TRImanager triMangr;

        void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;    
            triMangr = new TRImanager(_mesh, false); //false for is Smooth . if not smooth then there are 3 normals per triangle. if smooth, shit gets tough since shared verteces share a normal too
            pscript = P1.GetComponent<PLANE>();
        }

        void Removeone() { triMangr.Remove_TRI_at0(); }

        void RepaintMesh() {
            // _mesh.Clear();
            _mesh.triangles = triMangr.GetTriARRA();
            _mesh.vertices = triMangr.GetVertARRA().ToArray();
           // _mesh.RecalculateBounds();Do not use , i will recalc sooth normal counts and give you all verts normals instead of just 1 for shared verts
        }

        void placeVertAt(Vector3 here)
        {
            GameObject go = Instantiate(Resources.Load("sphereVert"), here + transform.position, Quaternion.identity) as GameObject;
        }

        void BreakupTri(int i) {
            //get mid o,1 and 02 
            TRI tri = triMangr.GetTriByListIndex(i);
            Vector3 p1 = getMid12(tri);
            Vector3 p2 = getMid23(tri);
            placeVertAt(p1);
            placeVertAt(p2);

        }

        Vector3 getMid12(TRI tri) {
            return (tri._V0._Vv + tri._V1._Vv) / 2;
        }

        Vector3 getMid23(TRI tri)
        {
            return (tri._V2._Vv + tri._V1._Vv) / 2;
        }
   
        void DrawIntersectionLine(GameObject GO) {
            Debug.Log("dyoyo");
            Vector3 pos = GO.transform.position;
            //drawnormal of plane

            Vector3 planeNormal = pscript.PlaneNormal;
            // Debug.DrawLine(Vector3.zero, planeNormal, Color.grey, 10);
            triMangr.DrawIntersection(planeNormal, GO);




        }
        //transform.InverseTransformDirection(transform.position.


        void Update()
        {
            //Mesh mesh = GetComponent<MeshFilter>().mesh;
            //Vector3[] normals = mesh.normals;
            //mesh.RecalculateNormals();

            //Quaternion rotation = Quaternion.AngleAxis(Time.deltaTime * 100f, Vector3.right);
            //for (int i = 0; i < normals.Length; i++)
            //    normals[i] = rotation * normals[i];

            //mesh.normals = normals;

            
        }

    }
}

