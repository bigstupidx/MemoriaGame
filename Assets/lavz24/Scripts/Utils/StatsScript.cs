///
/// StatsScript.cs
/// Copyright (c) 2014 DESSEC.com, Inc.
///

using UnityEngine;

namespace krellware.weatherzen.dev.utilities
{
	/// <summary>
	/// Shows some stats of the game.
	/// </summary>
	public class StatsScript : MonoBehaviour 
	{
		/// <summary>
		/// The update interval.
		/// </summary>
		public float UpdateInterval = 0.5f;

		/// <summary>
		/// The accumulated FPS.
		/// </summary>
		private float accumulated;

		/// <summary>
		/// Frames that passed by.
		/// </summary>
		private int frames;

		/// <summary>
		/// Time left for the next measurement.
		/// </summary>
		private float timeLeft;

		/// <summary>
		/// Label to show the info.
		/// </summary>
		private UILabel label;

		/// <summary>
		/// Toggles this component
		/// </summary>
		[SerializeField]
		private UIToggle toggle;

		/// <summary>
		/// Starts this instance.
		/// </summary>
		void Start()
		{
			timeLeft = UpdateInterval;
			accumulated = 0;
			frames = 0;
			label = GetComponent<UILabel>();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
				toggle.value = !toggle.value;

			timeLeft -= Time.deltaTime;
			accumulated += Time.timeScale / Time.deltaTime;
			++frames;

			if (timeLeft <= 0) 
			{
				float fps = accumulated / frames;
				timeLeft = UpdateInterval;
				accumulated = 0;
				frames = 0;

				label.text = System.String.Format("FPS: {0:F2}", fps);
			}
		}
	}
}
