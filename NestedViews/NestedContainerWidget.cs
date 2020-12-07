using UniMob.UI;

namespace Samples.NestedViews
{
    public class NestedContainerWidget : StatefulWidget
    {
        public override State CreateState() => StateProvider.Of(this);
    }

    public class NestedContainerState : ViewState<NestedContainerWidget>, INestedContainerState
    {
        private readonly StateHolder<INestedState> _nestedState;

        public NestedContainerState(WidgetViewReference view)
        {
            View = view;

            _nestedState = CreateChild<NestedWidget, NestedState>(BuildNested);
        }

        public override WidgetViewReference View { get; }

        public INestedState NestedState => _nestedState.Value;

        private NestedWidget BuildNested(BuildContext context)
        {
            return new NestedWidget
            {
                Message = "Hello from nested widget"
            };
        }
    }
}