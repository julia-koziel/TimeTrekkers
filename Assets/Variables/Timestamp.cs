using UnityEngine;
// using EasyButtons;

[CreateAssetMenu(menuName = "Custom/Variable/Timestamp")]
public class Timestamp : Variable
{
    public string format = "HH:mm:ss";

    public override string ToString()
    {
        return System.DateTime.Now.ToString(format);
    }

    [ContextMenu("Debug with current time")]
    public void TestDebug()
    {
        Debug.Log(this.ToString());
    }

}