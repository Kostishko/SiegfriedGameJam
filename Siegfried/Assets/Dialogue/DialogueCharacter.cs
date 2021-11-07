using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/DialogueCharacter", order = 1)]
public class DialogueCharacter : ScriptableObject
{
    public string characterName;
    public Sprite sprite;
}
