using PlayNANOO;
using PlayNANOO.CloudCode;
using UnityEngine;

public class ClodeServer : MonoBehaviour
{
    private void Start()
    {
        CloudSave();
    }

    private void CloudSave()
    {
        Plugin plugin = Plugin.GetInstance();
        plugin.SetPlayer("Bigpixel");
        var parameters = new CloudCodeExecution()
        {
            TableCode = "bigpixel966550157665-CS-B1651F7D-40000EB0",
            FunctionName = "makeHttp",
            FunctionArguments = new
            {
                q1 = "QueryString1",
                q2 = "QueryString2"
            }
        };

        plugin.CloudCodeExecution(parameters, (state, message, rawData, dictionary) =>
        {
            Debug.Log(dictionary["value"]);
        });
    }
}