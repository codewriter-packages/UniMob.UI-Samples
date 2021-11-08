using UniMob;

namespace Samples.SimpleTodoList
{
    public class Todo : ILifetimeScope
    {
        public Todo(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public Lifetime Lifetime { get; }

        [Atom] public string Title { get; set; } = "";
        [Atom] public bool Finished { get; set; } = false;
    }
}