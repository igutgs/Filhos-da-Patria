using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speedParallax;
    public Renderer parallax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parallax.material.mainTextureOffset += new Vector2(speedParallax * Time.deltaTime, 0f);
    }
}
