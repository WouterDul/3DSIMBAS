using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScanRontgenband1 : MonoBehaviour
{
    public static int RontgenStatus;
    public static int BagageTeller;
   
    //Bij het raken van de trigger wordt de status van de rontgenscan uitgelezen uit het object dat het triggert. Ook wordt de bagage geteld.
    private void OnTriggerEnter(Collider other)
    {
        if (Eindtest.EindTest == true || Verificatietest.VerificatieTest == true)
        {
            if(Verificatietest.VerificatieTest == true)
            {
                BagageIDVerificatietest BagageIDVerificatietest = other.GetComponent<BagageIDVerificatietest>();
                RontgenStatus = BagageIDVerificatietest.RontgenStatus;
                BagageTeller++;
            }
            if(Eindtest.EindTest == true)
            {
                BagageIDEindtest BagageIDEindtest = other.GetComponent<BagageIDEindtest>();
                RontgenStatus = BagageIDEindtest.RontgenStatus;
                BagageTeller++;
            }
        }
        else
        {
            BagageID BagageID = other.GetComponent<BagageID>();
            RontgenStatus = BagageID.RontgenStatus;
            BagageTeller++;
        }


    }
}
