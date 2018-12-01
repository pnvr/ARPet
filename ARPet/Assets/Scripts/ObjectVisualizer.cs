﻿using HuaweiARUnitySDK;
using UnityEngine;

public class ObjectVisualizer : MonoBehaviour
{
    private ARAnchor anchor;
    private MeshRenderer meshRenderer;

    public void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(ARAnchor anchor)
    {
        this.anchor = anchor;
        Update();
    }

    private void Update()
    {
        if (null == anchor)
        {
            meshRenderer.enabled = false;
            return;
        }
        switch (anchor.GetTrackingState())
        {
            case ARTrackable.TrackingState.TRACKING:
                Pose p = anchor.GetPose();
                gameObject.transform.position = p.position;
                gameObject.transform.rotation = p.rotation;
                gameObject.transform.Rotate(0f, 225f, 0f, Space.Self);
                meshRenderer.enabled = true;
                break;
            case ARTrackable.TrackingState.PAUSED:
                meshRenderer.enabled = false;
                break;
            case ARTrackable.TrackingState.STOPPED:
            default:
                meshRenderer.enabled = false;
                Destroy(gameObject);
                break;
        }
    }
}