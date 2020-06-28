using Org.BouncyCastle.Asn1.Esf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICommunicatie : MonoBehaviour
{
    public Toggle ToggleStartSignaal;
    public Toggle ToggleSpawnOnInterval;
    public Slider SliderCapaciteit;
    public Button ButtonCamera1;
    public Button ButtonCamera2;
    public Button ButtonCamera3;
    public Button ButtonCamera4;
    public Button ButtonReset;

    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    public Camera Camera4;

    public static bool StartSignaal;
    public static bool SpawnOnInterval;
    public static float Capaciteit;
    public static bool ResetSignaal;
    public static bool DespawnSignaal;
    public static float SensorWacht;

    public Text TextTellerRontgenband1;
    public Text TextStatusRontgenband1;
    public Text TextBestemmingIDband1;
    public Text TextFoutDespawnAfvoer1;
    public Text TextFoutDespawnOpslag1;
    public Text TextFoutDespawnAfvoer2;
    public Text TextCapaciteit;

    void Start()
    {
        //Configureren andere UI-objecten.
        ToggleStartSignaal = ToggleStartSignaal.GetComponent<Toggle>();
        ToggleSpawnOnInterval = ToggleSpawnOnInterval.GetComponent<Toggle>();
        SliderCapaciteit = SliderCapaciteit.GetComponent<Slider>();
        ButtonCamera1 = ButtonCamera1.GetComponent<Button>();
        ButtonCamera2 = ButtonCamera2.GetComponent<Button>();
        ButtonCamera3 = ButtonCamera3.GetComponent<Button>();
        ButtonCamera4 = ButtonCamera4.GetComponent<Button>();
        ButtonReset = ButtonReset.GetComponent<Button>();
        ButtonCamera1.onClick.AddListener(SwitchCamera1);
        ButtonCamera2.onClick.AddListener(SwitchCamera2);
        ButtonCamera3.onClick.AddListener(SwitchCamera3);
        ButtonCamera4.onClick.AddListener(SwitchCamera4);
        ButtonReset.onClick.AddListener(Reset);
        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;
        Camera4.enabled = false;
        SensorWacht = 2;
    }

    void Update()
    {
        //Uitlezen UI-objecten.
        StartSignaal = ToggleStartSignaal.isOn;
        SpawnOnInterval = ToggleSpawnOnInterval.isOn;
        Capaciteit = SliderCapaciteit.value;

        //Instellen andere UI-objecten.
        TextTellerRontgenband1.text = "Geteld: " + ScanRontgenband1.BagageTeller;
        TextStatusRontgenband1.text = "Rontgen: " + ScanRontgenband1.RontgenStatus;
        TextBestemmingIDband1.text = "Bestemming: " + Scan1ID.Bestemming;
        TextFoutDespawnAfvoer1.text = "Afhandelingsfout Afvoer1: " + DespawnBagageAfvoer1.FoutTeller;
        if(DespawnBagageAfvoer1.FoutTeller > 0){TextFoutDespawnAfvoer1.color = Color.red;}
        TextFoutDespawnOpslag1.text = "Afhandelingsfout Opslag1: " + DespawnBagageOpslag1.FoutTeller;
        if (DespawnBagageOpslag1.FoutTeller > 0) { TextFoutDespawnOpslag1.color = Color.red; }
        TextFoutDespawnAfvoer2.text = "Afhandelingsfout Afvoer2: " + DespawnBagageAfvoer2.FoutTeller;
        if (DespawnBagageAfvoer2.FoutTeller > 0) { TextFoutDespawnAfvoer2.color = Color.red; }
        TextCapaciteit.text = Capaciteit.ToString() + " Bagagestukken / min";

        //Resetten van signalen als de simulatie gereset wordt.
        if(DespawnSignaal == true)
        {
            ScanRontgenband1.BagageTeller = 0;
            ScanRontgenband1.RontgenStatus = 0;
            Scan1ID.Bestemming = 0;

            DespawnBagageAfvoer1.FoutTeller = 0;
            DespawnBagageAfvoer2.FoutTeller = 0;
            DespawnBagageOpslag1.FoutTeller = 0;

            TextFoutDespawnAfvoer1.color = Color.black;
            TextFoutDespawnAfvoer2.color = Color.black;
            TextFoutDespawnOpslag1.color = Color.black;

            Eindtest.EindTest = false;
            Verificatietest.VerificatieTest = false;
            Eindtest.Testteller = 0;
            Verificatietest.Testteller = 0;
        }

        //Zorgen dat bepaalde knoppen niet kunnen worden bediend als een andere actief is.
        if (PLCCom.PowerAanvoer1 == false || StartSignaal == false) { ToggleSpawnOnInterval.interactable = false; ToggleSpawnOnInterval.isOn = false; }
        if(PLCCom.PowerAanvoer1 == true) { ToggleSpawnOnInterval.interactable = true; }
    }

    //Switched naar deze camera als de bijbehorende knop wordt ingedrukt.
    void SwitchCamera1()
    {
        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;
        Camera4.enabled = false;
    }
    //Switched naar deze camera als de bijbehorende knop wordt ingedrukt.
    void SwitchCamera2()
    {
        Camera1.enabled = false;
        Camera2.enabled = true;
        Camera3.enabled = false;
        Camera4.enabled = false;
    }
    //Switched naar deze camera als de bijbehorende knop wordt ingedrukt.
    void SwitchCamera3()
    {
        Camera1.enabled = false;
        Camera2.enabled = false;
        Camera3.enabled = true;
        Camera4.enabled = false;
    }
    //Switched naar deze camera als de bijbehorende knop wordt ingedrukt.
    void SwitchCamera4()
    {
        Camera1.enabled = false;
        Camera2.enabled = false;
        Camera3.enabled = false;
        Camera4.enabled = true;
    }
    //Wanneer de resetknop wordt ingedruk wordt de reset in gang gezet.
    private void Reset()
    {

        if (ToggleStartSignaal.isOn == true) { ToggleStartSignaal.isOn = false; }
        if (ToggleStartSignaal.interactable == true) { ToggleStartSignaal.interactable = false; }

        StartCoroutine(ResetDelay());
    }
    //Coroutine voor het resseten van de simulatie. Om de simulatie de tijd te geven zijn delays toegepast.
    public IEnumerator ResetDelay()
    {
        ResetSignaal = true;
        yield return new WaitForSeconds(5);
        ResetSignaal = false;
        DespawnSignaal = true;
        yield return new WaitForSeconds(2);
        DespawnSignaal = false;
        ToggleStartSignaal.interactable = true;
    }
}
