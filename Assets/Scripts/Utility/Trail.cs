﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom
/// </summary>
public class Trail : MonoBehaviour {
    
    [SerializeField]
    private float trailSpacing = 1;
    [SerializeField]
    private int scaleFactorMax = 3;

    private Vector3 positionSinceLastTrail;
    private int scaleFactor;

    private List<GameObject> activeTrailList = new List<GameObject>();

    private void Start() {
        TrailManager.instance.trailList.Add(this);

        scaleFactor = scaleFactorMax;

        SpawnTrail();
    }

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, positionSinceLastTrail) > trailSpacing) {
            SpawnTrail();
        }
    }

    private void SpawnTrail() {
        positionSinceLastTrail = transform.position;

        Transform trail = ObjectPoolManager.instance.SpawnPoolObject("Trail").transform;

        trail.localScale = new Vector3(1f / scaleFactor, 1f /scaleFactor, 1);
        trail.transform.position = transform.position;

        activeTrailList.Add(trail.gameObject);

        scaleFactor--;

        if (scaleFactor == 0) {
            scaleFactor = scaleFactorMax;
        }
    }

    public void DisableTrail() {
        foreach(GameObject go in activeTrailList) {
            go.SetActive(false);
        }
    }
}
