using System;
using System.Collections.Generic;
using System.Linq;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Samples.Navigation
{
    public class DismissibleDialogApp : UniMobUIApp
    {
        protected override Widget Build(BuildContext context)
        {
            return new Navigator("main", new Dictionary<string, Func<Route>>
            {
                ["main"] = BuildMainRoute,
                ["dialog"] = BuildDialogRoute,
            });
        }

        private Route BuildMainRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("main", RouteModalType.Fullscreen),
                pageBuilder: (context, primaryAnimation, secondaryAnimation) => new Container
                {
                    BackgroundColor = Color.green,
                    Child = new UniMobButton
                    {
                        Child = new UniMobText(WidgetSize.Fixed(600, 80))
                        {
                            FontSize = 40,
                            MainAxisAlignment = MainAxisAlignment.Center,
                            CrossAxisAlignment = CrossAxisAlignment.Center,
                            Value = "Show Dialog",
                        },
                        OnClick = () => Navigator.PushNamed(context, "dialog"),
                    },
                }
            );
        }

        private Route BuildDialogRoute()
        {
            return new PageRouteBuilder(
                new RouteSettings("dialog", RouteModalType.Popup),
                pageBuilder: (context, primaryAnimation, secondaryAnimation) => new DismissibleDialog
                {
                    CollapsedHeight = 800,
                    Child = BuildDialogContent(),
                    OnDismiss = () => Navigator.Pop(context),
                },
                transitionsBuilder: BuildDialogTransitions,
                transitionDuration: 0.2f,
                reverseTransitionDuration: 0.2f
            );
        }

        private Widget BuildDialogContent()
        {
            return new ScrollList
            {
                Children =
                {
                    new Container
                    {
                        BackgroundColor = Color.white,
                        Child = new UniMobText(WidgetSize.FixedHeight(140))
                        {
                            FontSize = 40,
                            MainAxisAlignment = MainAxisAlignment.Center,
                            CrossAxisAlignment = CrossAxisAlignment.Center,
                            Value = "Drag up to expand, drag down to close",
                        },
                    },
                    Enumerable.Range(0, 20).Select(_ => BuildDialogListItem()),
                },
            };

            Widget BuildDialogListItem()
            {
                return new UniMobButton
                {
                    OnClick = () => Debug.Log("CLICK!"),
                    Child = new Container
                    {
                        Size = WidgetSize.FixedHeight(200),
                        BackgroundColor = Color.Lerp(Color.red, Color.green, Random.value),
                    },
                };
            }
        }

        private Widget BuildDialogTransitions(BuildContext context, AnimationController primaryAnimation,
            AnimationController secondaryAnimation, Widget child)
        {
            return new CompositeTransition
            {
                Position = primaryAnimation.Drive(new Vector2Tween(Vector2.down, Vector2.zero)),
                Child = child,
            };
        }
    }
}