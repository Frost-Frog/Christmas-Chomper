using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public /*abstract*/ class ElfBehavior : MonoBehaviour
{
    public Elf elf {get; private set;}
    public float duration;
    void Awake()
    {
        elf = this.gameObject.GetComponent<Elf>();
        this.enabled = false;
    }

    public void Enable()
    {
        DurationEnable(this.duration);
    }
    public virtual void DurationEnable(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }
    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
        
    }
}
