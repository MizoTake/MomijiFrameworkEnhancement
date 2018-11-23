using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblySwarm : MonoBehaviour
{

	[SerializeField]
	private GameObject _origin;

	private List<float> _mass = new List<float> ();
	private List<GameObject> _objects = new List<GameObject> ();
	private List<Vector2> _velocity = new List<Vector2> ();

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		for (var i = 0; i < _mass.Count; i++)
		{
			var accelerationX = 0f;
			var accelerationY = 0f;
			for (var j = 0; j < _mass.Count; j++)
			{
				if (i != j)
				{
					var distanceX = _objects[j].transform.position.x - _objects[i].transform.position.x;
					var distanceY = _objects[j].transform.position.y - _objects[i].transform.position.y;

					var distance = Mathf.Sqrt (distanceX * distanceX + distanceY * distanceY);
					if (distance < 1f) distance = 1f;

					var force = (distance - 32f) * _mass[j] / distance;
					accelerationX += force * distanceX;
					accelerationY += force * distanceY;
				}
			}

			_velocity[i] = new Vector2 (_velocity[i].x * 0.99f + accelerationX * _mass[i], _velocity[i].y * 0.99f + accelerationX * _mass[i]);
		}

		for (var i = 0; i < _objects.Count; i++)
		{
			_objects[i].transform.Translate (_velocity[i]);
		}

		if (Input.GetMouseButtonDown (0))
		{
			Instance ();
		}
	}

	private void Instance ()
	{
		var random = Random.Range (0.003f, 0.03f);
		_mass.Add (random);
		var pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f));
		GameObject newObj;
		if (_origin)
		{
			newObj = Instantiate (_origin);
		}
		else
		{
			newObj = GameObject.CreatePrimitive ((PrimitiveType) Random.Range (0, 6));
		}
		newObj.transform.SetPositionAndRotation (pos, Quaternion.identity);
		newObj.transform.localScale = Vector3.one * random * 10f;
		_velocity.Add (Vector2.zero);
		_objects.Add (newObj);
	}
}