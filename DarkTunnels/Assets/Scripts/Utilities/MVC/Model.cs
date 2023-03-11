using UnityEngine;

namespace DarkTunnels.Utilities.MVC
{
	public class Model<TView> : MonoBehaviour where TView : View
	{
		[field: SerializeField]
		protected TView CurrentView { get; private set; }
	}
}