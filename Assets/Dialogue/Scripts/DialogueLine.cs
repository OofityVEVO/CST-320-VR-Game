using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Line")]
public class DialogueLine : ScriptableObject
{
    public sourceName character;    
    public AudioClip line;
    public float timeAfterSound;
}
