using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Movement
{
    public bool attacking;
    public int combo;
    public void OnPunch()
    {
        if (!attacking)
            Combo();
    }
    public void Combo()
    {
        attacking = true;
        Animator.SetTrigger("Punch" + combo);
        //audio
    }
    public void StartCombo()
    {
        attacking = false;
        if(combo < Data.punchCombo)
        {
            combo++;
        }
    }
    public void FinishCombo()
    {
        attacking = false;
        combo = 0;
    }
}
