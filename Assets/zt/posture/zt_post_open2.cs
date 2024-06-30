using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zt_post_open2 : MonoBehaviour
{
    zt_post_open1 open1;
    public GameObject post;
    void Start()
    {
        open1 = FindObjectOfType<zt_post_open1>();

        post.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (open1.lister_post == 1)
        {
            if (open1.lister_post1 == 0)
            {
                post.SetActive(true);
            }

        }
        if (open1.lister_post1 == 1)
        {
            post.SetActive(false);
        }
    }
}
