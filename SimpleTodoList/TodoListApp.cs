using Samples.SimpleTodoList.Presentation;
using UniMob.UI;
using UnityEngine;

namespace Samples.SimpleTodoList
{
    public class TodoListApp : UniMobUIApp
    {
        [SerializeField] private GameObject todoListViewPrefab = default;
        [SerializeField] private GameObject todoViewPrefab = default;

        private readonly TodoList _todoList = new TodoList();

        protected override void Initialize()
        {
            _todoList.AddTodo("Get Coffee");
            _todoList.AddTodo("Write simpler code");
            _todoList.Todos[0].Finished = true;

            StateProvider.Register<TodoListWidget>(() => new TodoListState(
                view: WidgetViewReference.FromPrefab(todoListViewPrefab),
                todoList: _todoList
            ));

            StateProvider.Register<TodoWidget>(() => new TodoState(
                view: WidgetViewReference.FromPrefab(todoViewPrefab),
                todoList: _todoList
            ));
        }

        protected override Widget Build(BuildContext context)
        {
            return new TodoListWidget();
        }
    }
}