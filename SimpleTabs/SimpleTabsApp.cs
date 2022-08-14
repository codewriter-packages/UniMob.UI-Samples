using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.SimpleTabs
{
    public class SimpleTabsApp : UniMobUIApp
    {
        private TabController _tabController;

        protected override void Initialize()
        {
            base.Initialize();

            _tabController = new TabController(Lifetime, 5, 0.2f);
        }

        protected override Widget Build(BuildContext context)
        {
            return new VerticalSplitBox
            {
                FirstChild = new Tabs(_tabController)
                {
                    Children =
                    {
                        BuildTabBody(0),
                        BuildTabBody(1),
                        BuildTabBody(2),
                        BuildTabBody(3),
                        BuildTabBody(4),
                    },
                },
                SecondChild = new Row
                {
                    Children =
                    {
                        BuildTabIcon(0),
                        BuildTabIcon(1),
                        BuildTabIcon(2),
                        BuildTabIcon(3),
                        BuildTabIcon(4),
                    },
                },
            };
        }

        private Widget BuildTabBody(int tabIndex)
        {
            return new Container
            {
                Size = WidgetSize.Stretched,
                BackgroundColor = GetTabColor(tabIndex),
            };
        }

        private Widget BuildTabIcon(int tabIndex)
        {
            return new Container
            {
                BackgroundColor = Color.Lerp(Color.black, Color.white, Mathf.Abs(_tabController.Value - tabIndex) * 2f),
                Child = new UniMobButton
                {
                    Interactable = _tabController.Index != tabIndex,
                    OnClick = () => _tabController.AnimateTo(tabIndex),
                    Child = new UniMobText(WidgetSize.FixedHeight(120))
                    {
                        CrossAxisAlignment = CrossAxisAlignment.Center,
                        MainAxisAlignment = MainAxisAlignment.Center,
                        FontSize = 60,
                        Color = GetTabColor(tabIndex),
                        Value = tabIndex.ToString(),
                    },
                },
            };
        }

        private Color GetTabColor(int index)
        {
            return Color.Lerp(Color.green, Color.red, 1f * index / _tabController.TabCount);
        }
    }
}