using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.UITools.Outline
{
    [RequireComponent(typeof(Image))]
    public class OutlineBehaviour : MonoBehaviour
    {
        private float m_OutlineWidth = 0;
        public float OutlineWidth
        {
            get { return m_OutlineWidth; }
            set
            {
                m_OutlineWidth = value;
                if (m_OutlineWidth < 0) m_OutlineWidth = 0;
                m_OutlineMaterial.SetFloat("_OutlineWidth", m_OutlineWidth);
            }
        }

        private Color m_OutlineColor = Color.white;
        public Color OutlineColor
        {
            get { return m_OutlineColor; }
            set
            {
                m_OutlineColor = value;
                m_OutlineMaterial.SetColor("_OutlineColor", m_OutlineColor);
            }
        }

        private Material m_OutlineMaterial = null;

        void Awake()
        {
            m_OutlineMaterial = GetComponent<Image>().material;
            if (m_OutlineMaterial is null) print("Failed to get material");
        }

        // void Update()
        // {
        //     OutlineWidth = 5 * Mathf.Abs(Mathf.Sin(Time.time));
        // }
    }
}