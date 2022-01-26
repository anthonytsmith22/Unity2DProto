using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public Ability PrimaryAbility;
    public Ability SecondaryAbility;
    public Ability MovementAbility;
    public Ability TechAbility;

    private void Awake(){
        if(PrimaryAbility == null){
            PrimaryAbility = new Ability(); // Switch with default primary later
        }else if(PrimaryAbility.Type != Ability.AbilityType.Primary){
            Debug.LogError("Specified primary ability is not a primary ability!");
        }

        if(SecondaryAbility == null){
            PrimaryAbility = new Ability(); 
        }else if(SecondaryAbility.Type != Ability.AbilityType.Secondary){
            Debug.LogError("Specified secondary ability is not a secondary ability!");
        }

        if(MovementAbility == null){
            MovementAbility = new DashAbility();
        }else if(MovementAbility.Type != Ability.AbilityType.Mobility){
            Debug.LogError("Specified mobility ability is not a mobility ability!");
        }

        if(TechAbility == null){
            TechAbility = new Ability();
        }else if(TechAbility.Type != Ability.AbilityType.Tech){
            Debug.LogError("Specified tech ability is not a tech ability!");
        }
    }
}
