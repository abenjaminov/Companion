using System;

namespace _Scripts.States
{
    public class Transition
    {
        public State From;
        public State To;
        public Func<bool> Predicate;
    }
}