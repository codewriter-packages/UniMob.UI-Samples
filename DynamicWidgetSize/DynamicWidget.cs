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
        private const string Text =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. ";

        private readonly string detailText;
        private readonly StateHolder detailsState;

        public DynamicState(WidgetViewReference view)
        {
            View = view;

            detailsState = CreateChild(BuildItems);
            detailText = Text.Substring(0, Random.Range(0, Text.Length));
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
                SecondChild = new PaddingBox(new RectPadding(20, 20, 20, 20))
                {
                    Child = new UniMobText
                    {
                        Value = detailText,
                        Color = Color.white,
                        FontSize = 30,
                        MaxCrossAxisExtent = 600,
                    },
                },
            };
        }
    }
}