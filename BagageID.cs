using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagageID : MonoBehaviour
{
    public int RontgenStatus;
    public int Bestemming;

    private void Start()
    {
        //Het toeschrijven van een willekeurige waarde van 1 tot en met 9. Deze waarde bepaalt of het object wordt geaccepteerd door de rontgenscan.
        RontgenStatus = Random.Range(1, 10);
        //Het toeschrijven van een willekeurige waarde van 1 tot en met 4. Deze waarde bepaalt de bestemming van het object.
        Bestemming = Random.Range(1, 5);
    }
}
