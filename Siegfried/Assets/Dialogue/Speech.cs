using UnityEngine;

[CreateAssetMenu(fileName = "Speech", menuName = "ScriptableObjects/Speech", order = 1)]
public class Speech : ScriptableObject
{
    public Replica[] replicas;
    private int index;

    public Replica StartSpeech()
    {
        index = 0;
        return GetReplica();
    }
    public Replica Continue()
    {
        index++;
        return GetReplica();
    }

    private Replica GetReplica()
    {
        if (replicas.Length > index)
            return replicas[index];
        return null;
    }
}

[System.Serializable]
public class Replica
{
    [TextArea(2, 10)] public string text;
    public DialogueCharacter character;
}
