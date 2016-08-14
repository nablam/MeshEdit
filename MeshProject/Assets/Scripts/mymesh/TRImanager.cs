using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace mymesh {
    public class TRImanager 
    {
        List<TRI> _TRILIST;
        List<int> _trianglesList;
        List<Vector3> _verteciesList;
        List<Vector3> _normals;
        bool _isSmooth;

        public int CNTTRI = 0;

        //CONSTRUCTOR***************************************
        public TRImanager(Mesh aMEsh, bool isSmooth) {
            _isSmooth = isSmooth;
            Debug.Log("manager started");
            _TRILIST = new List<TRI>();
            _trianglesList = aMEsh.triangles.ToList();
            _verteciesList = aMEsh.vertices.ToList();
            _normals = aMEsh.normals.ToList();
            Init_TRILIST();

           // Debug.Log("Ts " + aMEsh.triangles.Count());
            Debug.Log("Vs " + aMEsh.vertices.Count());
           // Debug.Log("Ns " + aMEsh.normals.Count());
           // foreach (Vector3 n in aMEsh.normals) { Debug.Log("norm " + n); }

        }
        //CONSTRUCTOR***************************************

        public int[] GetTriARRA()
        {
            //for (int i = 0; i < _trianglesList.Count; i++) Debug.Log("{{{{" + i + "," + _trianglesList[i] + " }}}}}");              
            return _trianglesList.ToArray();
        }
        public Vector3[] GetVertARRA() { return _verteciesList.ToArray(); }
        public List<Vector3> GetVertList2() { return _verteciesList; }

        public void Remove_TRI(int TID)
        {
            int TID2 = 0;
            TRI tritoremove = _TRILIST.Find(e => e.id == CNTTRI);
            _TRILIST.Remove(tritoremove);
            Update_triangleList();

            CNTTRI++;
        }

        public void Remove_TRI_at0()
        {
            int TID2 = 0;
            TRI tritoremove = _TRILIST.ElementAt(0);
            _TRILIST.Remove(tritoremove);
            Update_triangleList();

            CNTTRI++;
        }

        public TRI GetTriByID(int x) { return _TRILIST.Find(e => e.id == x); }
        public TRI GetTriByListIndex(int x) { return _TRILIST.ElementAt(x); }
        public Vector3 getCenterOfTri(int x)
        {
            Debug.Log("ok" + x);
            TRI thetri = _TRILIST.Find(e => e.id == x);
            Vector3 temp1 = (thetri._V1._Vv + thetri._V0._Vv) / 2;
            Vector3 temp2 = (temp1 + thetri._V2._Vv) / 2;
            Debug.Log(temp2);


            // Vector3 temp2 = new Vector3(1, 1, 1);
            return temp2;
        }

        public void DrawAllNormals() {
            foreach (TRI tri in _TRILIST) tri.Draw_mynormal();
        }

        public void DrawIntersection(Vector3 p2NormalZero, GameObject P1) {
            foreach (TRI tri in _TRILIST) {
                //  tri.Draw_Plane_segment_intersection(p2NormalZero, p_pos);
                tri.DoAllIntersects_aandDraw(p2NormalZero, P1);
            }
        }






        void Init_TRILIST()
        {
            int TMax = _trianglesList.Count;
            for (int idCnt = 0; idCnt < TMax / 3; idCnt++)
            {
                VERT[] VERTarra = new VERT[3];
                for (int _localOffset = 0; _localOffset < 3; _localOffset++)
                {
                    int i = idCnt * 3 + _localOffset;
                    int valAti = _trianglesList[i];
                    Vector3 v3 = _verteciesList[valAti];
                    VERT aVERT = new VERT(i, valAti, valAti, v3);
                    VERTarra[_localOffset] = aVERT;
                }
                TRI aTRI = new TRI(idCnt, VERTarra);
                aTRI._myNormal = _normals[idCnt * 3];
                _TRILIST.Add(aTRI);
            }
        }

        void Update_triangleList()
        {
            _trianglesList.Clear();
            _trianglesList = _TRILIST.SelectMany(e => e.my3Verts).Select(e => e._Tv).ToList();
        }

        void Print_TriINTS_From_TRRIILIST() {
            Debug.Log("tri from TRILIST");
            foreach (TRI tri in _TRILIST)
            {

                Debug.Log("["+tri._V0._Ti+","+ tri._V0._Tv+"]");
                Debug.Log("[" + tri._V1._Ti + "," + tri._V1._Tv + "]");
                Debug.Log("[" + tri._V2._Ti + "," + tri._V2._Tv + "]");

            }
        }

    

    }
}
