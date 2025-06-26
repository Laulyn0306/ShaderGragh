using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dissolve : MonoBehaviour
{
    public GameObject Root;
    public float DissolveTime = 3f;

    private MeshRenderer[] _renderers;
    // Start is called before the first frame update
    void Start()
    {
        _renderers = Root.GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StartCoroutine(StartDissolve());
        }
    }

    private IEnumerator StartDissolve()
    {
        SetDissolveRate(0);

        float time = 0f;
        while (time < DissolveTime)
        {
            time += Time.deltaTime;
            SetDissolveRate(time / DissolveTime);
            yield return null;
        }
    }

    private void SetDissolveRate(float value)
    {
        int shaderId = Shader.PropertyToID(name: "_ClipRate");
        foreach(MeshRenderer meshRenderer in _renderers)
        {
            foreach (Material material in meshRenderer.materials)
            {
                material.SetFloat(shaderId, value);
            }
        }
    }
}
