using UniMob;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.DynamicWidgetSize
{
    public class DynamicWidget : StatefulWidget
    {
    }

    public class DynamicState : ViewState<DynamicWidget>
    {
        private readonly float detailSize = Mathf.Lerp(200, 600, Random.value);
        private readonly Color detailColor = Color.Lerp(Color.green, Color.red, Random.value);

        private readonly StateHolder detailsState;

        public DynamicState(WidgetViewReference view)
        {
            View = view;

            detailsState = CreateChild(BuildItems);
        }

        [Atom] private bool DetailsVisible { get; set; }
        public override WidgetViewReference View { get; }
        public IState Details => detailsState.Value;

        public override WidgetSize CalculateSize()
        {
            var baseSize = base.CalculateSize();
            var detailsSize = detailsState.Value.Size;

            return WidgetSize.StackY(baseSize, detailsSize);
        }

        public void ToggleDetails()
        {
            DetailsVisible = !DetailsVisible;
        }

        private Widget BuildItems(BuildContext context)
        {
            return new AnimatedCrossFade
            {
                CrossFadeState = DetailsVisible ? CrossFadeState.ShowSecond : CrossFadeState.ShowFirst,
                Duration = 0.2f,
                FirstChild = new Empty(),
                SecondChild = new Container
                {
                    BackgroundColor = detailColor,
                    Size = WidgetSize.Fixed(detailSize, detailSize),
                },
            };
        }
    }
}