using ArduinoBluetoothAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class BluetoothManager : MonoBehaviour
{
    public bool isBluetoothManagerReady { get; set; }
    private BluetoothHelper helper;
    void Awake()
    {
        isBluetoothManagerReady = false;
        BluetoothHelper.BLE = true;
        helper = BluetoothHelper.GetInstance("HMSoft");
        helper.OnScanEnded += OnScanEnded;
        helper.OnConnected += OnConnected;
        helper.OnConnectionFailed += OnConnectionFailed;
        helper.setTerminatorBasedStream("\n");
        if (!helper.IsBluetoothEnabled())
            helper.EnableBluetooth(true);
        Debug.Log("Bluetooth enabled");
        helper.ScanNearbyDevices();

        Permission.RequestUserPermission(Permission.CoarseLocation);
    }

    void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
    {
        Debug.Log("Scanning Finished");
        if (helper.isDevicePaired())
            helper.Connect();
        else
            helper.ScanNearbyDevices();
    }

    public void SendData(string noteInfo)
    {
        helper.SendData(noteInfo);
    }


    void OnConnected(BluetoothHelper helper)
    {
        Debug.Log("Connection succes");
        isBluetoothManagerReady = true;
        Debug.Log("BluetoothManager Ready");
        helper.StartListening();
    }

    void OnConnectionFailed(BluetoothHelper helper)
    {
        Debug.Log("Connection failed");
        helper.ScanNearbyDevices();
    }

    void OnDestroy()
    {
        Debug.Log("Bluetooth disconnect");
        helper.OnScanEnded -= OnScanEnded;
        helper.OnConnected -= OnConnected;
        helper.OnConnectionFailed -= OnConnectionFailed;
        helper.Disconnect();
    }

    void OnDataReceived()
    {
        Debug.Log("Receiving data");
    }
}
