using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LangeAfstand2 : MonoBehaviour
{
    public bool Power;
    public float Snelheid;
    public bool Richting;

    public int Richtingwaarde;

    public List<GameObject> opBand;

    public Transform Rotatiepunt;

    void FixedUpdate()
    {
        //Uitlezen van power, snelheid en richting vanuit de UI-objecten.
        Power = PLCCom.PowerLangeAfstand2;
        Snelheid = PLCCom.SnelheidLangeAfstand2;
        Richting = PLCCom.RichtingLangeAfstand2;

        //Bepalen van de rotatierichting.
        if (Richting == true) {Richtingwaarde = 1;}
        else {Richtingwaarde = -1;}

        //Het roteren van het object op de band. Dit gebeurt alleen wanneer de powerknop geactiveerd is en er objecten op de band liggen.
        if (Power == true)
        {
            for (int i = 0; i <= opBand.Count - 1; i++)
            {
                opBand[i].transform.RotateAround(Rotatiepunt.position, Rotatiepunt.up, Richtingwaarde * Snelheid * Time.fixedDeltaTime);
            }
        }
    }

    //Bij het registeren van een collisie wordt de count van objecten opgehoogd.
    private void OnCollisionEnter(Collision collision)
    {
        opBand.Add(collision.gameObject);
    }
    //Wanneer het systeem gereset wordt worden de objecten van de band verwijderd.
    private void OnCollisionStay(Collision collision)
    {
        if (UICommunicatie.ResetSignaal == true)
        {
            opBand.Clear();
        }
    }
    //Wanneer een object geen collisie meer maakt wordt deze weggehaald uit de count.
    private void OnCollisionExit(Collision collision)
    {
        opBand.Remove(collision.gameObject);
    }
}
