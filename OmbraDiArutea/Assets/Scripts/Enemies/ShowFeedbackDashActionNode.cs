using UnityEngine;

namespace OmbreDiAretua
{
    public class ShowFeedbackDashActionNode : ActionNode
    {
        private float timerShow = 2f;
        public GameObject feedbackDash;
        
        
        public override void Execute(Enemy enemy, Player player)
        {
            // Direzione A -> B
            Vector2 dir = player.transform.position - enemy.transform.position;
            // Distanza
            float distance = dir.magnitude;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bool isInverted = player.transform.position.x < enemy.transform.position.x;

            var feedback = Instantiate(feedbackDash, enemy.transform.position, Quaternion.identity);
            feedback.GetComponent<FeedbackDash>().Init(distance,angle,isInverted,timerShow);

        }

        public override void Stop()
        {
            
        }

        public override void Tick(Enemy enemy, Player player)
        {
        }
    }
}