using System.Collections.Generic;
using UnityEngine;

namespace Condition
{
    public sealed class ConditionEvaluator : Singleton<ConditionEvaluator>
    {
        [SerializeField] private List<PredicateEvaluator> evaluators = new List<PredicateEvaluator>();
        private readonly Dictionary<string, PredicateEvaluator> _evaluatorsDict =
            new Dictionary<string, PredicateEvaluator>();
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            ValidateEvaluatorList();

            void ValidateEvaluatorList()
            {
                if (UnityEditor.EditorApplication.isUpdating) return;

                evaluators.Clear();
                var predicateEvaluators = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(PredicateEvaluator)}");
                foreach (var guid in predicateEvaluators)
                {
                    var evaluator =
                        UnityEditor.AssetDatabase.LoadAssetAtPath<PredicateEvaluator>(
                            UnityEditor.AssetDatabase.GUIDToAssetPath(guid));
                    evaluators.Add(evaluator);
                }
            }
#endif
            _evaluatorsDict.Clear();
            foreach (var evaluator in evaluators)
            {
                _evaluatorsDict.Add(evaluator.EvaluatorName, evaluator);
            }
        }


        private void OnEnable()
        {
            OnValidate();
        }

        public bool Evaluate(Condition condition)
        {
            return EvaluateAnd(condition.And);
        }

        private bool EvaluateAnd(Disjunction[] and)
        {
            foreach (var dis in and)
            {
                if (!EvaluateOr(dis.Or)) return false;
            }

            return true;
        }

        private bool EvaluateOr(Predicate[] or)
        {
            foreach (var predicate in or)
            {
                if (EvaluatePredicate(predicate)) return true;
            }

            return false;
        }

        private bool EvaluatePredicate(Predicate predicate)
        {
            if (string.IsNullOrEmpty(predicate.PredicateName))
            {
                Debug.LogError("Predicate name is empty");
                return true;
            }

            if (_evaluatorsDict.TryGetValue(predicate.PredicateName, out var evaluator))
            {
                bool result = evaluator.Evaluate(predicate);
                if (predicate.Negate)
                    result = !result;
                Debug.Log($"EvaluatePredicate {predicate.PredicateName} negate {predicate.Negate} result {result}");
                return result;
            }

            Debug.LogError("No evaluator for predicate " + predicate.PredicateName);
            return true;
        }

        // private void PrintAllPredicateEvaluator()
        // {
        //     Debug.LogWarning("PrintAllPredicateEvaluator in list");
        //     foreach (var evaluator in evaluators)
        //     {
        //         Debug.Log(evaluator.EvaluatorName);
        //     }
        //
        //     Debug.LogWarning("PrintAllPredicateEvaluator in dict");
        //     foreach (var evaluator in _evaluatorsDict)
        //     {
        //         Debug.Log(evaluator.Key);
        //     }
        // }
    }
}