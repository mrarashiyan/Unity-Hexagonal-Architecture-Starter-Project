using System;
using System.Collections.Generic;
using Project.Core.Data.GameDesignData;

namespace Project.Presentation.UI.Screens.Home
{
    /// <summary>
    /// ViewModel for HomeScreen. Contains pure data, no Unity dependencies.
    /// </summary>
    public class HomeScreenViewModel
    {
        public int LevelCount { get; }
        public IReadOnlyList<LevelItemModel> Levels { get; }
        public bool IsAudioMute { get; }

        public HomeScreenViewModel(int levelCount, IReadOnlyList<LevelItemModel> levels, bool isAudioMute)
        {
            LevelCount = levelCount;
            Levels = levels ?? throw new ArgumentNullException(nameof(levels));
            IsAudioMute = isAudioMute;
        }
    }

    /// <summary>
    /// Model representing a single level item.
    /// </summary>
    public class LevelItemModel
    {
        public int LevelIndex { get; }
        public int SpriteIndex { get; }
        public bool IsLocked { get; }
        public int HighScore { get; }

        public LevelItemModel(int levelIndex, int spriteIndex, bool isLocked = false, int highScore = 0)
        {
            LevelIndex = levelIndex;
            SpriteIndex = spriteIndex;
            IsLocked = isLocked;
            HighScore = highScore;
        }
    }
}
