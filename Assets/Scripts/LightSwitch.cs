using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private Light lighting;
    [SerializeField] private AudioSource lightClick;

    private void Awake()
    {
        //emissiveMat.DisableKeyword("_EMISSION");
        lighting.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lighting.enabled = !lighting.enabled;
            lightClick.Play();
            /*
            if (lighting.enabled)
            {
                emissiveMat.EnableKeyword("_EMISSION");
            }
            else
            {
                emissiveMat.DisableKeyword("_EMISSION");
            }
            */
        }
    }
}
