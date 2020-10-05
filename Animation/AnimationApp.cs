using System;
using UniMob;
using UniMob.UI;
using UniMob.UI.Widgets;
using UnityEngine;

namespace Samples.Animation
{
    public class AnimationApp : UniMobUIApp
    {
        public float duration = 1f;

        public FloatTween opacity = new FloatTween(0, 1);
        public Vector2Tween position = new Vector2Tween(Vector2.up, Vector2.down);

        private AnimationController _controller;

        protected override void Initialize()
        {
            _controller = new AnimationController(duration);
            _controller.Forward();

            Atom.Reaction(() => _controller.Status, OnControllerStatusChanged);
        }

        private void OnControllerStatusChanged(AnimationStatus status)
        {
            switch (status)
            {
                case AnimationStatus.Completed:
                    _controller.Reverse();
                    break;

                case AnimationStatus.Dismissed:
                    _controller.Forward();
                    break;
            }
        }

        protected override Widget Build(BuildContext context)
        {
            return new Container
            {
                Size = WidgetSize.Stretched,
                BackgroundColor = Color.white,
                Child = new CompositeTransition
                {
                    Opacity = opacity.Animate(_controller),
                    Position = position.Animate(_controller),
                    Child = new Container
                    {
                        BackgroundColor = Color.black,
                        Size = WidgetSize.Fixed(300, 200),
                    }
                }
            };
        }
    }
}