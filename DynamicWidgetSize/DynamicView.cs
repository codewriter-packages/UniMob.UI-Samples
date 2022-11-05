using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.DynamicWidgetSize
{
    public class DynamicView : View<DynamicState>
    {
        [SerializeField] private Button detailsButton;
        [SerializeField] private ViewPanel details;

        protected override void Awake()
        {
            base.Awake();

            detailsButton.Click(() => State.ToggleDetails);
        }

        protected override void Render()
        {
            details.Render(State.Details);
        }
    }
}