using UnityEngine;

public class RecordController : MonoBehaviour
{
    public static float record= 10000;
    public static bool isFirstTime = true;

    
    public static void SaveRecord(float newRecord)
    {
        PlayerPrefs.SetFloat("Record", newRecord);
        PlayerPrefs.Save();
        record = newRecord;
    }

    public static void FlagFirstTime()
    {
        PlayerPrefs.SetInt("isFirstTime", 0);
        PlayerPrefs.Save();
        isFirstTime = false;
    }
}
