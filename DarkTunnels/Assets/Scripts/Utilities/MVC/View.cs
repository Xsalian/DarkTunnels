using UnityEngine;

namespace DarkTunnels.Utilities.MVC
{
	public class View : MonoBehaviour, IView
	{
		protected Model<View> CurrentModel { get; set; }

		public void SetModel (Model<View> model)
		{
			CurrentModel = model;
		}

		protected T GetModel<T> () where T : Model<View>
		{
			return CurrentModel as T;
		}
	}
}