using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeryAnimation
{
    public class Human : MonoBehaviour
    {
        public GameObject door;

        private Vector3 savePosition;
        private Quaternion saveRotation;

        private void Awake()
        {
            savePosition = transform.localPosition;
            saveRotation = transform.localRotation;
        }

        public void Restart()
        {
            var animator = GetComponent<Animator>();
            if (animator == null) return;

            transform.localPosition = savePosition;
            transform.localRotation = saveRotation;
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        public void OpenDoor()
        {
            if (door == null) return;
            var animator = door.GetComponent<Animator>();
            if (animator == null) return;
            animator.SetTrigger("Open");
        }
    }
}