using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class zt_space_text : MonoBehaviour
{
    zt_space_leftupperarm leftupperarm;
    zt_space_rightupperarm rightupperarm;
    zt_space_rightForearm rightForearm;
    zt_space_leftForearm leftForearm;

    zt_space_2leftupperarm leftupperarm2;
    zt_space_2rightupperarm rightupperarm2;
    zt_space_2rightForearm rightForearm2;
    zt_space_2leftForearm leftForearm2;

    zt_space_3leftupperarm leftupperarm3;
    zt_space_3rightupperarm rightupperarm3;
    zt_space_3rightForearm rightForearm3;
    zt_space_3leftForearm leftForearm3;

    public List<string> left_upperarm2 = new List<string>();
    public List<string> right_upperarm2 = new List<string>();
    public List<string> right_Forearm2 = new List<string>();
    public List<string> left_Forearm2 = new List<string>();
    zt_space_dropdown dropdown1;

    public string left_upperarm3;
    public string right_upperarm3;
    public string right_Forearm3;
    public string left_Forearm3;

    zt_space_click click1;
    zt_space_open1 open;

    void Start()
    {
        leftupperarm = FindObjectOfType<zt_space_leftupperarm>();
        rightupperarm = FindObjectOfType<zt_space_rightupperarm>();
        rightForearm = FindObjectOfType<zt_space_rightForearm>();
        leftForearm = FindObjectOfType<zt_space_leftForearm>();
        open = FindObjectOfType<zt_space_open1>();

        leftupperarm2 = FindObjectOfType<zt_space_2leftupperarm>();
        rightupperarm2 = FindObjectOfType<zt_space_2rightupperarm>();
        rightForearm2 = FindObjectOfType<zt_space_2rightForearm>();
        leftForearm2 = FindObjectOfType<zt_space_2leftForearm>();

        leftupperarm3 = FindObjectOfType<zt_space_3leftupperarm>();
        rightupperarm3 = FindObjectOfType<zt_space_3rightupperarm>();
        rightForearm3 = FindObjectOfType<zt_space_3rightForearm>();
        leftForearm3 = FindObjectOfType<zt_space_3leftForearm>();

        click1 = FindObjectOfType<zt_space_click>();
        dropdown1 = FindObjectOfType<zt_space_dropdown>();

    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_space1 == 1)
        {
            click1.lister = 0;
        }
        if (click1.lister == 1)
        {
            
            if (dropdown1.a == 2)
            {
                foreach (string eachString in leftupperarm.left_upperarm1)

                {

                    if (!left_upperarm2.Contains(eachString))
                    {
                        left_upperarm2.Add(eachString);
                    }

                        

                }

                foreach (string eachString in rightupperarm.right_upperarm1)

                {

                    if (!right_upperarm2.Contains(eachString))
                    {
                        right_upperarm2.Add(eachString);
                    }

                       

                }

                foreach (string eachString in rightForearm.right_Forearm1)

                {

                    if (!right_Forearm2.Contains(eachString))
                    {
                        right_Forearm2.Add(eachString);
                    }

                       

                }
                foreach (string eachString in leftForearm.left_Forearm1)

                {

                    if (!left_Forearm2.Contains(eachString))
                    {
                        left_Forearm2.Add(eachString);
                    }

                        

                }

                left_upperarm3 = string.Join(" ", left_upperarm2.ToArray());
                right_upperarm3 = string.Join(" ", right_upperarm2.ToArray());
                right_Forearm3 = string.Join(" ", right_Forearm2.ToArray());
                left_Forearm3 = string.Join(" ", left_Forearm2.ToArray());

                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h2/space10").GetComponent<Text>().text = left_upperarm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h4/space12").GetComponent<Text>().text = right_upperarm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h5/space13").GetComponent<Text>().text = right_Forearm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h3/space11").GetComponent<Text>().text = left_Forearm3.ToString();
            }

            if (dropdown1.a == 3)
            {
                foreach (string eachString in leftupperarm2.left_upperarm1)

                {

                    if (!left_upperarm2.Contains(eachString))
                    {
                        left_upperarm2.Add(eachString);
                    }

                        

                }

                foreach (string eachString in rightupperarm2.right_upperarm1)

                {

                    if (!right_upperarm2.Contains(eachString))
                    {
                        right_upperarm2.Add(eachString);
                    }

                       

                }

                foreach (string eachString in rightForearm2.right_Forearm1)

                {

                    if (!right_Forearm2.Contains(eachString))
                    {
                        right_Forearm2.Add(eachString);
                    }

                        

                }
                foreach (string eachString in leftForearm2.left_Forearm1)

                {

                    if (!left_Forearm2.Contains(eachString))
                    {
                        left_Forearm2.Add(eachString);
                    }

                        

                }

                left_upperarm3 = string.Join(" ", left_upperarm2.ToArray());
                right_upperarm3 = string.Join(" ", right_upperarm2.ToArray());
                right_Forearm3 = string.Join(" ", right_Forearm2.ToArray());
                left_Forearm3 = string.Join(" ", left_Forearm2.ToArray());

                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h2/space10").GetComponent<Text>().text = left_upperarm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h4/space12").GetComponent<Text>().text = right_upperarm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h5/space13").GetComponent<Text>().text = right_Forearm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h3/space11").GetComponent<Text>().text = left_Forearm3.ToString();
            }

            if (dropdown1.a == 4)
            {
                foreach (string eachString in leftupperarm3.left_upperarm1)

                {

                    if (!left_upperarm2.Contains(eachString))
                    {
                        left_upperarm2.Add(eachString);
                    }

                        

                }

                foreach (string eachString in rightupperarm3.right_upperarm1)

                {

                    if (!right_upperarm2.Contains(eachString))
                    {
                        right_upperarm2.Add(eachString);
                    }

                       

                }

                foreach (string eachString in rightForearm3.right_Forearm1)

                {

                    if (!right_Forearm2.Contains(eachString))
                    {
                        right_Forearm2.Add(eachString);
                    }

                        

                }
                foreach (string eachString in leftForearm3.left_Forearm1)

                {

                    if (!left_Forearm2.Contains(eachString))
                    {
                        left_Forearm2.Add(eachString);
                    }

                       

                }

                left_upperarm3 = string.Join(" ", left_upperarm2.ToArray());
                right_upperarm3 = string.Join(" ", right_upperarm2.ToArray());
                right_Forearm3 = string.Join(" ", right_Forearm2.ToArray());
                left_Forearm3 = string.Join(" ", left_Forearm2.ToArray());

                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h2/space10").GetComponent<Text>().text = left_upperarm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h4/space12").GetComponent<Text>().text = right_upperarm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h5/space13").GetComponent<Text>().text = right_Forearm3.ToString();
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h3/space11").GetComponent<Text>().text = left_Forearm3.ToString();
            }
        }
    }
}
