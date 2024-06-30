using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CNCC.Panels
{
    public class Chair : Panel
    {
        public static List<Chair> ALLChairs = new List<Chair>();
        public Transform Sitransform;
        GameObject generatedChair;
        float objectHeight = 0f;
        public Chair(GameObject ChairPrefab, Transform spawnPoint, Transform position,string id)
        {
            generatedChair = Instantiate(ChairPrefab, spawnPoint);
            generatedChair.gameObject.GetComponent<Chair>().ID = SetID(id);
            generatedChair.gameObject.transform.position = position.position;
            ALLChairs.Add(generatedChair.GetComponent<Chair>());
            Panel.AllPanels.Add(generatedChair.GetComponent<Chair>());
        }
        string SetID(string id)
        {
            if (id.Trim() == "")
            {
                id = "Chair" + ALLChairs.Count;
            }
            return id;
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public override void DeletePanel(Panel panel)
        {
            base.DeletePanel(panel);
            ALLChairs.Remove((Chair)panel);
        }

        public override float Hcompensation(float height)
        {
            float heightcompensation = objectHeight * transform.localScale.y / 2;
            return heightcompensation;
        }
    }
}