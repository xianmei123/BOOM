using System;
using System.Collections.Generic;
using UnityEngine;

namespace Condition
{
    [Serializable]
    public class Condition
    {
        [SerializeField] private Disjunction[] and;
        public Disjunction[] And => and;
    }

    [Serializable]
    public class Disjunction
    {
        [SerializeField] private Predicate[] or;
        public Predicate[] Or => or;
    }

    [Serializable]
    public class Predicate
    {
        [SerializeField] private string predicate;
        public string PredicateName => predicate;
        [SerializeField] private string[] args;
        public IReadOnlyList<string> Args => args;
        [SerializeField] private bool negate;
        public bool Negate => negate;
    }
}