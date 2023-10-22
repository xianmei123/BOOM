using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    [SerializeField]public List<string> animations;
    
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        // Debug.Log(skeletonAnimation.SkeletonDataAsset.GetAnimationStateData());

        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < animations.Count ; i++)
            {
                // i = i % animations.Count;
                if (animations[i].Length != 0)
                {
                    PlayAnimation(0, animations[i], 1);
                }
           
                
            }
        }
        
        
    }

    private void PlayAnimation(int i, string s, int times)
    {
        skeletonAnimation.AnimationState.AddAnimation(0, s, false, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
