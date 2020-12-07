using UniMob.UI;
using UnityEngine;

namespace Samples.NestedViews
{
    public class NestedContainerView : View<INestedContainerState>
    {
        [SerializeField] private NestedView nestedView = default;

        protected override void Render()
        {
            nestedView.Render(State.NestedState);
        }
    }

    public interface INestedContainerState : IViewState
    {
        INestedState NestedState { get; }
    }
}