using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CSharp;
using UnityEngine;

public class TextCompileForRuntime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        // var source = File.ReadAllText (pathToMyFile);
        var provider = new CSharpCodeProvider ();
        var param = new CompilerParameters ()
        {
            GenerateExecutable = false,
            GenerateInMemory = true
        };
        param.ReferencedAssemblies.Add ("System.dll");
        param.ReferencedAssemblies.Add ("UnityEngine.dll");

        var result = provider.CompileAssemblyFromSource (param, "");
    }

    // Update is called once per frame
    void Update ()
    {

    }
}