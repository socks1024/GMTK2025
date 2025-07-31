using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingBackground : MonoBehaviour
{
    [Serializable]
    public struct RollingLayer
    {
        public GameObject Layer;
        [HideInInspector] public Image LayerImage;
        [HideInInspector] public Transform LayerTransform;
        public float Speed;
        [HideInInspector] public Transform subLayerTransform;
    }

    public List<RollingLayer> layers = new();

    public float TotalSpeed;

    public Transform StartPoint;

    public Transform EndPoint;

    void Start()
    {
        for (int i = 0; i < layers.Count; i++) 
        {
            RollingLayer currLayer = new();

            currLayer.Layer = layers[i].Layer;
            currLayer.Speed = layers[i].Speed;
            currLayer.LayerImage = currLayer.Layer.GetComponent<Image>();
            currLayer.LayerTransform = currLayer.Layer.transform;

            currLayer.subLayerTransform = Instantiate(currLayer.Layer, currLayer.LayerTransform.parent).transform;
            currLayer.subLayerTransform.position = (EndPoint.position + StartPoint.position)/2;
            currLayer.subLayerTransform.SetSiblingIndex(currLayer.LayerTransform.GetSiblingIndex() + 1);

            layers[i] = currLayer;
        }
    }

    void Update()
    {
        for(int i = 0; i < layers.Count; i++)
        {
            layers[i].LayerTransform.position += Vector3.down * layers[i].Speed * TotalSpeed;

            layers[i].subLayerTransform.position += Vector3.down * layers[i].Speed * TotalSpeed;

            if (layers[i].LayerTransform.position.y < EndPoint.position.y)
            {
                layers[i].LayerTransform.position = StartPoint.position;
            }

            if (layers[i].subLayerTransform.position.y < EndPoint.position.y)
            {
                layers[i].subLayerTransform.position = StartPoint.position;
            }
        }
    }
}

