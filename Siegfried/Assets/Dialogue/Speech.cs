using UnityEngine;

[CreateAssetMenu(fileName = "Speech", menuName = "ScriptableObjects/Speech", order = 1)]
public class Speech : ScriptableObject
{
    [TextArea(2, 10)] public string[] replicas;
    private int index;

    public string StartSpeech()
    {
        index = 0;
        return GetReplica();
    }
    public string Continue()
    {
        index++;
        return GetReplica();
    }

    private string GetReplica()
    {
        if (replicas.Length > index)
            return replicas[index];
        return "";
    }
}
