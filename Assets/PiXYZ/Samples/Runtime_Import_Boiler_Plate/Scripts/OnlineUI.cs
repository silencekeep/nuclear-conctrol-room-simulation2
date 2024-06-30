using PiXYZ.PiXYZImportScript;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineUI : MonoBehaviour
{
    public RectTransform onlineCreds;
    public RectTransform onlineWin;

    public InputField username;
    public InputField password;

    public Text LicenseTitles;
    public Text LicenseDetails;

    public Dropdown licenseList;
    public Plugin4UnityI.WebLicenseInfoList webLicences;
    public Button installBtn;

    public ErrorWinScript errorWindow;

    private bool connected = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        onlineCreds.gameObject.SetActive(!connected);
        onlineWin.gameObject.SetActive(connected);
    }

    public void connect()
    {
        if (username.text.Length != 0 && password.text.Length != 0)
            refresh();
    }

    public void refresh()
    {
        try
        {
            webLicences = Plugin4UnityI.retrieveAvailableLicenses(username.text, new Plugin4UnityI.Password(password.text));
        }
        catch (Exception e)
        {
            errorWindow.popWithText(e.Message);
            return;
        }
        populateLicenseList();
    }

    private void populateLicenseList()
    {
        int index = licenseList.value;
        licenseList.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < webLicences.Length(); ++i)
        {
            string option = "License " + (i + 1) + ": " + webLicences[i].product + "  [" + PiXYZ4UnityUtils.convertPiXYZDateToString(webLicences[i].validity) + "]";
            if (webLicences[i].onMachine)
                option += "  (installed)";
            options.Add(option);
        }
        licenseList.AddOptions(options);
        switchLicense(index);
    }

    public void switchLicense(int index)
    {
        Plugin4UnityI.WebLicenseInfo info = webLicences[index];
        Plugin4UnityI.Date d = webLicences[index].validity;
        int daysRemaining = Math.Max(0, (new DateTime(d.year, d.month, d.day) - DateTime.Now).Days + 1);
        string remainingTextColor = daysRemaining > 185 ? "green" : daysRemaining > 92 ? "orange" : "red";
        bool installed = info.onMachine;
        string productName = info.product;
        string validity = PiXYZ4UnityUtils.convertPiXYZDateToString(info.validity)
            + "   (<color='" + remainingTextColor + "'><b>" + daysRemaining + "</b> Day" + (daysRemaining > 1 ? "s" : "") + " remaining</color>)";
        string licenseUse = "" + (int)info.inUse + " / " + (int)info.count;
        string currentlyInstalled = installed ? "<color='green'>true</color>" : "false";

        LicenseTitles.text = "Product name: \n";
        LicenseTitles.text += "Validity: \n";
        LicenseTitles.text += "License use: \n";
        LicenseTitles.text += "Currently installed: \n";

        LicenseDetails.text = productName + "\n";
        LicenseDetails.text += validity + "\n";
        LicenseDetails.text += licenseUse + "\n";
        LicenseDetails.text += currentlyInstalled + "\n";

        installBtn.gameObject.SetActive(!installed);
    }

    public void installLicense()
    {
        try
        {
            Plugin4UnityI.requestWebLicense(username.text, new Plugin4UnityI.Password(password.text), licenseList.value);
        }
        catch (Exception e)
        {
            errorWindow.popWithText("An error occured while installing the license: \n" + e.Message);
        }
    }

    public void releaseLicense()
    {
        try
        {
            Plugin4UnityI.releaseWebLicense(username.text, new Plugin4UnityI.Password(password.text), licenseList.value);
        }
        catch (Exception e)
        {
            errorWindow.popWithText("An error occured while installing the license: \n" + e.Message);
        }
    }
}
