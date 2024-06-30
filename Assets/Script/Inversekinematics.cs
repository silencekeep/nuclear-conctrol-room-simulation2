using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HullDelaunayVoronoi.Hull;
using HullDelaunayVoronoi.Primitives;
namespace HullDelaunayVoronoi
{ 
    public class Inversekinematics : MonoBehaviour
    {
        click click1;
        private Animator animator;
        
        
        bool isIK = true;
        public bool ishand = true;
        
        dropdown1 drop;
        input input1;
        // Use this for initialization

        void Start()
        {
            click1 = FindObjectOfType<click>();
            input1 = FindObjectOfType<input>();
            drop = FindObjectOfType<dropdown1>();
            animator = GetComponent<Animator>();
           
            
        }
       
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                isIK = false;

            }


        }


        private void OnAnimatorIK(int layerIndex)
        {
            if (click1.lister == 1)
            {
                if (isIK)
                {
                    if (drop.ValueChange)
                    {
                        animator.SetLookAtWeight(0.5f, 0.5f, 0.5f, 0.5f, 0.5f);
                        animator.SetLookAtPosition(GameObject.Find(input1.endValue1).GetComponent<Transform>().position);
                        AvatarIKGoal g = ishand ? AvatarIKGoal.RightHand : AvatarIKGoal.RightFoot;
                        AvatarIKHint h = ishand ? AvatarIKHint.RightElbow : AvatarIKHint.RightKnee;
                        animator.SetIKPositionWeight(g, 1f);
                        animator.SetIKPosition(g, GameObject.Find(input1.endValue1).GetComponent<Transform>().position);
                        animator.SetIKRotationWeight(g, 1f);
                        animator.SetIKRotation(g, GameObject.Find(input1.endValue1).GetComponent<Transform>().rotation);

                        animator.SetIKHintPositionWeight(h, 1f);
                        animator.SetIKHintPosition(h, GameObject.Find("swat/swat:Hips").GetComponent<Transform>().position);


                    }
                    else
                    {
                        animator.SetLookAtWeight(0.5f, 0.5f, 0.5f, 0.5f, 0.5f);
                        animator.SetLookAtPosition(GameObject.Find(input1.endValue1).GetComponent<Transform>().position);
                        AvatarIKGoal g = ishand ? AvatarIKGoal.LeftHand : AvatarIKGoal.LeftFoot;
                        AvatarIKHint h = ishand ? AvatarIKHint.LeftElbow : AvatarIKHint.LeftKnee;
                        animator.SetIKPositionWeight(g, 1f);
                        animator.SetIKPosition(g, GameObject.Find(input1.endValue1).GetComponent<Transform>().position);
                        animator.SetIKRotationWeight(g, 1f);
                        animator.SetIKRotation(g, GameObject.Find(input1.endValue1).GetComponent<Transform>().rotation);

                        animator.SetIKHintPositionWeight(h, 1f);
                        animator.SetIKHintPosition(h, GameObject.Find("swat/swat:Hips").GetComponent<Transform>().position);
                    }
                }
               
                
            }

        }

    }
}
