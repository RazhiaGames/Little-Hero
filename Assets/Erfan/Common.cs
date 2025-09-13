using System;

public class Common
{
    public enum ArrangingGameItemType
    {
        Sport,
        Math,
        Art,
        Geography,
        Karagah,
        Food,
        Tools,
        Animal,
        Bed,
        Cloth,
        Human,
        Wall,
        None
        
    }
    
    
    public enum ChooseSimilarGameItemType
    {
        School,
        Hospital,
        AmusementPark,
        Sports,
        Nature,
        Animals,
        Etc,
        Food,
        Tools,
    }
    
    
    public enum GameLocation
    {
        School,
        Hospital,
        AmusementPark
    }

    public enum Difficulty
    {
        Easy, Medium, Hard
    }
    
    public enum Location { School, Hospital, AmusementPark }

    public class LevelFinishData
    {
        public int RightCount;
        public int WrongCount;
        public int TimeCount;
        public GameWinState gameWinState;
        public int checkButtonCount;
        public string gameName;

        public LevelFinishData(int rightCount, int wrongCount, int timeCount, 
            GameWinState mGameWinState, int mCheckButtonCount, string mGameName)
        {
            RightCount = rightCount;
            WrongCount = wrongCount;
            TimeCount = timeCount;
            gameWinState = mGameWinState;
            checkButtonCount = mCheckButtonCount;
            gameName = mGameName;
        }
    }
    
    public enum NumbersGameItemType
    {
        even,
        odd,
        bird,
        wildAnimals,
        nonWildAnimals,
        bed,
        clothes,
        None
    }
    
    public enum GameWinState
    {
        Neutral,
        Win,
        Loose,
    }
    
    
    public enum GameType
    {
        ArrangingGame,
        FindDifferenceGame,
        ChooseSimillar,
        FindPath,
        Typo,
        Scale,
        ChooseNumbers,
        FindFriend
    }
    [Serializable]
    public struct GameTypeConfigPair
    {
        public GameType gameType;
        public LevelConfig config;
    }
    

}
