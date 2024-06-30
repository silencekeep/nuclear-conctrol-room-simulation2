using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using PiXYZ.PiXYZImportScript;

public class LiceseServerScript : MonoBehaviour {

    public InputField address;
    public InputField port;
    public bool flexLM;
    public ErrorWinScript errorWindow;

    public UnityEvent onSuccess;

    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        Plugin4UnityI.getLicenseServerReturn ret = Plugin4UnityI.getLicenseServer();
        if (address.text == "") address.text = ret.serverHost;
        if (port.text == "") port.text = ret.serverPort.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void apply()
    {
        Plugin4UnityI.configureLicenseServer(address.text, ushort.Parse(port.text), new Plugin4UnityI.Boolean(flexLM));
        onSuccess.Invoke();
    }
}
