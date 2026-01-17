namespace OmbreDiAretua
{
    public class DefaultTutorialStep : TutorialStep
    {
        public DialogueData DialogueData;
    
        public override void InitTutorialStep()
        {
            var enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
            DialogueData.RequestStartDialogue();
        }

        public override void UnlcokStep()
        {
            OnEndTutorialStep?.Invoke(); 
        }
    }
}