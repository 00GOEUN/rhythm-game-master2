using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;
using UnityEngine.Android;

public class BLE : MonoBehaviour
{
    private BluetoothHelper helper;
    void Start()
    {
        BluetoothHelper.BLE = true;
        helper = BluetoothHelper.GetInstance("HMSoft");
        helper.OnScanEnded += OnScanEnded;
        helper.OnConnected += OnConnected;
        helper.OnConnectionFailed += OnConnectionFailed;
        helper.ScanNearbyDevices();
        
        Permission.RequestUserPermission(Permission.CoarseLocation);
    }

    void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
    {
        if(helper.isDevicePaired())
            helper.Connect();
        else
            helper.ScanNearbyDevices();
    }

    void OnConnected(BluetoothHelper helper)
    {
        helper.setLengthBasedStream();
    }

    void OnConnectionFailed(BluetoothHelper helper)
    {
        Debug.Log("Connection failed");
        helper.ScanNearbyDevices();
    }

    void OnDestroy()
    {
        helper.OnScanEnded -= OnScanEnded;
        helper.OnConnected -= OnConnected;
        helper.OnConnectionFailed -= OnConnectionFailed;
        helper.Disconnect();
    }
}
