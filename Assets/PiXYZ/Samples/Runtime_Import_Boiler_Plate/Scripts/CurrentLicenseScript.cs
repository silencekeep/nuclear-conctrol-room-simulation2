using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PiXYZ.PiXYZImportScript;

public class CurrentLicenseScript : MonoBehaviour
{
    public Text infoText;
    public Text detailText;

    // Use this for initialization
    void Start () {
        PiXYZConfig.CheckLicense();
	}
	
	// Update is called once per frame
	void Update () {
		if(infoText != null)
        {
            if (Plugin4UnityI.checkLicense())
            {
                detailText.text = "";
                infoText.text = "";
                infoText.color = Color.black;
                infoText.fontSize = 14;
                infoText.alignment = TextAnchor.MiddleLeft;
                string[] names;
                string[] values;
                if (Plugin4UnityI.isFloatingLicense())
                {
                    Plugin4UnityI.getLicenseServerReturn ret = Plugin4UnityI.getLicenseServer();
                    names = new string[]{
                        "License",
                        "",
                        "Server address",
                        "Port"
                    };
                    values = new string[] {
                        "Floating",
                        "",
                        ret.serverHost,
                        ret.serverPort.ToString()
                    };
                }
                else
                {
                    names = new string[]{
                        "License version",
                        "Start date",
                        "End date",
                        "Company name",
                        "Name",
                        "E-mail"
                    };
                    Plugin4UnityI.LicenseInfos info = Plugin4UnityI.getCurrentLicenseInfos();
                    values = new string[] {
                        info.version,
                        PiXYZ4UnityUtils.convertPiXYZDateToString(info.startDate),
                        PiXYZ4UnityUtils.convertPiXYZDateToString(info.endDate),
                        info.customerCompany,
                        info.customerName,
                        info.customerEmail,
                    };
                }

                for (int i = 0; i < names.Length; ++i)
                {
                    infoText.text += names[i] + (names[i].Length > 0 ? ":\n" : "\n");
                    detailText.text += values[i] + "\n";
                }
            }
            else
            {
                infoText.text = "Your license is inexistant or invalid.";
                infoText.color = Color.red;
                infoText.fontSize = 18;
                infoText.alignment = TextAnchor.MiddleCenter;
            }
        }
	}
}
