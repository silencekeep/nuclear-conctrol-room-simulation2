using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using PiXYZ.PiXYZImportScript;

public class LicenseManagerScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void GenerateActivationCode()
    {
        SaveFileDialog save = new SaveFileDialog();
        save.InitialDirectory = UnityEngine.Application.dataPath;
        save.FileName = "PiXYZ_activationCode.bin";
        save.Filter = "Binary file | *.bin";
        if (save.ShowDialog() == DialogResult.OK)
        {
            Plugin4UnityI.generateActivationCode(new Plugin4UnityI.OutputFilePath(save.FileName));
        }
    }

    public void InstalLicense()
    {
        OpenFileDialog open = new OpenFileDialog();
        open.InitialDirectory = UnityEngine.Application.dataPath;
        open.Filter = "License file|*.lic|Binary file|*.bin";
        if (open.ShowDialog() == DialogResult.OK)
        {
            string path = open.FileName;
            if (path.ToLower().EndsWith(".bin") || path.ToLower().EndsWith(".lic"))
            {
                Plugin4UnityI.installActivationCode(new Plugin4UnityI.FilePath(path));
            }
        }
    }

    public void GenerateReleaseCode()
    {
        SaveFileDialog save = new SaveFileDialog();
        save.InitialDirectory = UnityEngine.Application.dataPath;
        save.FileName = "PiXYZ_activationCode.bin";
        save.Filter = "Binary file | *.bin";
        if (save.ShowDialog() == DialogResult.OK)
        {
            Plugin4UnityI.generateDeactivationCode(new Plugin4UnityI.OutputFilePath(save.FileName));
        }
    }
}
