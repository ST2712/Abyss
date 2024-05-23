using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatModifierHealthSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Health health = character.GetComponent<Health>();
        Debug.Log(health.health);
        if (health != null)
        {
            health.health += (int)val;
        }
    }
}
