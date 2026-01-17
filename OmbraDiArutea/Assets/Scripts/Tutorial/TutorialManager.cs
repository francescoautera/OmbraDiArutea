using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    
public class TutorialManager : MonoBehaviour
{
  public List<TutorialStep> _TutorialSteps = new List<TutorialStep>();
  private TutorialStep _currentTutorialStep;
  [SerializeField] private string nameScene = "Tutorial";
  private int index = 0;

  private void Start()
  {
    _currentTutorialStep = _TutorialSteps[index];
    _currentTutorialStep.InitTutorialStep();
    _currentTutorialStep.OnEndTutorialStep += OnChangeTutorial;
  }

  
  private void OnChangeTutorial()
  {
      index++;
      _currentTutorialStep.OnEndTutorialStep -= OnChangeTutorial;
      if (index >= _TutorialSteps.Count)
      {
          FindFirstObjectByType<LevelManager>().LoadHub(nameScene);
          return;
      }

      _currentTutorialStep = _TutorialSteps[index];
      _currentTutorialStep.InitTutorialStep();
      _currentTutorialStep.OnEndTutorialStep += OnChangeTutorial;
  }
}
}
