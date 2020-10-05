using System;
using System.Collections.Generic;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.Navigation
{
    public class NavigationApp : UniMobUIApp
    {
        protected override Widget Build(BuildContext context)
        {
            return new Navigator("main", new Dictionary<string, Func<Route>>
            {
                ["main"] = BuildMainRoute,
                ["detail"] = BuildDetailRoute,
            });
        }

        private Route BuildMainRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("name", RouteModalType.Fullscreen),
                transitionDuration: 0.2f,
                reverseTransitionDuration: 0.2f,
                pageBuilder: (context, animation, secondaryAnimation) => new MainWidget
                {
                    ShowDetail = () => Navigator.PushNamed(context, "detail"),
                },
                transitionsBuilder: BuildSlideTransitions
            );
        }

        private Route BuildDetailRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("detail", RouteModalType.Fullscreen),
                transitionDuration: 0.2f,
                reverseTransitionDuration: 0.2f,
                pageBuilder: (context, animation, secondaryAnimation) => new DetailWidget
                {
                    Close = () => Navigator.Pop(context),
                },
                transitionsBuilder: BuildSlideTransitions
            );
        }

        private Widget BuildSlideTransitions(BuildContext context, AnimationController animation,
            AnimationController secondaryAnimation, Widget child)
        {
            return new CompositeTransition
            {
                Position = secondaryAnimation.Drive(new Vector2Tween(Vector2.left, Vector2.zero)),
                Child = new CompositeTransition
                {
                    Position = animation.Drive(new Vector2Tween(Vector2.right, Vector2.zero)),
                    Child = child,
                }
            };
        }
    }
}