using UnityEngine;
using System.Collections;
using System;

namespace WorldSpaceTransitions
{
    public class PlaneHover : MonoBehaviour
    {

        public Color hovercolor;
        private Color original;

        private bool selected;
        private float _sc = 1;

        private Material m;
        private bool propertyExists = false;

      //  private static int i = 0;

        public Color emissionColor;
        // Use this for initialization
        void Start()
        {

            //Material m0 = GetComponent<Renderer>().sharedMaterial;
            //m = new Material(m0);
            //m.name = m0.name + i.ToString();
            m = GetComponent<Renderer>().material;
            m.EnableKeyword("_Emission");
            m.SetColor("_EmissionColor", Color.black);
            //GetComponent<Renderer>().material = m;
            original = m.color;
            propertyExists = m.HasProperty("_BaseColor");
            if (propertyExists) original = m.GetColor("_BaseColor");
        }

        public void SetColor(float sc)
        {
            //This is to make the corner highlighted with colour when it gets very small
            _sc = sc;
            float a = Mathf.Clamp01(-2.0f * sc + 1.25f);
            Color c2 = a * emissionColor + original;

            if (propertyExists)
            {
    
                m.SetColor("_BaseColor", c2);
            }
            m.SetColor("_EmissionColor", a * emissionColor);
            m.color = c2;
        }



        void OnMouseEnter()
        {
            m.color = hovercolor;
            if (m.HasProperty("_BaseColor")) m.SetColor("_BaseColor", hovercolor);
        }

        void OnMouseExit()
        {
            if (!selected) SetColor(_sc);
        }

        void SetOriginal()
        {

            m.color = original;
            if (m.HasProperty("_BaseColor"))m.SetColor("_BaseColor", original);
        }

        void Update()
        {

            if (selected && Input.GetMouseButtonUp(0))
            {
                SetOriginal();
                selected = false;
            }
        }

    }
}
