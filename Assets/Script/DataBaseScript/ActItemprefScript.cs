using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActItemprefScript : MonoBehaviour
{
    public static string curtext;
    public Image image;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnClick()
    {        
        curtext = text.text;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
