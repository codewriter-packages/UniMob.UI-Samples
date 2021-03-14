using UniMob.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.SimpleTodoList.Presentation
{
    public class TodoView : View<ITodoState>
    {
        [SerializeField] private Text todoText = default;
        [SerializeField] private Toggle completedToggle = default;
        [SerializeField] private Button deleteButton = default;

        [SerializeField] private Color activeTextColor = Color.black;
        [SerializeField] private Color completedTextColor = Color.gray;

        protected override void Awake()
        {
            base.Awake();

            deleteButton.Click(() => State.Delete);
            completedToggle.onValueChanged.AddListener(completed => State.Active = !completed);
        }

        protected override void Render()
        {
            todoText.color = State.Active ? activeTextColor : completedTextColor;
            todoText.text = State.Text;

            completedToggle.isOn = !State.Active;
        }
    }

    public interface ITodoState : IViewState
    {
        string Text { get; }
        bool Active { get; set; }

        void Delete();
    }
}