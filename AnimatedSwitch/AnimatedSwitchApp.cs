using UniMob;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.AnimatedSwitch
{
    public class AnimatedSwitchApp : UniMobUIApp
    {
        [Atom] private int CurrentPage { get; set; }

        protected override Widget Build(BuildContext context)
        {
            return new Column
            {
                CrossAxisAlignment = CrossAxisAlignment.Center,
                Children =
                {
                    new UniMobText
                    {
                        Value = $"Page {CurrentPage}",
                        FontSize = 50,
                    },

                    new Container
                    {
                        Size = WidgetSize.Fixed(600, 600),
                        BackgroundColor = Color.white,
                        Child = new AnimatedSwitcher
                        {
                            Child = BuildCurrentPage(),
                            Duration = 0.25f,
                            ReverseDuration = 0.15f,
                            TransitionMode = AnimatedSwitcherTransitionMode.Parallel,
                            TransitionBuilder = CustomTransitionBuilder,
                        },
                    },

                    BuildPagination(),
                },
            };
        }

        private Widget BuildCurrentPage()
        {
            switch (CurrentPage)
            {
                case 1: return BuildPage1();
                case 2: return BuildPage2();
                case 3: return BuildPage3();
                default: return null;
            }
        }

        private Widget BuildPage1()
        {
            return new Container
            {
                Key = Key.Of("page 1"),
                Size = WidgetSize.Stretched,
                BackgroundColor = Color.red,
            };
        }

        private Widget BuildPage2()
        {
            return new Container
            {
                Key = Key.Of("page 2"),
                Size = WidgetSize.Stretched,
                BackgroundColor = Color.green,
            };
        }

        private Widget BuildPage3()
        {
            return new Container
            {
                Key = Key.Of("page 3"),
                Size = WidgetSize.Stretched,
                BackgroundColor = Color.blue,
            };
        }

        private Widget BuildPagination()
        {
            return new PaddingBox(new RectPadding(10, 10, 10, 10))
            {
                Child = new Container
                {
                    BackgroundColor = Color.green,
                    Child = new UniMobButton
                    {
                        OnClick = () => CurrentPage = (CurrentPage + 1) % 4,
                        Child = new UniMobText
                        {
                            Value = $"Next Page",
                            FontSize = 50,
                        },
                    },
                },
            };
        }

        private static Widget CustomTransitionBuilder(IAnimation<float> animation, Widget child)
        {
            if (animation.Status == AnimationStatus.Reverse)
            {
                return new CompositeTransition
                {
                    Scale = animation.Drive(new Vector3Tween(new Vector3(0, 1, 1), Vector3.one)),
                    Opacity = animation,
                    Child = child,
                };
            }

            return new CompositeTransition
            {
                Position = animation.Drive(new Vector2Tween(new Vector2(0.0f, -0.2f), Vector2.zero)),
                Opacity = animation,
                Child = child,
            };
        }
    }
}