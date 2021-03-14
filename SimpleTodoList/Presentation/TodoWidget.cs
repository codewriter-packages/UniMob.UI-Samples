using UniMob;

namespace Samples.SimpleTodoList.Presentation
{
    using UniMob.UI;

    public class TodoWidget : StatefulWidget
    {
        public Todo Todo { get; }

        public TodoWidget(Todo todo)
        {
            Todo = todo;
        }

        public override State CreateState() => StateProvider.Of(this);
    }

    public class TodoState : ViewState<TodoWidget>, ITodoState
    {
        private readonly TodoList _todoList;

        public TodoState(WidgetViewReference view, TodoList todoList)
        {
            View = view;
            _todoList = todoList;
        }

        public override WidgetViewReference View { get; }

        [Atom] public string Text => Widget.Todo.Title;

        [Atom] public bool Active
        {
            get => !Widget.Todo.Finished;
            set => Widget.Todo.Finished = !value;
        }

        public void Delete()
        {
            _todoList.RemoveTodo(Widget.Todo);
        }
    }
}