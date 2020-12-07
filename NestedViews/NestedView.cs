using UniMob.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.NestedViews
{
    public class NestedView : View<INestedState>
    {
        [SerializeField] private Text nestedText = default;

        protected override void Render()
        {
            nestedText.text = State.Message;
        }
    }

    public interface INestedState : IViewState
    {
        string Message { get; }
    }
}