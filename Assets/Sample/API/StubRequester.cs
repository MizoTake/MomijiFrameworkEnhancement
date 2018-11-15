using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class StubRequester : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		var request = new StubPutRequest ();

		request.Put
			.Subscribe (_ =>
			{
				Debug.Log (_);
			})
			.AddTo (this);

		request.Dispatch (new StubPutParameter ("message"));
	}

	// Update is called once per frame
	void Update ()
	{

	}
}