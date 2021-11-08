using Samples.SimpleTodoList.Widgets;
using UniMob.UI;
using UnityEngine;

namespace Samples.SimpleTodoList
{
    public class TodoListApp : UniMobUIApp
    {
        [SerializeField] private GameObject todoListViewPrefab = default;
        [SerializeField] private GameObject todoViewPrefab = default;

        private TodoList _todoList;

        protected override void Initialize()
        {
            _todoList = new TodoList(Lifetime);
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