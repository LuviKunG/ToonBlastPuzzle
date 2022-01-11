namespace ToonBlastPuzzle
{
    public class GameContext
    {
        public PuzzleLevelData levelData;
        public PuzzleComboRuleBase comboRules;
        public PuzzleScoreCalculationBase scoreCalculation;
        public PuzzleGemRandomizer gemRandomizer;
        public PuzzleGemStyles gemStyle;

        public GameContext()
        {

        }
    }
}