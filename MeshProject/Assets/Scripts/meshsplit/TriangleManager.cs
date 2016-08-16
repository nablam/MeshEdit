using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
namespace meshsplit
{
    public class TriangleManager
    {
        List<Triangle> _TRILIST;
        List<int> _trianglesList;
        List<Vector3> _verteciesList;
        List<Vector3> _normals;
        GameObject _P1;

        public TriangleManager(Mesh aMEsh, GameObject p1) {
            _TRILIST = new List<Triangle>();
            _trianglesList = aMEsh.triangles.ToList();
            _verteciesList = aMEsh.vertices.ToList();
            _normals = aMEsh.normals.ToList();
            _P1 = p1;
            nabFileWriter.writeMesh(aMEsh);
            Init_TRILIST();
            Build_vertList_from_TRILIST();
           Build_triangleList_fromTRILIST();
          

            nabFileWriter.GenericWriter(_trianglesList);
            nabFileWriter.GenericWriter(_verteciesList);
            nabFileWriter.writeMeshNormals(aMEsh);

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

        public void Calc_Intersects() {
            foreach (Triangle t in _TRILIST) { t.CheckAllSegmentsForIntersects(); }
        }

        public List<Triangle> Get_TRILIST() { return this._TRILIST; }

        public List<Vector3> find_all_RightOfPlane() {
            Vector3 p1Normal = _P1.GetComponent<PLANE>().PlaneNormal;        
            return _verteciesList.Select(e => e).Where(v => Vector3.Dot(v-_P1.transform.position, p1Normal) > 0.0f).ToList(); 
        }

        public List<Vector3> find_all_LeftOfPlane()
        {
            return _verteciesList.Select(e => e).Where(v => Vector3.Dot(v.normalized, _P1.transform.rotation.eulerAngles.normalized) < 0.0f).ToList();
        }

    }
}

