using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.BaseClasses
{
    public class BaseCharacterAnimations : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        public void SetMoveMagnitude(float magnitude)
        {
            animator.SetFloat("moveMagnitude", magnitude);
        }
    }
}
