using System.Collections;
using System.Collections.Generic;
using Momiji;
using UnityEngine;

public class StubPutParameter : IParameterizable
{

	public string message;

	public StubPutParameter (string message)
	{
		this.message = message;
	}
}