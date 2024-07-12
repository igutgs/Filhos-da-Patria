using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSomPaUSE : MonoBehaviour
{
    [SerializeField] private AudioSource fundoMusical;
    public void VolumeMusical(float value)
    {
        fundoMusical.volume = value;
    }
}
