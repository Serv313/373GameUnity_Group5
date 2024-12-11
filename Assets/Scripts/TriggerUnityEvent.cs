/**
 *@author: Weston R. Campbell
 *@organization: Outer Games Entertainment Inc.
 *@license: Free Use
 */

using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TriggerUnityEvent : MonoBehaviour
{
    public UnityEvent executeOnEnter;
    public UnityEvent executeOnExit;
    public string triggerTag = "Player";
    public bool triggerEnterOnlyOnce;
    public bool triggerExitOnlyOnce;

    private bool _enterTriggered;
    private bool _exitTriggered;

    [SerializeField] private TMP_Text openText;
    [SerializeField]private bool canOpen = false;

    private void Awake()
    {
        openText.enabled = false;
        canOpen = false;
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            executeOnEnter?.Invoke();
            _enterTriggered = true;
            openText.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerEnterOnlyOnce && _enterTriggered) return;

        if (!other.CompareTag(triggerTag)) return;
        
        //executeOnEnter?.Invoke();
        //_enterTriggered = true;

        if (other.gameObject.tag == "Player")
        {
            openText.enabled = true;
            canOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (triggerExitOnlyOnce && _exitTriggered) return;

        if (!other.CompareTag(triggerTag)) return;

        executeOnExit?.Invoke();
        _exitTriggered = true;

        if (other.gameObject.tag == "Player")
        {
            openText.enabled = false;
            canOpen = false;
        }
    }
}
