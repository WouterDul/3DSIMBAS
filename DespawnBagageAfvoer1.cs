using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBagageAfvoer1 : MonoBehaviour
{
    public static int FoutTeller = 0;

    private void OnTriggerEnter(Collider other)
    {
        //Eerst wordt er gecheckt of er de verificatie- of eindtest actief is.
        if (Eindtest.EindTest == true || Verificatietest.VerificatieTest == true)
        {
            if (Verificatietest.VerificatieTest == true)
            {
                BagageIDVerificatietest BagageIDVerificatietest = other.GetComponent<BagageIDVerificatietest>();
                if (BagageIDVerificatietest.RontgenStatus != 1)
                {
                    //Wanneer het bagagestuk niet overeen komt met de criteria voor het afvoerpunt wordt er een fout opgeteld.
                    FoutTeller = FoutTeller + 1;
                    Debug.Log("Afhandelingsfout Afvoer1: " + BagageIDVerificatietest.localteller);
                }
                Destroy(other.gameObject);
            }
            if (Eindtest.EindTest == true)
            {
                BagageIDEindtest BagageIDEindtest = other.GetComponent<BagageIDEindtest>();
                if (BagageIDEindtest.RontgenStatus != 1)
                {
                    //Wanneer het bagagestuk niet overeen komt met de criteria voor het afvoerpunt wordt er een fout opgeteld.
                    FoutTeller = FoutTeller + 1;
                    Debug.Log("Afhandelingsfout Afvoer1: " + BagageIDEindtest.localteller);
                }
                Destroy(other.gameObject);
            }
        }
        else
        {
            BagageID BagageID = other.GetComponent<BagageID>();
            if (BagageID.RontgenStatus != 1)
            {
                //Wanneer het bagagestuk niet overeen komt met de criteria voor het afvoerpunt wordt er een fout opgeteld.
                FoutTeller = FoutTeller + 1;
            }
            Destroy(other.gameObject);
        }
    }
}
