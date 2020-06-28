using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Aanvoer1 : MonoBehaviour
{
    public bool Power;
    public float Snelheid;
    public bool Richting;

    Vector3 RichtingVector;
    float xrichting;
    float yrichting = 0;
    float zrichting = 0;

    public List<GameObject> opBand;

    void FixedUpdate()
    {
        //Uitlezen van power, snelheid en richting vanuit de UI-objecten.
        Power = PLCCom.PowerAanvoer1;
        Snelheid = PLCCom.SnelheidAanvoer1;
        Richting = PLCCom.RichtingAanvoer1;

        //Bepalen van de bewegingsrichting.
        if (Richting == true){xrichting = 1;}
        else{xrichting = -1;}

        //Het bewegen van het object op de band. Dit gebeurt alleen wanneer de powerknop geactiveerd is en er objecten op de band liggen.
        if (Power == true)
        {
            for (int i = 0; i <= opBand.Count - 1; i++)
            {
                RichtingVector = new Vector3(xrichting, yrichting, zrichting);
                opBand[i].GetComponent<Rigidbody>().velocity = Snelheid * RichtingVector * Time.fixedDeltaTime;
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
