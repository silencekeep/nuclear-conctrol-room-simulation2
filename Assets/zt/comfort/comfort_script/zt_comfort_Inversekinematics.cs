using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class zt_comfort_Inversekinematics : MonoBehaviour
    {
   
        private Animator animator;
        
        
        bool isIK = true;
        public bool ishand = true;
        
      
        // Use this for initialization
        //public Transform   rightHandObj;
        void Start()
        {
           
            animator = GetComponent<Animator>();
           // rightHandObj=GameObject.Find("")


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


        animator.SetLookAtWeight(0.5f, 0.5f, 0.5f, 0.5f, 0.5f);
        animator.SetLookAtPosition(GameObject.Find("target").GetComponent<Transform>().position);
        AvatarIKGoal g = ishand ? AvatarIKGoal.RightHand : AvatarIKGoal.RightFoot;
        AvatarIKHint h = ishand ? AvatarIKHint.RightElbow : AvatarIKHint.RightKnee;
        animator.SetIKPositionWeight(g, 1f);
        animator.SetIKPosition(g, GameObject.Find("target").GetComponent<Transform>().position);
        animator.SetIKRotationWeight(g, 1f);
        animator.SetIKRotation(g, GameObject.Find("target").GetComponent<Transform>().rotation);

        animator.SetIKHintPositionWeight(h, 1f);
        animator.SetIKHintPosition(h, GameObject.Find("swat/swat:Hips").GetComponent<Transform>().position);


    }
                    
            

    }

