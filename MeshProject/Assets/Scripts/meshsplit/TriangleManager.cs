using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
namespace meshsplit
{
    public class TriangleManager
    {
        List<Triangle> _TRILIST;
        List<Triangle> _TRILIST_OntheCut;
        List<int> _trianglesList;
        List<Vector3> _verteciesList;
        List<Vector3> _normals;
        GameObject _P1;

        public TriangleManager(Mesh aMEsh, GameObject p1) {
            _TRILIST = new List<Triangle>();
            _TRILIST_OntheCut = new List<Triangle>();
            _trianglesList = aMEsh.triangles.ToList();
            _verteciesList = aMEsh.vertices.ToList();
            _normals = aMEsh.normals.ToList();
            _P1 = p1;
//  nabFileWriter.writeMesh(aMEsh);
            Init_TRILIST();


            Build_vertList_from_TRILIST();
            Build_triangleList_fromTRILIST();

//  nabFileWriter.Write_TriInfo(_TRILIST, "precut");

//  nabFileWriter.GenericWriter(_trianglesList);
//  nabFileWriter.GenericWriter(_verteciesList);
//  nabFileWriter.writeMeshNormals(aMEsh);

        }



        void Init_TRILIST()
        {
            int NMax = _normals.Count;
            int N = 0;
            int TMax = _trianglesList.Count;
            for (int idCnt = 0; idCnt < TMax / 3; idCnt++)
            {               
                TriVert[] VERTarra = new TriVert[3];
                for (int _localOffset = 0; _localOffset < 3; _localOffset++)
                {
                    
                    int i = idCnt * 3 + _localOffset;
                  
                    int valAti = _trianglesList[i];
                    
                     Vector3 v3 = _verteciesList[valAti];

                    TriVert aVERT = new TriVert( v3, 'L');
                    VERTarra[_localOffset] = aVERT;
                   
                }
                Triangle aTRI = new Triangle(idCnt, VERTarra, _P1);
                //Debug.Log(idCnt);

                // aTRI._myNormal = _normals[idCnt% _normals.Count];
                aTRI._myNormal = _normals[idCnt/3];

                _TRILIST.Add(aTRI);
            }
        }

        void Build_vertList_from_TRILIST()
        {
            _verteciesList.Clear();
            _verteciesList = _TRILIST.SelectMany(s => s.TVarra).Select(e=>e.VV).Distinct<Vector3>().ToList(); 
        }

        void Build_triangleList_fromTRILIST() {
            _trianglesList.Clear();
            foreach (Triangle t in _TRILIST) {
                for (int x = 0; x < 3; x++) {
                    int index = _verteciesList.IndexOf(t.TVarra[x].VV);
                    _trianglesList.Add(index);
                }
            }

        }

        //GENERICm GetVerts Must be assigne first
        public List<Vector3> GENERICBuild_vertList_from(List<Triangle> aTlist)
        {
             
            return aTlist.SelectMany(s => s.TVarra).Select(e => e.VV).Distinct<Vector3>().ToList();
        }
        //GENERICm gettri  Must be assigne second
        public List<int> GENERICBuild_triangleList_from(List<Triangle> aTlist, List<Vector3> aVlist)
        {
            List<int> tempTriList = new List<int>();            
            foreach (Triangle t in aTlist)
            {
                for (int x = 0; x < 3; x++)
                {
                    int index = aVlist.IndexOf(t.TVarra[x].VV);
                    tempTriList.Add(index);
                }
            }
            return tempTriList;

        }

        /// <summary>
        /// 
        /// </summary>
        public void Set_TRILIST_intersects() {
            foreach (Triangle t in _TRILIST) { t.SetIntersects(); }
        }

        public List<Triangle> Get_TRILIST() { return this._TRILIST; }

        //public List<Vector3> find_all_RightOfPlane() {
        //    Vector3 p1Normal = _P1.GetComponent<PLANE>().PlaneNormal;        
        //    return _verteciesList.Select(e => e).Where(v => Vector3.Dot(v-_P1.transform.position, p1Normal) > 0.0f).ToList(); 
        //}

        public void Sel_LRonly_no_O() {
            Vector3 p1Normal = _P1.GetComponent<PLANE>().PlaneNormal;
            foreach (Triangle t in _TRILIST)
            {
                foreach (TriVert tv in t.TVarra)
                {
                    if (Vector3.Dot(tv.VV - _P1.transform.position, p1Normal) > 0.0f) { tv.LRO = 'R'; }
                    else
                    { tv.LRO = 'L'; }
                }
            }
        }

        public List<Triangle> find_all_RightOfPlane_fromTRILIST()
        {
            Sel_LRonly_no_O();
            List<Triangle> temp_TRI = new List<Triangle>();
            foreach (Triangle t in _TRILIST)
            {       
                if (t.ALLmyVertsAreOntheRigh())
                    temp_TRI.Add(t);
            }

            return temp_TRI;
        }


        //old way to find all tris even the onse on the cut line
        //public List<Triangle> find_all_RightOfPlane_fromTRILIST() {
        //    List<Triangle> temp_TRI= new List<Triangle>();
        //    Vector3 p1Normal = _P1.GetComponent<PLANE>().PlaneNormal;
        //    foreach (Triangle t in _TRILIST) {
        //        bool isOnRight = false;
        //        foreach (TriVert tv in t.TVarra) {
        //            if (Vector3.Dot(tv.VV - _P1.transform.position, p1Normal) > 0.0f) { tv.LR = 'R'; isOnRight = true; }
        //        }
        //        if(isOnRight)
        //        temp_TRI.Add(t);
        //    }

        //    return temp_TRI;
        //}



        public void ADDTRI(Triangle t) {
            int nextId = _TRILIST.Count;
            t.ID = nextId;
            _TRILIST.Add(t);

        }

        public List<Triangle> Remove_trisOnPathFromTRILIST()
        {
            List<Triangle> TRI_ON_path = new List<Triangle>();
            List<Triangle> tri_NOT_on = new List<Triangle>();
            for (int x = 0; x < _TRILIST.Count; x++) {
             Triangle t = _TRILIST[x];
             //   Debug.Log("checking tri" + t.ID + " " + t.obj_L_R_OP_X);
                if (t.obj_L_R_OP_X=='O')
                {
                    TRI_ON_path.Add(t);
                }
                else
                    tri_NOT_on.Add(t);
            }

            _TRILIST.Clear();
 
            _TRILIST = tri_NOT_on;
            nabFileWriter.Write_TriInfo(_TRILIST, "TRILIST_not_on");
            nabFileWriter.Write_TriInfo(TRI_ON_path, "_list_on");


            return TRI_ON_path;

        }

    }
}

