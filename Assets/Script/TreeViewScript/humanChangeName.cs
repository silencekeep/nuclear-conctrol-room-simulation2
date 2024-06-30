using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class humanChangeName : MonoBehaviour
{
    public GameObject humanTree11, humanTree12, humanTree13, humanTree21,
        humanTree22, humanTree23, humanTree31, humanTree32, humanTree33;
    public Dropdown cityDropdown,viewDropdown,humanDropdown, visibilityDropdown, accessibilityDropdown,spaceDropdown,comfortDropdown,catchDropdown;
    public InputField cityInput,viewInput,humanInput, visibilityInput, accessibilityInput, spaceInput, comfortInput,catchInput;
    public Text humanText1, humanText2, humanText3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //流程输入框
    public void ClearContent()
    {
        //oldContent = cityDropdown.options[cityDropdown.value].text;
        //cityDropdown.options[cityDropdown.value].text = "";
        if (cityInput.text != "")
        {
            cityDropdown.options[cityDropdown.value].text = cityInput.text;
            if (cityDropdown.value == 0)
            {
                humanTree11.name = cityInput.text;
                humanTree21.name = cityInput.text;
                humanTree31.name = cityInput.text;
                humanText1.text = cityInput.text;
                viewDropdown.options[0].text = cityInput.text;
                humanDropdown.options[0].text = cityInput.text;
                visibilityDropdown.options[0].text = cityInput.text;
                accessibilityDropdown.options[0].text = cityInput.text;
                spaceDropdown.options[0].text = cityInput.text;
                comfortDropdown.options[0].text = cityInput.text;
                catchDropdown.options[0].text = cityInput.text;
            }
            else
            {
                if (cityDropdown.value == 1)
                {
                    humanTree12.name = cityInput.text;
                    humanTree22.name = cityInput.text;
                    humanTree32.name = cityInput.text;
                    humanText2.text = cityInput.text;
                    viewDropdown.options[1].text = cityInput.text;
                    humanDropdown.options[1].text = cityInput.text;
                    visibilityDropdown.options[1].text = cityInput.text;
                    accessibilityDropdown.options[1].text = cityInput.text;
                    spaceDropdown.options[1].text = cityInput.text;
                    comfortDropdown.options[1].text = cityInput.text;
                    catchDropdown.options[1].text = cityInput.text;
                }
                else
                {
                    humanTree13.name = cityInput.text;
                    humanTree23.name = cityInput.text;
                    humanTree33.name = cityInput.text;
                    humanText3.text = cityInput.text;
                    viewDropdown.options[2].text = cityInput.text;
                    humanDropdown.options[2].text = cityInput.text;
                    visibilityDropdown.options[2].text = cityInput.text;
                    accessibilityDropdown.options[2].text = cityInput.text;
                    spaceDropdown.options[2].text = cityInput.text;
                    comfortDropdown.options[2].text = cityInput.text;
                    catchDropdown.options[2].text = cityInput.text;
                }
            }

            viewDropdown.captionText.text = viewDropdown.options[viewDropdown.value].text;
            humanDropdown.captionText.text = humanDropdown.options[humanDropdown.value].text;
            visibilityDropdown.captionText.text = visibilityDropdown.options[visibilityDropdown.value].text;
            accessibilityDropdown.captionText.text = accessibilityDropdown.options[accessibilityDropdown.value].text;
            spaceDropdown.captionText.text = spaceDropdown.options[spaceDropdown.value].text;
            comfortDropdown.captionText.text = comfortDropdown.options[comfortDropdown.value].text;
            catchDropdown.captionText.text = catchDropdown.options[catchDropdown.value].text;
        }
        cityInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void onSelectChanged()
    {
        cityInput.text = "";
        cityInput.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    //视角输入框
    public void ViewInputChange()
    {
       if (viewInput.text != "")
        {
            viewDropdown.options[viewDropdown.value].text = viewInput.text;
            if (viewDropdown.value == 0)
            {
                humanTree11.name = viewInput.text;
                humanTree21.name = viewInput.text;
                humanTree31.name = viewInput.text;
                humanText1.text = viewInput.text;
                cityDropdown.options[0].text = viewInput.text;
                humanDropdown.options[0].text = viewInput.text;
                visibilityDropdown.options[0].text = viewInput.text;
                accessibilityDropdown.options[0].text = viewInput.text;
                spaceDropdown.options[0].text = viewInput.text;
                comfortDropdown.options[0].text = viewInput.text;
                catchDropdown.options[0].text = cityInput.text;
            }
            else
            {
                if (viewDropdown.value == 1)
                {
                    humanTree12.name = viewInput.text;
                    humanTree22.name = viewInput.text;
                    humanTree32.name = viewInput.text;
                    humanText2.text = viewInput.text;
                    cityDropdown.options[1].text = viewInput.text;
                    humanDropdown.options[1].text = viewInput.text;
                    visibilityDropdown.options[1].text = viewInput.text;
                    accessibilityDropdown.options[1].text = viewInput.text;
                    spaceDropdown.options[1].text = viewInput.text;
                    comfortDropdown.options[1].text = viewInput.text;
                    catchDropdown.options[1].text = cityInput.text;
                }
                else
                {
                    humanTree13.name = viewInput.text;
                    humanTree23.name = viewInput.text;
                    humanTree33.name = viewInput.text;
                    humanText3.text = viewInput.text;
                    cityDropdown.options[2].text = viewInput.text;
                    humanDropdown.options[2].text = viewInput.text;
                    visibilityDropdown.options[2].text = viewInput.text;
                    accessibilityDropdown.options[2].text = viewInput.text;
                    spaceDropdown.options[2].text = viewInput.text;
                    comfortDropdown.options[2].text = viewInput.text;
                    catchDropdown.options[2].text = cityInput.text;
                }
            }
            cityDropdown.captionText.text = cityDropdown.options[cityDropdown.value].text;
            humanDropdown.captionText.text = humanDropdown.options[humanDropdown.value].text;
            visibilityDropdown.captionText.text = visibilityDropdown.options[visibilityDropdown.value].text;
            accessibilityDropdown.captionText.text = accessibilityDropdown.options[accessibilityDropdown.value].text;
            spaceDropdown.captionText.text = spaceDropdown.options[spaceDropdown.value].text;
            comfortDropdown.captionText.text = comfortDropdown.options[comfortDropdown.value].text;
            catchDropdown.captionText.text = catchDropdown.options[catchDropdown.value].text;
        }
        viewInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    //数字人输入框
    public void HumanInputChange()
    {
        if (humanInput.text != "")
        {
            humanDropdown.options[humanDropdown.value].text = humanInput.text;
            if (humanDropdown.value == 0)
            {
                humanTree11.name = humanInput.text;
                humanTree21.name = humanInput.text;
                humanTree31.name = humanInput.text;
                humanText1.text = humanInput.text;
                cityDropdown.options[0].text = humanInput.text;
                viewDropdown.options[0].text = humanInput.text;
                visibilityDropdown.options[0].text = humanInput.text;
                accessibilityDropdown.options[0].text = humanInput.text;
                spaceDropdown.options[0].text = humanInput.text;
                comfortDropdown.options[0].text = humanInput.text;
                catchDropdown.options[0].text = cityInput.text;
            }
            else
            {
                if (humanDropdown.value == 1)
                {
                    humanTree12.name = humanInput.text;
                    humanTree22.name = humanInput.text;
                    humanTree32.name = humanInput.text;
                    humanText2.text = humanInput.text;
                    cityDropdown.options[1].text = humanInput.text;
                    viewDropdown.options[1].text = humanInput.text;
                    visibilityDropdown.options[1].text = humanInput.text;
                    accessibilityDropdown.options[1].text = humanInput.text;
                    spaceDropdown.options[1].text = humanInput.text;
                    comfortDropdown.options[1].text = humanInput.text;
                    catchDropdown.options[1].text = cityInput.text;
                }
                else
                {
                    humanTree13.name = humanInput.text;
                    humanTree23.name = humanInput.text;
                    humanTree33.name = humanInput.text;
                    humanText3.text = humanInput.text;
                    cityDropdown.options[2].text = humanInput.text;
                    viewDropdown.options[2].text = humanInput.text;
                    visibilityDropdown.options[2].text = humanInput.text;
                    accessibilityDropdown.options[2].text = humanInput.text;
                    spaceDropdown.options[2].text = humanInput.text;
                    comfortDropdown.options[2].text = humanInput.text;
                    catchDropdown.options[2].text = cityInput.text;
                }
            }
            cityDropdown.captionText.text = cityDropdown.options[cityDropdown.value].text;
            viewDropdown.captionText.text = viewDropdown.options[viewDropdown.value].text;
            visibilityDropdown.captionText.text = visibilityDropdown.options[visibilityDropdown.value].text;
            accessibilityDropdown.captionText.text = accessibilityDropdown.options[accessibilityDropdown.value].text;
            spaceDropdown.captionText.text = spaceDropdown.options[spaceDropdown.value].text;
            comfortDropdown.captionText.text = comfortDropdown.options[comfortDropdown.value].text;
            catchDropdown.captionText.text = catchDropdown.options[catchDropdown.value].text;
        }
        humanInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    //可视性输入框
    public void VisibilityInputChange()
    {
        if (visibilityInput.text != "")
        {
            visibilityDropdown.options[visibilityDropdown.value].text = visibilityInput.text;
            if (visibilityDropdown.value == 0)
            {
                humanTree11.name = visibilityInput.text;
                humanTree21.name = visibilityInput.text;
                humanTree31.name = visibilityInput.text;
                humanText1.text = visibilityInput.text;
                cityDropdown.options[0].text = visibilityInput.text;
                viewDropdown.options[0].text = visibilityInput.text;
                humanDropdown.options[0].text = visibilityInput.text;
                accessibilityDropdown.options[0].text = visibilityInput.text;
                spaceDropdown.options[0].text = visibilityInput.text;
                comfortDropdown.options[0].text = visibilityInput.text;
                catchDropdown.options[0].text = cityInput.text;
            }
            else
            {
                if (visibilityDropdown.value == 1)
                {
                    humanTree12.name = visibilityInput.text;
                    humanTree22.name = visibilityInput.text;
                    humanTree32.name = visibilityInput.text;
                    humanText2.text = visibilityInput.text;
                    cityDropdown.options[1].text = visibilityInput.text;
                    viewDropdown.options[1].text = visibilityInput.text;
                    humanDropdown.options[1].text = visibilityInput.text;
                    accessibilityDropdown.options[1].text = visibilityInput.text;
                    spaceDropdown.options[1].text = visibilityInput.text;
                    comfortDropdown.options[1].text = visibilityInput.text;
                    catchDropdown.options[1].text = cityInput.text;
                }
                else
                {
                    humanTree13.name = visibilityInput.text;
                    humanTree23.name = visibilityInput.text;
                    humanTree33.name = visibilityInput.text;
                    humanText3.text = visibilityInput.text;
                    cityDropdown.options[2].text = visibilityInput.text;
                    viewDropdown.options[2].text = visibilityInput.text;
                    humanDropdown.options[2].text = visibilityInput.text;
                    accessibilityDropdown.options[2].text = visibilityInput.text;
                    spaceDropdown.options[2].text = visibilityInput.text;
                    comfortDropdown.options[2].text = visibilityInput.text;
                    catchDropdown.options[2].text = cityInput.text;
                }
            }
            cityDropdown.captionText.text = cityDropdown.options[cityDropdown.value].text;
            viewDropdown.captionText.text = viewDropdown.options[viewDropdown.value].text;
            humanDropdown.captionText.text = humanDropdown.options[humanDropdown.value].text;
            accessibilityDropdown.captionText.text = accessibilityDropdown.options[accessibilityDropdown.value].text;
            spaceDropdown.captionText.text = spaceDropdown.options[spaceDropdown.value].text;
            comfortDropdown.captionText.text = comfortDropdown.options[comfortDropdown.value].text;
            catchDropdown.captionText.text = catchDropdown.options[catchDropdown.value].text;
        }
        humanInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    //可达性输入框
    public void accessibilityInputChange()
    {
        if (accessibilityInput.text != "")
        {
            accessibilityDropdown.options[accessibilityDropdown.value].text = accessibilityInput.text;
            if (accessibilityDropdown.value == 0)
            {
                humanTree11.name = accessibilityInput.text;
                humanTree21.name = accessibilityInput.text;
                humanTree31.name = accessibilityInput.text;
                humanText1.text = accessibilityInput.text;
                cityDropdown.options[0].text = accessibilityInput.text;
                viewDropdown.options[0].text = accessibilityInput.text;
                visibilityDropdown.options[0].text = accessibilityInput.text;
                humanDropdown.options[0].text = accessibilityInput.text;
                spaceDropdown.options[0].text = accessibilityInput.text;
                comfortDropdown.options[0].text = accessibilityInput.text;
                catchDropdown.options[0].text = cityInput.text;
            }
            else
            {
                if (accessibilityDropdown.value == 1)
                {
                    humanTree12.name = accessibilityInput.text;
                    humanTree22.name = accessibilityInput.text;
                    humanTree32.name = accessibilityInput.text;
                    humanText2.text = accessibilityInput.text;
                    cityDropdown.options[1].text = accessibilityInput.text;
                    viewDropdown.options[1].text = accessibilityInput.text;
                    visibilityDropdown.options[1].text = accessibilityInput.text;
                    humanDropdown.options[1].text = accessibilityInput.text;
                    spaceDropdown.options[1].text = accessibilityInput.text;
                    comfortDropdown.options[1].text = accessibilityInput.text;
                    catchDropdown.options[1].text = cityInput.text;
                }
                else
                {
                    humanTree13.name = accessibilityInput.text;
                    humanTree23.name = accessibilityInput.text;
                    humanTree33.name = accessibilityInput.text;
                    humanText3.text = accessibilityInput.text;
                    cityDropdown.options[2].text = accessibilityInput.text;
                    viewDropdown.options[2].text = accessibilityInput.text;
                    visibilityDropdown.options[2].text = accessibilityInput.text;
                    humanDropdown.options[2].text = accessibilityInput.text;
                    spaceDropdown.options[2].text = accessibilityInput.text;
                    comfortDropdown.options[2].text = accessibilityInput.text;
                    catchDropdown.options[2].text = cityInput.text;
                }
            }
            cityDropdown.captionText.text = cityDropdown.options[cityDropdown.value].text;
            viewDropdown.captionText.text = viewDropdown.options[viewDropdown.value].text;
            visibilityDropdown.captionText.text = visibilityDropdown.options[visibilityDropdown.value].text;
            humanDropdown.captionText.text = humanDropdown.options[humanDropdown.value].text;
            spaceDropdown.captionText.text = spaceDropdown.options[spaceDropdown.value].text;
            comfortDropdown.captionText.text = comfortDropdown.options[comfortDropdown.value].text;
            catchDropdown.captionText.text = catchDropdown.options[catchDropdown.value].text;
        }
        accessibilityInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    //操作空间输入框
    public void SpaceInputChange()
    {
        if (spaceInput.text != "")
        {
            spaceDropdown.options[spaceDropdown.value].text = spaceInput.text;
            if (spaceDropdown.value == 0)
            {
                humanTree11.name = spaceInput.text;
                humanTree21.name = spaceInput.text;
                humanTree31.name = spaceInput.text;
                humanText1.text = spaceInput.text;
                cityDropdown.options[0].text = spaceInput.text;
                viewDropdown.options[0].text = spaceInput.text;
                visibilityDropdown.options[0].text = spaceInput.text;
                accessibilityDropdown.options[0].text = spaceInput.text;
                humanDropdown.options[0].text = spaceInput.text;
                comfortDropdown.options[0].text = spaceInput.text;
                catchDropdown.options[0].text = cityInput.text;
            }
            else
            {
                if (spaceDropdown.value == 1)
                {
                    humanTree12.name = spaceInput.text;
                    humanTree22.name = spaceInput.text;
                    humanTree32.name = spaceInput.text;
                    humanText2.text = spaceInput.text;
                    cityDropdown.options[1].text = spaceInput.text;
                    viewDropdown.options[1].text = spaceInput.text;
                    visibilityDropdown.options[1].text = spaceInput.text;
                    accessibilityDropdown.options[1].text = spaceInput.text;
                    humanDropdown.options[1].text = spaceInput.text;
                    comfortDropdown.options[1].text = spaceInput.text;
                    catchDropdown.options[1].text = cityInput.text;
                }
                else
                {
                    humanTree13.name = spaceInput.text;
                    humanTree23.name = spaceInput.text;
                    humanTree33.name = spaceInput.text;
                    humanText3.text = spaceInput.text;
                    cityDropdown.options[2].text = spaceInput.text;
                    viewDropdown.options[2].text = spaceInput.text;
                    visibilityDropdown.options[2].text = spaceInput.text;
                    accessibilityDropdown.options[2].text = spaceInput.text;
                    humanDropdown.options[2].text = spaceInput.text;
                    comfortDropdown.options[2].text = spaceInput.text;
                    catchDropdown.options[2].text = cityInput.text;
                }
            }
            cityDropdown.captionText.text = cityDropdown.options[cityDropdown.value].text;
            viewDropdown.captionText.text = viewDropdown.options[viewDropdown.value].text;
            visibilityDropdown.captionText.text = visibilityDropdown.options[visibilityDropdown.value].text;
            accessibilityDropdown.captionText.text = accessibilityDropdown.options[accessibilityDropdown.value].text;
            humanDropdown.captionText.text = humanDropdown.options[humanDropdown.value].text;
            comfortDropdown.captionText.text = comfortDropdown.options[comfortDropdown.value].text;
            catchDropdown.captionText.text = catchDropdown.options[catchDropdown.value].text;
        }
        spaceInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    //舒适性输入框
    public void ComfortInputChange()
    {
        if (comfortInput.text != "")
        {
            comfortDropdown.options[comfortDropdown.value].text = comfortInput.text;
            if (comfortDropdown.value == 0)
            {
                humanTree11.name = comfortInput.text;
                humanTree21.name = comfortInput.text;
                humanTree31.name = comfortInput.text;
                humanText1.text = comfortInput.text;
                cityDropdown.options[0].text = comfortInput.text;
                viewDropdown.options[0].text = comfortInput.text;
                visibilityDropdown.options[0].text = comfortInput.text;
                accessibilityDropdown.options[0].text = comfortInput.text;
                spaceDropdown.options[0].text = comfortInput.text;
                humanDropdown.options[0].text = comfortInput.text;
                catchDropdown.options[0].text = cityInput.text;
            }
            else
            {
                if (comfortDropdown.value == 1)
                {
                    humanTree12.name = comfortInput.text;
                    humanTree22.name = comfortInput.text;
                    humanTree32.name = comfortInput.text;
                    humanText2.text = comfortInput.text;
                    cityDropdown.options[1].text = comfortInput.text;
                    viewDropdown.options[1].text = comfortInput.text;
                    visibilityDropdown.options[1].text = comfortInput.text;
                    accessibilityDropdown.options[1].text = comfortInput.text;
                    spaceDropdown.options[1].text = comfortInput.text;
                    humanDropdown.options[1].text = comfortInput.text;
                    catchDropdown.options[1].text = cityInput.text;
                }
                else
                {
                    humanTree13.name = comfortInput.text;
                    humanTree23.name = comfortInput.text;
                    humanTree33.name = comfortInput.text;
                    humanText3.text = comfortInput.text;
                    cityDropdown.options[2].text = comfortInput.text;
                    viewDropdown.options[2].text = comfortInput.text;
                    visibilityDropdown.options[2].text = comfortInput.text;
                    accessibilityDropdown.options[2].text = comfortInput.text;
                    spaceDropdown.options[2].text = comfortInput.text;
                    humanDropdown.options[2].text = comfortInput.text;
                    catchDropdown.options[2].text = cityInput.text;
                }
            }
            cityDropdown.captionText.text = cityDropdown.options[cityDropdown.value].text;
            viewDropdown.captionText.text = viewDropdown.options[viewDropdown.value].text;
            visibilityDropdown.captionText.text = visibilityDropdown.options[visibilityDropdown.value].text;
            accessibilityDropdown.captionText.text = accessibilityDropdown.options[accessibilityDropdown.value].text;
            spaceDropdown.captionText.text = spaceDropdown.options[spaceDropdown.value].text;
            humanDropdown.captionText.text = humanDropdown.options[humanDropdown.value].text;
            catchDropdown.captionText.text = catchDropdown.options[catchDropdown.value].text;
        }
        comfortInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    //抓放输入框
    public void CatchInputChange()
    {
        if (catchInput.text != "")
        {
            catchDropdown.options[catchDropdown.value].text = catchInput.text;
            if (catchDropdown.value == 0)
            {
                humanTree11.name = catchInput.text;
                humanTree21.name = catchInput.text;
                humanTree31.name = catchInput.text;
                humanText1.text = catchInput.text;
                cityDropdown.options[0].text = catchInput.text;
                viewDropdown.options[0].text = catchInput.text;
                visibilityDropdown.options[0].text = catchInput.text;
                accessibilityDropdown.options[0].text = catchInput.text;
                spaceDropdown.options[0].text = catchInput.text;
                humanDropdown.options[0].text = catchInput.text;
                comfortDropdown.options[0].text = catchInput.text;
            }
            else
            {
                if (catchDropdown.value == 1)
                {
                    humanTree11.name = catchInput.text;
                    humanTree21.name = catchInput.text;
                    humanTree31.name = catchInput.text;
                    humanText2.text = catchInput.text;
                    cityDropdown.options[1].text = catchInput.text;
                    viewDropdown.options[1].text = catchInput.text;
                    visibilityDropdown.options[1].text = catchInput.text;
                    accessibilityDropdown.options[1].text = catchInput.text;
                    spaceDropdown.options[1].text = catchInput.text;
                    humanDropdown.options[1].text = catchInput.text;
                    comfortDropdown.options[1].text = catchInput.text;
                }
                else
                {
                    humanTree11.name = catchInput.text;
                    humanTree21.name = catchInput.text;
                    humanTree31.name = catchInput.text;
                    humanText3.text = catchInput.text;
                    cityDropdown.options[2].text = catchInput.text;
                    viewDropdown.options[2].text = catchInput.text;
                    visibilityDropdown.options[2].text = catchInput.text;
                    accessibilityDropdown.options[2].text = catchInput.text;
                    spaceDropdown.options[2].text = catchInput.text;
                    humanDropdown.options[2].text = catchInput.text;
                    comfortDropdown.options[2].text = catchInput.text;
                }
            }
            cityDropdown.captionText.text = cityDropdown.options[cityDropdown.value].text;
            viewDropdown.captionText.text = viewDropdown.options[viewDropdown.value].text;
            visibilityDropdown.captionText.text = visibilityDropdown.options[visibilityDropdown.value].text;
            accessibilityDropdown.captionText.text = accessibilityDropdown.options[accessibilityDropdown.value].text;
            spaceDropdown.captionText.text = spaceDropdown.options[spaceDropdown.value].text;
            humanDropdown.captionText.text = humanDropdown.options[humanDropdown.value].text;
            comfortDropdown.captionText.text = comfortDropdown.options[comfortDropdown.value].text;
        }
        catchInput.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

}
