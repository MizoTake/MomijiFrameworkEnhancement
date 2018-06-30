using System.Collections;
using System.Collections.Generic;
using Momiji.Sample;
using UniRx;
using UnityEngine;

public class Requester : MonoBehaviour
{

	void Start ()
	{
		var param = new SampleParamter (city: 130010);
		var request = new SampleGetRequest ();

		request.Get (param)
			.Subscribe (_ =>
			{
				Debug.Log (_.title);
			})
			.AddTo (this);
	}
}