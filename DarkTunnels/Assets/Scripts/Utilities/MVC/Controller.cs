using UnityEngine;

namespace DarkTunnels.Utilities.MVC
{
	public class Controller<TView, TModel> : MonoBehaviour where TView : View where TModel : Model<TView>
	{
		[field: SerializeField]
		protected TView CurrentView { get; private set; }
		[field: SerializeField]
		protected TModel CurrentModel { get; private set; }
	}
}