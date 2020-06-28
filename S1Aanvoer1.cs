using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S1Aanvoer1 : MonoBehaviour
{
    public static bool SensorON;
    public static bool SensorONMemory;

    //Default waarde is altijd uit
    private void Start()
    {
        SensorON = false;
    }
    //Waneer de trigger wordt geraakt is de waarde hoog.
    private void OnTriggerEnter(Collider other)
    {
        SensorON = true;
    }

    //Bij het verlaten van de trigger is de waarde laag.
    private void OnTriggerExit(Collider other)
    {
        SensorON = false;
    }

    //Wanneer een sensor wordt geactiveerd door een OnTriggerEnter wordt een coroutine gestart.
    private void Update()
    {
        if (SensorON == true)
        {
            StartCoroutine(SensorHigh());
        }
    }

    //De coroutine houdt de variabele van de sensor hoog voor een tijd gedefinieerd in het script 'UICommunicatie'.
    //Dit is om te voorkomen dat de sensor te kort aan is om opgepikt te worden door de OPCUA-communicatie.
    public IEnumerator SensorHigh()
    {
        SensorONMemory = true;
        yield return new WaitForSeconds(UICommunicatie.SensorWacht);
        SensorONMemory = false;
    }
}
