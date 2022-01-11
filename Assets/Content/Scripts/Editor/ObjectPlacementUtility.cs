using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AI;
using UnityEngine.AI;

[CustomEditor(typeof(NavMeshSurface))]
public class ObjectPlacementUtility : NavMeshSurfaceEditor
{
    private bool m_PlaceTrees = false;
    private NavMeshSurface meshSurface;

    public override void OnEnable()
    {
        base.OnEnable();

        m_PlaceTrees = false;

        meshSurface = (NavMeshSurface)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Object Placement Properties for Surface", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        if (GUILayout.Button(m_PlaceTrees ? "Stop Placing Trees" : "Start Placing Trees"))
        {
            m_PlaceTrees = !m_PlaceTrees;
        }

       
        

    }

    public void OnSceneGUI()
    {
        if (m_PlaceTrees)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            PlaceTrees();
        }
      
    }

    private void PlaceTrees()
    {
        if (Event.current.type == EventType.MouseUp)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (GameObject.ReferenceEquals(meshSurface.gameObject, hit.transform.gameObject))
                {
                    Debug.Log("Place Tree!!");
                    CreateTree(hit.point);
                }
            }
        }
    }

    private void CreateTree(Vector3 pos)
    {
        GameObject resTree = Resources.Load<GameObject>("Tree");

        Bounds b = resTree.GetComponent<MeshRenderer>().bounds;


        GameObject _tree = Instantiate<GameObject>(Resources.Load<GameObject>("Tree"), pos + new Vector3(0, Random.Range(0, 0.5f), 0), Quaternion.Euler(0, Random.Range(0, 360), 0));
        _tree.transform.localScale = Vector3.one * Random.Range(0.9f, 1);
    }
}
