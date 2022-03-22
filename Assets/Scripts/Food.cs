using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);

        transform.position = new Vector3(Mathf.Round(x), 0.5f, Mathf.Round(z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Duck>(out Duck snake) && GameManager.Instance.gameActive)
            RandomizePosition();
    }
}
