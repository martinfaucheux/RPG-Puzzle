using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorData
{
    public string entityName { get; private set; }
    public string description { get; private set; }
    public Health health { get; private set; }
    public Attack attack { get; private set; }

    public InspectorData(string entityName, string description, Health health, Attack attack)
    {
        this.entityName = entityName;
        this.description = description;
        this.health = health;
        this.attack = attack;
    }
}
