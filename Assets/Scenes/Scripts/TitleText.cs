using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleText : MonoBehaviour
{
    TMP_Text textMesh;
    Mesh mesh;
    Vector3[] verticies;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        verticies = mesh.vertices;

        for (int i = 0; i <verticies.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            verticies[i] = verticies[i] + offset;
        }

        mesh.vertices = verticies;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time){
        return new Vector2(Mathf.Sin(time*3.3f), Mathf.Cos(time*2.5f));
    }
}
