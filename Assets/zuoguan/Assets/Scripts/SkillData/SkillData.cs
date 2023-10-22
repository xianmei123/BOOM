using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillData: PersistentSingleton1<SkillData>
{

    public bool SprintSkill = false;
    public bool FlySprintSkill = false;
    public bool ClimbSkill = false;
    public bool AttackSkill = false;

    public int index = 1;
    
    public List<int> IDs = new List<int>();
}