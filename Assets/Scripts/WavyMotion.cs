﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyMotion : MonoBehaviour
{
    public float period = 1;
    public float amplitude = 0.5f;
    public float timeOffset = 0;

    Vector3 basePosition;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float wave = Mathf.Sin((Time.time + timeOffset) * 2 * Mathf.PI / period) * amplitude;
        transform.localPosition = basePosition + Vector3.up * wave;
    }
}