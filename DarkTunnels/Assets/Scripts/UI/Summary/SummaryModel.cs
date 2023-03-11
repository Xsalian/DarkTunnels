using DarkTunnels.GameManagment;
using DarkTunnels.Utilities.MVC;
using UnityEngine;

namespace DarkTunnels.UI
{
    public class SummaryModel : Model<SummaryView>
    {
        [field: SerializeField]
        private string TrainDestroyedText { get; set; }
        [field: SerializeField]
        private string StationReachedText { get; set; }

        protected virtual void Start ()
        {
            if (GameplayManager.Instance.PlayerWin == true)
            {
                CurrentView.DisplaySummaryText(StationReachedText);
            }
            else
            {
                CurrentView.DisplaySummaryText(TrainDestroyedText);
            }
        }
    }
}