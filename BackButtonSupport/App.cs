using System;
using System.Collections.Generic;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.BackButtonSupport
{
    public class App : StatefulWidget
    {
        public BackButtonController BackButtonController { get; }

        public App(BackButtonController backButtonController)
        {
            BackButtonController = backButtonController;
        }

        public override State CreateState() => new AppState();
    }

    public class AppState : HocState<App>
    {
        private readonly GlobalKey<NavigatorState> appNavigatorKey = new GlobalKey<NavigatorState>();

        private NavigatorState AppNavigator => appNavigatorKey.CurrentState;

        public override void InitState()
        {
            base.InitState();

            Widget.BackButtonController.RegisterHandler(() => HandleBack());
        }

        public override Widget Build(BuildContext context)
        {
            var routes = new Dictionary<string, Func<Route>> {["main"] = BuildMainRoute};
            return new Navigator("main", routes) {Key = appNavigatorKey,};
        }

        private bool HandleBack()
        {
            var handled = AppNavigator.HandleBack();
            if (!handled)
            {
                // Show quit app dialog
                AppNavigator.Push(BuildQuitRoute());
            }

            return true;
        }

        private Route BuildMainRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("main", RouteModalType.Fullscreen),
                (context, controller, secondaryAnimation) => new Container
                {
                    BackgroundColor = Color.white,
                    Child = new Column
                    {
                        Children =
                        {
                            new UniMobButton
                            {
                                OnClick = () => AppNavigator.Push(BuildDetailRoute()),
                                Child = DemoUtils.Text("Open Details Page >"),
                            },
                            new UniMobButton
                            {
                                OnClick = () => AppNavigator.Push(BuildAdvancedControlRoute()),
                                Child = DemoUtils.Text("Open Complex Flow Page >"),
                            },
                        },
                    },
                }
            );
        }

        private Route BuildDetailRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("detail", RouteModalType.Fullscreen),
                (context, controller, secondaryAnimation) => new Container
                {
                    BackgroundColor = Color.gray,
                    Child = DemoUtils.Text("DETAIL PAGE", "Press ESC to return"),
                }
            ).WithPopOnBack(AppNavigator); // Basic: Automatically call 'Navigator.Pop()' on back button click
        }

        private Route BuildAdvancedControlRoute()
        {
            // Advanced: Pass back button control to widget
            return BackButtonController.Create(bbc => new PageRouteBuilder(
                new RouteSettings("advanced", RouteModalType.Fullscreen),
                (context, controller, secondaryAnimation) => new AdvancedControlWidget(bbc)
                {
                    OnClose = () => AppNavigator.Pop(),
                }
            ));
        }

        private Route BuildQuitRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("quit", RouteModalType.Fullscreen),
                (context, controller, secondaryAnimation) => new Container
                {
                    BackgroundColor = Color.gray,
                    Child = DemoUtils.Text("QUIT POPUP", "Do you really want to quit app?", "Press ESC to return"),
                }
            ).WithPopOnBack(AppNavigator);
        }
    }
}