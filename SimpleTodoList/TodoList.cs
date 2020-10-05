using System.Linq;
using UniMob;

namespace Samples.SimpleTodoList
{
    public class TodoList
    {
        [Atom] public Todo[] Todos { get; set; } = new Todo[0];

        [Atom] public int UnfinishedTodoCount => Todos.Count(t => !t.Finished);
    }
}