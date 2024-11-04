using System;
using UnityEngine;

[Serializable]

public class PowerupInfo :MonoBehaviour
{
    public PowerupType p_type;

    public PowerupType GetPowerupType()
    {
        return p_type;
    }
}

public enum PowerupType
{
    Multiplier,
    Shield,
    Speed
}

