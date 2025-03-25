using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
        public float fadeTime =1.2f;

        private float timeElapsed = 0f;

        SpriteRenderer spriteRenderer;
        GameObject objToRemove;

Color startColor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed=0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timeElapsed += Time.deltaTime;

        float newAlpha =startColor.a*  (1- (timeElapsed/ fadeTime));

        spriteRenderer.color = new Color(startColor.r,startColor.g,startColor.b, newAlpha);

        if(timeElapsed > fadeTime){
            Destroy(objToRemove);
        }


       
    }

 
}
