using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTNWIC.Utils
{
    public class ShaderManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Shader.WarmupAllShaders();
        }
    }
}
