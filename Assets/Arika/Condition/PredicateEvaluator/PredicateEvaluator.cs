using UnityEngine;

namespace Condition
{
    public abstract class PredicateEvaluator : ScriptableObject
    {
        protected virtual void Awake()
        {
            OnValidate();
        }

        protected virtual void OnValidate()
        {
        }

        public string EvaluatorName => GetType().ToString().Replace("PredicateEvaluator", "").Replace("Condition.", "");

        public abstract bool Evaluate(Predicate predicate);
    }
}