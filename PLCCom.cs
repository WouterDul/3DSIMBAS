using nl.jbsystems.unity.opcua;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PLCCom : MonoBehaviour
{
    public static bool PowerAanvoer1;
    public static bool RichtingAanvoer1;
    public static float SnelheidAanvoer1;

    public static bool PowerPreBuffer1;
    public static bool RichtingPreBuffer1;
    public static float SnelheidPreBuffer1;

    public static bool PowerPreBuffer2;
    public static bool RichtingPreBuffer2;
    public static float SnelheidPreBuffer2;

    public static bool PowerPreBuffer3;
    public static bool RichtingPreBuffer3;
    public static float SnelheidPreBuffer3;

    public static bool PowerPreBuffer4;
    public static bool RichtingPreBuffer4;
    public static float SnelheidPreBuffer4;

    public static bool PowerPreBuffer5;
    public static bool RichtingPreBuffer5;
    public static float SnelheidPreBuffer5;

    public static bool PowerPreBuffer0;
    public static bool RichtingPreBuffer0;
    public static float SnelheidPreBuffer0;

    public static bool PowerRontgenband1;
    public static bool RichtingRontgenband1;
    public static float SnelheidRontgenband1;

    public static bool PowerSorteerder1;
    public static bool RichtingSorteerder1;
    public static float SnelheidSorteerder1;
    public static bool ZijrichtingSorteerder1;

    public static bool PowerAfvoer1;
    public static bool RichtingAfvoer1;
    public static float SnelheidAfvoer1;

    public static bool PowerMidBuffer1;
    public static bool RichtingMidBuffer1;
    public static float SnelheidMidBuffer1;

    public static bool PowerIDband1;
    public static bool RichtingIDband1;
    public static float SnelheidIDband1;

    public static bool PowerSorteerder2;
    public static bool RichtingSorteerder2;
    public static float SnelheidSorteerder2;
    public static bool ZijrichtingSorteerder2;

    public static bool PowerOpslag1;
    public static bool RichtingOpslag1;
    public static float SnelheidOpslag1;

    public static bool PowerLangeAfstand1;
    public static bool RichtingLangeAfstand1;
    public static float SnelheidLangeAfstand1;

    public static bool PowerLangeAfstand2;
    public static bool RichtingLangeAfstand2;
    public static float SnelheidLangeAfstand2;

    public static bool PowerLangeAfstand3;
    public static bool RichtingLangeAfstand3;
    public static float SnelheidLangeAfstand3;

    public static bool PowerLangeAfstand4;
    public static bool RichtingLangeAfstand4;
    public static float SnelheidLangeAfstand4;

    public static bool PowerLangeAfstand5;
    public static bool RichtingLangeAfstand5;
    public static float SnelheidLangeAfstand5;

    public static bool PowerLangeAfstand6;
    public static bool RichtingLangeAfstand6;
    public static float SnelheidLangeAfstand6;

    public static bool PowerAfvoer2;
    public static bool RichtingAfvoer2;
    public static float SnelheidAfvoer2;

    public bool isConnected = false;

    public OPCUACommunication opcUACommunication;

    //Het lezen van een PLC output (boolean)
    private bool readPLCOutputBoolean(string nodeId)
    {
        Tag tag = new Tag();
        tag.nodeId = nodeId;
        Tag _tag = opcUACommunication.addOrGetTagToMonitor(tag);

        bool result = false;
        try
        {
            result = bool.Parse(_tag.OnlineValue);
        }
        catch (Exception e)
        {
            //Debug.Log("Bool.Parsing error: " + e.Message);
        }

        return result;
    }

    //Het lezen van een PLC output (float)
    private float readPLCOutputFloat(string nodeId)
    {
        Tag tag = new Tag();
        tag.nodeId = nodeId;
        Tag _tag = opcUACommunication.addOrGetTagToMonitor(tag);

        return float.Parse(_tag.OnlineValue);
    }
    //Het schrijven van een PLC input
    private void writePLCInput(string nodeId, object value)
    {
        Tag tag = new Tag();
        tag.nodeId = nodeId;

        tag.ForceValue = value.ToString();
        opcUACommunication.addTagToWrite(tag);
    }

    void FixedUpdate()
    {
        if (!isConnected) return;

        //Schrijft de algemene signalen naar OPCUA
        writePLCInput("ns=3;s=\"I_Startsignaal\"", UICommunicatie.StartSignaal);
        writePLCInput("ns=3;s=\"I_BagageTeller\"", ScanRontgenband1.BagageTeller);
        writePLCInput("ns=3;s=\"I_Bestemming\"", Scan1ID.Bestemming);
        writePLCInput("ns=3;s=\"I_RontgenStatus\"", ScanRontgenband1.RontgenStatus);
        writePLCInput("ns=3;s=\"I_ResetSignaal\"", UICommunicatie.ResetSignaal);
        writePLCInput("ns=3;s=\"I_FoutAfvoer1\"", DespawnBagageAfvoer1.FoutTeller);
        writePLCInput("ns=3;s=\"I_FoutAfvoer2\"", DespawnBagageAfvoer2.FoutTeller);
        writePLCInput("ns=3;s=\"I_FoutOpslag1\"", DespawnBagageOpslag1.FoutTeller);

        //Uitlezen van de OPCUA-communicatie. Dit is geordend per band. 
        //Alleen de waarden die benodigd zijn worden verstuurd, de rest is neergezet als comment zodat deze makkelijk actief kunnen worden gemaakt wanneer nodig.
        PowerAanvoer1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerAanvoer1\"");
        RichtingAanvoer1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingAanvoer1\"");
        SnelheidAanvoer1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidAanvoer1\"");
        //writePLCInput("ns=3;s=\"I_S1Aanvoer1\"", S1Aanvoer1.SensorONMemory);
        //writePLCInput("ns=3;s=\"I_S2Aanvoer1\"", S2Aanvoer1.SensorONMemory);
        //writePLCInput("ns=3;s=\"I_S3Aanvoer1\"", S3Aanvoer1.SensorONMemory);
        //writePLCInput("ns=3;s=\"I_S4Aanvoer1\"", S4Aanvoer1.SensorONMemory);

        PowerPreBuffer5 = readPLCOutputBoolean("ns=3;s=\"Q_PowerPreBuffer5\"");
        RichtingPreBuffer5 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingPreBuffer5\"");
        SnelheidPreBuffer5 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidPreBuffer5\"");
        //writePLCInput("ns=3;s=\"I_S1PreBuffer5\"", S1PreBuffer5.SensorONMemory);
        //writePLCInput("ns=3;s=\"I_S2PreBuffer5\"", S2PreBuffer5.SensorONMemory);
        //writePLCInput("ns=3;s=\"I_S3PreBuffer5\"", S3PreBuffer5.SensorONMemory);
        //writePLCInput("ns=3;s=\"I_S4PreBuffer5\"", S4PreBuffer5.SensorONMemory);

        PowerPreBuffer4 = readPLCOutputBoolean("ns=3;s=\"Q_PowerPreBuffer4\"");
        RichtingPreBuffer4 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingPreBuffer4\"");
        SnelheidPreBuffer4 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidPreBuffer4\"");
        /*writePLCInput("ns=3;s=\"I_S1PreBuffer4\"", S1PreBuffer4.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2PreBuffer4\"", S2PreBuffer4.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3PreBuffer4\"", S3PreBuffer4.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4PreBuffer4\"", S4PreBuffer4.SensorONMemory);*/

        PowerPreBuffer3 = readPLCOutputBoolean("ns=3;s=\"Q_PowerPreBuffer3\"");
        RichtingPreBuffer3 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingPreBuffer3\"");
        SnelheidPreBuffer3 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidPreBuffer3\"");
        /*writePLCInput("ns=3;s=\"I_S1PreBuffer3\"", S1PreBuffer3.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2PreBuffer3\"", S2PreBuffer3.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3PreBuffer3\"", S3PreBuffer3.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4PreBuffer3\"", S4PreBuffer3.SensorONMemory);*/

        PowerPreBuffer2 = readPLCOutputBoolean("ns=3;s=\"Q_PowerPreBuffer2\"");
        RichtingPreBuffer2 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingPreBuffer2\"");
        SnelheidPreBuffer2 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidPreBuffer2\"");
        /*writePLCInput("ns=3;s=\"I_S1PreBuffer2\"", S1PreBuffer2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2PreBuffer2\"", S2PreBuffer2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3PreBuffer2\"", S3PreBuffer2.SensorONMemory);*/

        PowerPreBuffer1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerPreBuffer1\"");
        RichtingPreBuffer1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingPreBuffer1\"");
        SnelheidPreBuffer1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidPreBuffer1\"");
        /*writePLCInput("ns=3;s=\"I_S1PreBuffer1\"", S1PreBuffer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2PreBuffer1\"", S2PreBuffer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3PreBuffer1\"", S3PreBuffer1.SensorONMemory);*/

        PowerPreBuffer0 = readPLCOutputBoolean("ns=3;s=\"Q_PowerPreBuffer0\"");
        RichtingPreBuffer0 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingPreBuffer0\"");
        SnelheidPreBuffer0 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidPreBuffer0\"");
        /*writePLCInput("ns=3;s=\"I_S1PreBuffer0\"", S1PreBuffer0.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2PreBuffer0\"", S2PreBuffer0.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3PreBuffer0\"", S3PreBuffer0.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4PreBuffer0\"", S4PreBuffer0.SensorONMemory);*/

        PowerRontgenband1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerRontgenband1\"");
        RichtingRontgenband1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingRontgenband1\"");
        SnelheidRontgenband1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidRontgenband1\"");
        writePLCInput("ns=3;s=\"I_S1Rontgenband1\"", S1Rontgenband1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2Rontgenband1\"", S2Rontgenband1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3Rontgenband1\"", S3Rontgenband1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4Rontgenband1\"", S4Rontgenband1.SensorONMemory);

        PowerSorteerder1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerSorteerder1\"");
        RichtingSorteerder1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingSorteerder1\"");
        ZijrichtingSorteerder1 = readPLCOutputBoolean("ns=3;s=\"Q_ZijrichtingSorteerder1\"");
        SnelheidSorteerder1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidSorteerder1\"");
        /*writePLCInput("ns=3;s=\"I_S1Sorteerder1\"", S1Sorteerder1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2Sorteerder1\"", S2Sorteerder1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3Sorteerder1\"", S3Sorteerder1.SensorONMemory);*/

        PowerAfvoer1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerAfvoer1\"");
        RichtingAfvoer1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingAfvoer1\"");
        SnelheidAfvoer1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidAfvoer1\"");
        /*writePLCInput("ns=3;s=\"I_S1Afvoer1\"", S1Afvoer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2Afvoer1\"", S2Afvoer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3Afvoer1\"", S3Afvoer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4Afvoer1\"", S4Afvoer1.SensorONMemory);*/

        PowerMidBuffer1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerMidBuffer1\"");
        RichtingMidBuffer1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingMidBuffer1\"");
        SnelheidMidBuffer1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidMidBuffer1\"");
        /*writePLCInput("ns=3;s=\"I_S1MidBuffer1\"", S1MidBuffer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2MidBuffer1\"", S2MidBuffer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3MidBuffer1\"", S3MidBuffer1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4MidBuffer1\"", S4MidBuffer1.SensorONMemory);*/

        PowerIDband1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerIDband1\"");
        RichtingIDband1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingIDband1\"");
        SnelheidIDband1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidIDband1\"");
        writePLCInput("ns=3;s=\"I_S1IDband1\"", S1IDband1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2IDband1\"", S2IDband1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3IDband1\"", S3IDband1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4IDband1\"", S4IDband1.SensorONMemory);

        PowerSorteerder2 = readPLCOutputBoolean("ns=3;s=\"Q_PowerSorteerder2\"");
        RichtingSorteerder2 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingSorteerder2\"");
        ZijrichtingSorteerder2 = readPLCOutputBoolean("ns=3;s=\"Q_ZijrichtingSorteerder2\"");
        SnelheidSorteerder2 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidSorteerder2\"");
        /*writePLCInput("ns=3;s=\"I_S1Sorteerder2\"", S1Sorteerder2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2Sorteerder2\"", S2Sorteerder2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3Sorteerder2\"", S3Sorteerder2.SensorONMemory);*/

        PowerOpslag1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerOpslag1\"");
        RichtingOpslag1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingOpslag1\"");
        SnelheidOpslag1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidOpslag1\"");
        /*writePLCInput("ns=3;s=\"I_S1Opslag1\"", S1Opslag1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2Opslag1\"", S2Opslag1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3Opslag1\"", S3Opslag1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4Opslag1\"", S4Opslag1.SensorONMemory);*/

        PowerLangeAfstand1 = readPLCOutputBoolean("ns=3;s=\"Q_PowerLangeAfstand1\"");
        RichtingLangeAfstand1 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingLangeAfstand1\"");
        SnelheidLangeAfstand1 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidLangeAfstand1\"");
        /*writePLCInput("ns=3;s=\"I_S1LangeAfstand1\"", S1LangeAfstand1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2LangeAfstand1\"", S2LangeAfstand1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3LangeAfstand1\"", S3LangeAfstand1.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4LangeAfstand1\"", S4LangeAfstand1.SensorONMemory);*/

        PowerLangeAfstand2 = readPLCOutputBoolean("ns=3;s=\"Q_PowerLangeAfstand2\"");
        RichtingLangeAfstand2 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingLangeAfstand2\"");
        SnelheidLangeAfstand2 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidLangeAfstand2\"");
        /*writePLCInput("ns=3;s=\"I_S1LangeAfstand2\"", S1LangeAfstand2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2LangeAfstand2\"", S2LangeAfstand2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3LangeAfstand2\"", S3LangeAfstand2.SensorONMemory);*/

        PowerLangeAfstand3 = readPLCOutputBoolean("ns=3;s=\"Q_PowerLangeAfstand3\"");
        RichtingLangeAfstand3 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingLangeAfstand3\"");
        SnelheidLangeAfstand3 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidLangeAfstand3\"");
        /*writePLCInput("ns=3;s=\"I_S1LangeAfstand3\"", S1LangeAfstand3.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2LangeAfstand3\"", S2LangeAfstand3.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3LangeAfstand3\"", S3LangeAfstand3.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4LangeAfstand3\"", S4LangeAfstand3.SensorONMemory);*/

        PowerLangeAfstand4 = readPLCOutputBoolean("ns=3;s=\"Q_PowerLangeAfstand4\"");
        RichtingLangeAfstand4 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingLangeAfstand4\"");
        SnelheidLangeAfstand4 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidLangeAfstand4\"");
        /*writePLCInput("ns=3;s=\"I_S1LangeAfstand4\"", S1LangeAfstand4.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2LangeAfstand4\"", S2LangeAfstand4.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3LangeAfstand4\"", S3LangeAfstand4.SensorONMemory);*/

        PowerLangeAfstand5 = readPLCOutputBoolean("ns=3;s=\"Q_PowerLangeAfstand5\"");
        RichtingLangeAfstand5 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingLangeAfstand5\"");
        SnelheidLangeAfstand5 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidLangeAfstand5\"");
        /*writePLCInput("ns=3;s=\"I_S1LangeAfstand5\"", S1LangeAfstand5.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2LangeAfstand5\"", S2LangeAfstand5.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3LangeAfstand5\"", S3LangeAfstand5.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4LangeAfstand5\"", S4LangeAfstand5.SensorONMemory);*/

        PowerLangeAfstand6 = readPLCOutputBoolean("ns=3;s=\"Q_PowerLangeAfstand6\"");
        RichtingLangeAfstand6 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingLangeAfstand6\"");
        SnelheidLangeAfstand6 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidLangeAfstand6\"");
        /*writePLCInput("ns=3;s=\"I_S1LangeAfstand6\"", S1LangeAfstand6.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2LangeAfstand6\"", S2LangeAfstand6.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3LangeAfstand6\"", S3LangeAfstand6.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4LangeAfstand6\"", S4LangeAfstand6.SensorONMemory);*/

        PowerAfvoer2 = readPLCOutputBoolean("ns=3;s=\"Q_PowerAfvoer2\"");
        RichtingAfvoer2 = readPLCOutputBoolean("ns=3;s=\"Q_RichtingAfvoer2\"");
        SnelheidAfvoer2 = readPLCOutputFloat("ns=3;s=\"Q_SnelheidAfvoer2\"");
        /*writePLCInput("ns=3;s=\"I_S1Afvoer2\"", S1Afvoer2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S2Afvoer2\"", S2Afvoer2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S3Afvoer2\"", S3Afvoer2.SensorONMemory);
        writePLCInput("ns=3;s=\"I_S4Afvoer2\"", S4Afvoer2.SensorONMemory);*/
    }
}
