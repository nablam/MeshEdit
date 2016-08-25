using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace meshsplit
{

    public class MeshObjectManager : MonoBehaviour
    {

         GameObject P1;

        PLANE pscript;
        Mesh _mesh;
        TriangleManager tm;

        //NEWOBJECT
        GameObject EMPTY;
        Vector3[] _Vertices2;
        int[] _Triangles2;
        public Material mat;
        Mesh _meshHalf;
        //NEWOBJECT

        public Material mat2;
        public Material mat3;

        GameObject EMPTYL;
        Mesh _MeshLeftHalf;
        GameObject EMPTYR;
        Mesh _MeshRighttHalf;




        public void OnGetVerts()
        {
           // Debug.Log("click1 intersets");
            tm.Set_TRILIST_intersects();
            nabDrawHelper.DrawLine(P1, tm.Get_TRILIST());
        }

        public void OnShowRIGHTVerts() {
            //   Debug.Log("click find lef verts ");
            nabFileWriter.Write_TriInfo(tm.Get_TRILIST(), "BEFORE_CUT");
            tm.Set_LRonly_no_O();
            nabFileWriter.Write_TriInfo(tm.Get_TRILIST(), "after_CUT");
            nabDrawHelper.DrawLine(P1,  tm.Get_TRILIST().SelectMany(t=>t.TVarra).Where(e=>e.LRO == 'R').Select(e=>e.VV).ToList() , Color.blue);
          
        }

        public void OnMakeHalf() {
            Debug.Log("WOW I'm there");
            SplitMe();
        }

        
        


        void Awake()
        {
            P1 = GameObject.Find("P1");
            _mesh = GetComponent<MeshFilter>().mesh;
            tm = new TriangleManager(_mesh, P1); 
            pscript = P1.GetComponent<PLANE>();
        }



        void ExecuteAcut() {
            tm.Set_TRILIST_intersects(); //will fill x_01, x_10....
           // nabFileWriter.Write_TriInfo(tm.Get_TRILIST(), "_After_SetIntersects");

            List<Triangle> TEMPoNpATH = tm.Remove_trisOnPathFromTRILIST();
           
            int NEWID = tm.Get_TRILIST().Count;
            List<Triangle> tempListOfTrisOnPath_s_explodedTris = new List<Triangle>();
            foreach (Triangle t in TEMPoNpATH) {
                
                    t.BreakUp_Tri_Set_T0_T1_T2_or_T1_T2();
                if (t.T0 != null)
                {
                    t.T0.ID = NEWID++;
                    tempListOfTrisOnPath_s_explodedTris.Add(t.T0);
                }


                    t.T1.ID = NEWID++;
                    tempListOfTrisOnPath_s_explodedTris.Add(t.T1);

                    t.T2.ID = NEWID++;
                    tempListOfTrisOnPath_s_explodedTris.Add(t.T2);
                
            }

            for (int x = 0; x < tempListOfTrisOnPath_s_explodedTris.Count; x++) {
                tm.ADDTRI(tempListOfTrisOnPath_s_explodedTris[x]);
            }
        }

        List<Triangle> _TRILIST_OBJL;
        List<Triangle> _TRILIST_OBJR;
        List<int> _triL;
        List<Vector3> _vertL;
        List<int> _triR;
        List<Vector3> _vertR;   

        void SplitMe()
        {
            ExecuteAcut();

            nabFileWriter.Write_TriInfo(tm.Get_TRILIST(), "_After_SetIntersects");


            _TRILIST_OBJL = tm.Get_TRILIST().Where(e => e.obj_L_R_OP_X == 'L').Select(e => e).ToList();
            _vertL = tm.GENERICBuild_vertList_from(_TRILIST_OBJL);
            _triL = tm.GENERICBuild_triangleList_from(_TRILIST_OBJL, _vertL);


            _TRILIST_OBJR = tm.Get_TRILIST().Where(e => e.obj_L_R_OP_X == 'R').Select(e => e).ToList();
            _vertR = tm.GENERICBuild_vertList_from(_TRILIST_OBJR);
            _triR = tm.GENERICBuild_triangleList_from(_TRILIST_OBJR, _vertR);



            _MeshLeftHalf = new Mesh();
            EMPTYL = new GameObject(gameObject.name + "_l");
            var meshFilter = EMPTYL.AddComponent<MeshFilter>();
            EMPTYL.AddComponent<MeshRenderer>().material = mat2;
            meshFilter.sharedMesh = _MeshLeftHalf;
            _MeshLeftHalf.vertices = _vertL.ToArray();
            _MeshLeftHalf.triangles = _triL.ToArray();
         //   EMPTYL.AddComponent<MeshObjectManager>();


            _mesh.Clear();
            // _mesh.vertices = _vertR.ToArray();
            // _mesh.triangles = _triR.ToArray();

            _MeshRighttHalf = new Mesh();
            EMPTYR = new GameObject(gameObject.name + "_r");
            var meshFilter2 = EMPTYR.AddComponent<MeshFilter>();
            EMPTYR.AddComponent<MeshRenderer>().material = mat3;
            meshFilter2.sharedMesh = _MeshRighttHalf;
            _MeshRighttHalf.vertices = _vertR.ToArray();
            _MeshRighttHalf.triangles = _triR.ToArray();
          //  EMPTYR.AddComponent<MeshObjectManager>();
        }

    }
}
