using System;
using System.Collections.Generic;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.BackButtonSupport
{
    public class AppWidget : StatefulWidget
    {
        public BackButtonController BackButtonController { get; }

        public AppWidget(BackButtonController backButtonController)
        {
            BackButtonController = backButtonController;
        }

        public override State CreateState() => new AppWidgetState();
    }

    public class AppWidgetState : HocState<AppWidget>
    {
        private readonly GlobalKey<NavigatorState> appNavigatorKey = new GlobalKey<NavigatorState>();

        private NavigatorState AppNavigator => appNavigatorKey.CurrentState;

        public override void InitState()
        {
            base.InitState();

            Widget.BackButtonController.RegisterHandler(StateLifetime, () => HandleBack());
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
                ShowQuitGameDialog();
            }

            return true;
        }

        private async void ShowQuitGameDialog()
        {
            var quit = await AppNavigator.Push<bool>(BuildQuitRoute());
            if (quit)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        private Route BuildMainRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("main", RouteModalType.Fullscreen),
                (context, controller, secondaryAnimation) => new Container
                {
                    Size = WidgetSize.Stretched,
                    BackgroundColor = Color.white,
                    Child = new Column
                    {
                        Children =
                        {
                            new UniMobButton
                            {
                                OnClick = () => AppNavigator.Push(BuildDetailRoute()),
                                Child = DemoUtils.Text(Color.yellow, "Open Details Page >"),
                            },
                            new UniMobButton
                            {
                                OnClick = () => AppNavigator.Push(BuildAdvancedControlRoute()),
                                Child = DemoUtils.Text(Color.yellow, "Open Complex Flow Page >"),
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
                    Size = WidgetSize.Stretched,
                    BackgroundColor = Color.gray,
                    Child = DemoUtils.Text(Color.white, "DETAIL PAGE", "Press ESC to return"),
                }
            ).WithPopOnBack(AppNavigator); // Basic: Automatically call 'Navigator.Pop()' on back button click
        }

        private Route BuildAdvancedControlRoute()
        {
            // Advanced: Pass back button control to widget
            return BackButtonController.Create(bbc => new PageRouteBuilder(
                new RouteSettings("advanced", RouteModalType.Fullscreen),
                (context, animation, secondaryAnimation) => new CompositeTransition
                {
                    Opacity = animation,
                    Child = new AdvancedControlWidget(bbc)
                    {
                        OnClose = () => AppNavigator.Pop(),
                    },
                },
                transitionDuration: 0.2f,
                reverseTransitionDuration: 0.2f
            ));
        }

        private Route BuildQuitRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("quit", RouteModalType.Fullscreen),
                (context, controller, secondaryAnimation) => new Container
                {
                    Size = WidgetSize.Stretched,
                    BackgroundColor = Color.gray,
                    Child = new Column
                    {
                        CrossAxisAlignment = CrossAxisAlignment.Center,
                        Children =
                        {
                            DemoUtils.Text(Color.white,
                                "QUIT POPUP",
                                "Do you really want to quit app?",
                                "Press ESC to return"),

                            new UniMobButton
                            {
                                Child = DemoUtils.Text(Color.red, "QUIT"),
                                OnClick = () => AppNavigator.Pop(true),
                            },
                        },
                    },
                }
            ).WithPopOnBack(AppNavigator, false);
        }
    }
}