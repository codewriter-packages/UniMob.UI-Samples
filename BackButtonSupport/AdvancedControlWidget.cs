using System;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.BackButtonSupport
{
    using UniMob.UI;

    public class AdvancedControlWidget : StatefulWidget
    {
        public BackButtonController BackButtonController { get; }
        public Action OnClose { get; set; }

        public AdvancedControlWidget(BackButtonController backButtonController)
        {
            BackButtonController = backButtonController;
        }

        public override State CreateState() => new AdvancedControlState();
    }

    public class AdvancedControlState : HocState<AdvancedControlWidget>
    {
        private TabController _tabController;

        public override void InitState()
        {
            base.InitState();

            // Register back button callback
            Widget.BackButtonController.RegisterHandler(() => HandleBack());

            _tabController = new TabController(StateLifetime, 2, 0.2f);
            _tabController.AnimateTo(1);
        }

        public override Widget Build(BuildContext context)
        {
            return new Tabs(_tabController)
            {
                Children =
                {
                    new Container
                    {
                        BackgroundColor = Color.yellow,
                        Child = DemoUtils.Text("TAB 1", "Press ESC to return"),
                    },
                    new Container
                    {
                        BackgroundColor = Color.cyan,
                        Child = DemoUtils.Text("TAB 2", " Press ESC to scroll to first tab"),
                    },
                },
            };
        }

        private bool HandleBack()
        {
            if (_tabController.Index != 0)
            {
                // Scroll to the first tab
                _tabController.AnimateTo(0);
                return true;
            }

            // If we are on first tab, starts normal closing process
            Widget.OnClose?.Invoke();
            return true;
        }
    }
}