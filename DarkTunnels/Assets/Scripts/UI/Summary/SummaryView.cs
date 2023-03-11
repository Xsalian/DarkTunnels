using DarkTunnels.Utilities.MVC;
using UnityEngine;
using TMPro;

namespace DarkTunnels.UI
{
    public class SummaryView : View
    {
        [field: SerializeField]
        private TextMeshProUGUI SummaryText { get; set; }

        public void DisplaySummaryText (string text)
        {
            SummaryText.text = text;
        }
    }
}