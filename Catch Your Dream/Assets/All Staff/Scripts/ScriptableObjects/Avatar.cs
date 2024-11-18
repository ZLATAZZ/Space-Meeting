using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Avatar", menuName = "Avatar")]
public class Avatar : ScriptableObject
{
    public string name;
    public int cost;

    public Sprite avatarImage;
    public bool isActivated;
}
