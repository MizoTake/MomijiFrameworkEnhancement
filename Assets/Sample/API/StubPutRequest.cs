using System;
using System.Collections;
using System.Collections.Generic;
using Momiji;
using Momiji.Sample;
using UnityEngine;

interface IStubPutRequestable
{
	IObservable<StubPutResponse> Put { get; }
}

public class StubPutRequest : PutRequestable<StubPutParameter, StubPutResponse>, IStubPutRequestable
{
	public StubPutRequest ()
	{
		HostName = "https://us-central1-test-878c7.cloudfunctions.net/hello";
	}
}