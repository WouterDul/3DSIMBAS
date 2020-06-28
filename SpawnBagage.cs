using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBagage : MonoBehaviour
{
    public Transform[] SpawnLocaties;
    public GameObject[] PrefabBagage;
    public GameObject[] BagageClone;

    private bool DelayCheck;
    public bool OnInterval;
    public float Capaciteit;
    public float SpawnDelay;
    public bool StartSignaal;

    public Button ButtonSpawnBagage;

    public float MinSchaalx = 0.7f;
    public float MinSchaaly = 0.5f;   
    public float MinSchaalz = 0.7f;    
    
    public float MaxSchaal = 1f;

    public List<GameObject> Bagagelijst = new List<GameObject>();
    public int GespawndTeller;


    private void Start()
    {
        //Bij het starten wordt de delaycheck op false gezet, zodat de delay uitgevoerd wordt. Ook wordt de button geconfigureerd.
        DelayCheck = false;
        ButtonSpawnBagage = ButtonSpawnBagage.GetComponent<Button>();
        ButtonSpawnBagage.onClick.AddListener(Spawn);
    }

    private void Update()
    {
        //Opvragen van signalen en omrekenen capaciteit naar een delay.
        StartSignaal = UICommunicatie.StartSignaal;        
        OnInterval = UICommunicatie.SpawnOnInterval;
        Capaciteit = UICommunicatie.Capaciteit;
        SpawnDelay = 60 / Capaciteit;
        
        //Het aanroepen van de coroutinefunctie. Dit gebeurt alleen wanneer de power is aangeschakeld en de delaycheck niet al uitgevoerd wordt.
        if (OnInterval == true && StartSignaal == true)
        {
            if (DelayCheck == false)
            {
                StartCoroutine(IntervalSpawn());
            }
        }

        //Wanneer de simulatie gereset wordt worden alle bagagestukken, die zijn bijgehouden in een lijst, verwijderd.
        if (UICommunicatie.DespawnSignaal == true)
        {
            for(int i = 0; i <= Bagagelijst.Count - 1; i++)
            {
                var TeDeleten = Bagagelijst[i];
                Bagagelijst.Remove(Bagagelijst[i]);
                Destroy(TeDeleten);
            }
        }

        //Zorgen dat bepaalde knoppen niet kunnen worden bediend als een andere actief is.
        if ((PLCCom.PowerAanvoer1 == false) || (UICommunicatie.SpawnOnInterval == true || UICommunicatie.StartSignaal == false)) { ButtonSpawnBagage.interactable = false; }
        else {  ButtonSpawnBagage.interactable = true; }
    }


    
    public IEnumerator IntervalSpawn()
    //Het spawnen van bagage over een gedefinieerd tijdsinterval. Elk bagagestuk krijgt een willekeurige kleur en schaal toegewezen.
    {
        DelayCheck = true;
        BagageClone[0] = Instantiate(PrefabBagage[0], SpawnLocaties[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        BagageClone[0].GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        BagageClone[0].transform.localScale = new Vector3(Random.Range(MinSchaalx,MaxSchaal), Random.Range(MinSchaaly, MaxSchaal), Random.Range(MinSchaalz, MaxSchaal));
        Bagagelijst.Add(BagageClone[0]);
        yield return new WaitForSeconds(SpawnDelay);
        DelayCheck = false;
    }    
    
    void Spawn()
    {
        if(StartSignaal == true && OnInterval == false)
        {
            BagageClone[0] = Instantiate(PrefabBagage[0], SpawnLocaties[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            BagageClone[0].GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            BagageClone[0].transform.localScale = new Vector3(Random.Range(MinSchaalx, MaxSchaal), Random.Range(MinSchaaly, MaxSchaal), Random.Range(MinSchaalz, MaxSchaal));
            Bagagelijst.Add(BagageClone[0]);
        }
    }
}