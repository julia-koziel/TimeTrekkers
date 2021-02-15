using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName="Custom/GameEvent/DataGameEvent")]
public class DataGameEvent : GameEvent<(List<string[]>, string)>
{
    public StringVariable participantId;
    public void Raise(List<string[]> data, string identifier)
    {
        Raise((data, identifier));
    }
}