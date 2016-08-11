using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace mymesh {
    public class MeshExt : MonoBehaviour
    {

        public void OnBtn1() {
           Debug.Log("click");
            Removeone(); RepaintMesh();      
            }
        public void OnBtn2()
        {
            Debug.Log("click2");
            Removeone(); RepaintMesh();
        }
        public void OnBtn3() { }

        Mesh _mesh;
        TRImanager triMangr;

        void Start()
        {
            _mesh = GetComponent<MeshFilter>().mesh;

       

            triMangr = new TRImanager(_mesh);
        }

        void Removeone() { triMangr.Remove_TRI(0); }

        void RepaintMesh() {
            // _mesh.Clear();
            _mesh.triangles = triMangr.GetTriARRA();
            _mesh.vertices = triMangr.GetVertARRA().ToArray();
           // _mesh.RecalculateBounds();
        }

        void Update()
        {

        }
    }
}

