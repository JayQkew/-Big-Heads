using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SoftBody : MonoBehaviour
{
    [Range(3, 40)] public int numberOfNodes = 10;
    public float oldRadius = 3;
    public float radius = 3;
    public float nodeRadius = 0.001f;
    [SerializeField] private Transform nodeParent;

    [Header("Soft Body Qualities")] [Range(0, 1)]
    public float dampingRatio;
    [Range(0.1f, 10)] public float frequency;
    [SerializeField] private float radiusChangeSpeed;

    public List<GameObject> nodes = new List<GameObject>();
    public List<Rigidbody2D> nodes_rb = new List<Rigidbody2D>();
    private readonly List<SpringJoint2D> _springJoints = new List<SpringJoint2D>();
    public bool collectedDistance = false;
    private int frameCount = 0;
    public List<float> _springJointsStartDistance = new List<float>();

    private PolygonCollider2D _polygonCollider;
    private Mesh _mesh;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    public Material meshMaterial;
    private Vector3[] _verts;
    private Vector2[] _uvs;
    private int[] _tris;

    public List<SpringJoint2D> attachJoints = new List<SpringJoint2D>();
    public float attachFreq;
    public float attachDamp;

    public float nodeWeight;

    private void Start()
    {
        oldRadius = radius;
        CreateNodes();
        ArrangeNodes();
        ConnectNodes();
        CreateMesh();
    }

    private void Update()
    {
        ApplySoftBodyQualities();
        UpdateMesh();
        UpdatePosition();
        UpdateRadius(radius);
        
        if (!collectedDistance && frameCount == 2)
        {
            GetSpringDistances();
            collectedDistance = true;
        }
        if(frameCount < 2) frameCount++;
    }

    private void FixedUpdate()
    {
        UpdateCollider();
    }

    private void OnEnable()
    {
        nodeParent.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        nodeParent.gameObject.SetActive(false);
    }

    private void UpdatePosition()
    {
        Vector3 centerPos = Vector3.zero;
        foreach (GameObject n in nodes)
        {
            centerPos += n.transform.position;
        }

        transform.position = centerPos / nodes.Count;
    }

    private void UpdateRadius(float newRadius)
    {
        if (newRadius != oldRadius)
        {
            float radiusFactor = Mathf.Lerp(oldRadius, newRadius, Time.deltaTime * radiusChangeSpeed);

            for (int i = 0; i < _springJoints.Count; i++)
            {
                _springJoints[i].distance = _springJointsStartDistance[i] * (1 + radiusFactor);
            }

            oldRadius = radiusFactor; // Update oldRadius for consistency
        }
    }
    
    private void CreateNodes()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            GameObject node = new GameObject("SoftBodyNode");
            node.tag = "SoftBodyNode";

            Rigidbody2D rb = node.AddComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.gravityScale = nodeWeight;
            nodes_rb.Add(rb);

            CircleCollider2D cc = node.AddComponent<CircleCollider2D>();
            cc.radius = nodeRadius;

            //exclude this polygon collider (flip the colliders between the two slimes)
            int excludeLayer = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
            cc.excludeLayers = excludeLayer;
            node.layer = LayerMask.LayerToName(gameObject.layer) == "SoftBody1"
                ? LayerMask.NameToLayer("SoftBody2")
                : LayerMask.NameToLayer("SoftBody1");

            node.transform.SetParent(nodeParent == null ? transform : nodeParent);

            SpringJoint2D attachJoint = node.AddComponent<SpringJoint2D>();
            attachJoint.frequency = attachFreq;
            attachJoint.dampingRatio = attachDamp;
            attachJoints.Add(attachJoint);

            nodes.Add(node);
        }
    }

    private void ArrangeNodes()
    {
        float equalAngles = 360 / numberOfNodes;
        float startAngle = 90;

        foreach (var n in nodes)
        {
            float x = Mathf.Cos(startAngle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(startAngle * Mathf.Deg2Rad) * radius;
            n.transform.localPosition = new Vector3(x, y, 0);

            startAngle += equalAngles;
        }
    }

    private void ConnectNodes()
    {
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            for (int j = i + 1; j < nodes.Count; j++)
            {
                SpringJoint2D sprintJoint = nodes[i].AddComponent<SpringJoint2D>();
                sprintJoint.enableCollision = true;
                sprintJoint.connectedBody = nodes[j].GetComponent<Rigidbody2D>();
                sprintJoint.frequency = frequency;
                
                _springJoints.Add(sprintJoint);
            }
        }
    }

    private void GetSpringDistances()
    {
        for (int i = 0; i < _springJoints.Count; i++)
        {
            _springJointsStartDistance.Add(_springJoints[i].distance);
        }
    }

    private void ApplySoftBodyQualities()
    {
        foreach (var springJoint in _springJoints)
        {
            springJoint.dampingRatio = dampingRatio;
            springJoint.frequency = frequency;
        }
    }

    private void CreateMesh()
    {
        _mesh = new Mesh();
        _mesh.name = "SoftBodyMesh";

        _polygonCollider = gameObject.AddComponent<PolygonCollider2D>();

        if (nodeParent == null)
        {
            _meshFilter = gameObject.AddComponent<MeshFilter>();
            _meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
        else
        {
            _meshFilter = nodeParent.gameObject.AddComponent<MeshFilter>();
            _meshRenderer = nodeParent.GetComponent<MeshRenderer>();
        }

        _verts = new Vector3[numberOfNodes];
        _uvs = new Vector2[numberOfNodes];
        _tris = new int[(numberOfNodes - 2) * 3];
        _polygonCollider.points = new Vector2[_verts.Length];
        Vector2[] colliderPoints = new Vector2[_verts.Length];

        // Set vertices and UVs
        for (int n = 0; n < numberOfNodes; n++)
        {
            _verts[n] = nodes[n].transform.localPosition;
            _uvs[n] = new Vector2(_verts[n].x / radius, _verts[n].y / radius);
            colliderPoints[n] = nodes[n].transform.position;
        }

        // Set triangles
        for (int i = 0; i < numberOfNodes - 2; i++)
        {
            _tris[i * 3] = 0;
            _tris[i * 3 + 1] = i + 1;
            _tris[i * 3 + 2] = i + 2;
        }

        _mesh.vertices = _verts;
        _mesh.uv = _uvs;
        _mesh.triangles = _tris;
        _mesh.RecalculateNormals();

        _meshRenderer.material = meshMaterial;
        _meshFilter.mesh = _mesh;
        _polygonCollider.SetPath(0, colliderPoints);
    }

    private void UpdateMesh()
    {
        Vector2[] colliderPoints = new Vector2[_verts.Length];
        // Update vertices
        for (int n = 0; n < numberOfNodes; n++)
        {
            _verts[n] = nodes[n].transform.localPosition;
        }

        _mesh.vertices = _verts;
        _mesh.RecalculateNormals();
    }

    private void UpdateCollider()
    {
        Vector2[] colliderPoints = new Vector2[_verts.Length];
        for (int n = 0; n < numberOfNodes; n++)
        {
            colliderPoints[n] = transform.InverseTransformPoint(nodes[n].transform.position);
        }

        _polygonCollider.SetPath(0, colliderPoints);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        foreach (Rigidbody2D rb in nodes_rb)
        {
            rb.AddForce(force, mode);
        }
    }

    public void MoveSlime(Vector2 newPosition)
    {
        //find difference from center of slime
        //store those differences
        Vector2[] dif = new Vector2[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
        {
            dif[i] = nodes[i].transform.position - transform.position;
            
            nodes[i].transform.position = newPosition + dif[i];
        }
        
        //find new position
        //apply differences to the new postion
    }

    public void Damage(int dmg)
    {
        transform.GetComponentInParent<old_Head>().Damage(dmg);
            
    }
}
