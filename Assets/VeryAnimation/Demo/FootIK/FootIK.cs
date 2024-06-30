using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VeryAnimation
{
    public class FootIK : MonoBehaviour
    {
        public Text text;

        private void Update()
        {
            if (text == null) return;
            var animator = GetComponent<Animator>();
            if (animator == null) return;

            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.shortNameHash == Animator.StringToHash("FootIK Enable"))
                text.text = "Foot IK Enable";
            else if (stateInfo.shortNameHash == Animator.StringToHash("FootIK Disable"))
                text.text = "Foot IK Disable";
        }
    }
}
