using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    private bool isFocus;
    private Transform player;
    private bool hasInteracted;

    private void Awake()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnFocus(Transform player)
    {
        this.player = player;
        isFocus = true;
        hasInteracted = false;
    }

    public void OnDefocus()
    {
        this.player = null;
        isFocus = false;
        hasInteracted = false;
    }

    protected virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }
}
