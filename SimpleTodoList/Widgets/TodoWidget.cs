using System;
using Samples.SimpleTodoList.Views;
using UniMob;
using UniMob.UI;

namespace Samples.SimpleTodoList.Widgets
{
    public class TodoWidget : StatefulWidget
    {
        public TodoWidget(Guid todoId)
        {
            TodoId = todoId;
        }

        public Guid TodoId { get; }
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

        [Atom] private Todo Todo => _todoList.GetTodoById(Widget.TodoId);

        [Atom] public string Text => Todo.Title;

        [Atom] public bool Completed
        {
            get => Todo.Finished;
            set => Todo.Finished = value;
        }

        public void Delete()
        {
            _todoList.RemoveTodoById(Todo.Id);
        }
    }
}