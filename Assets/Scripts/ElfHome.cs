using System.Collections;
using UnityEngine;

public class ElfHome : ElfBehavior
{
    public Transform inHome;
    public Transform exit;

    void OnEnable()
    {
        StopAllCoroutines();
    }
    void OnDisable()
    {
        if(this.gameObject.activeSelf)
        {
            StartCoroutine(ExitHome());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            this.elf.movement.SetDirection(-this.elf.movement.direction);
        }
    }

    private IEnumerator ExitHome()
    {
        this.elf.movement.SetDirection(Vector2.up, true);
        this.elf.movement.rb.isKinematic = true;
        this.elf.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            Vector3 newposition = Vector3.Lerp(position, this.inHome.position, elapsed/duration);
            newposition.z = position.z;
            this.elf.transform.position = newposition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0.0f;
        while(elapsed < duration)
        {
            Vector3 newposition = Vector3.Lerp(this.inHome.position, this.exit.position, elapsed/duration);
            newposition.z = position.z;
            this.elf.transform.position = newposition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.elf.movement.SetDirection(new Vector2(Random.value > 0.5f ? 1.0f : -1.0f, 0.0f), true);
        this.elf.movement.rb.isKinematic = false;
        elf.movement.enabled = true;


    }
}