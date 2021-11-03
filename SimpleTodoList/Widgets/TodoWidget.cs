using Samples.SimpleTodoList.Views;
using UniMob;
using UniMob.UI;

namespace Samples.SimpleTodoList.Widgets
{
    public class TodoWidget : StatefulWidget
    {
        public TodoWidget(Todo todo)
        {
            Todo = todo;
        }

        public Todo Todo { get; }

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

        [Atom] public bool Completed
        {
            get => Widget.Todo.Finished;
            set => Widget.Todo.Finished = value;
        }

        public void Delete()
        {
            _todoList.RemoveTodo(Widget.Todo);
        }
    }
}