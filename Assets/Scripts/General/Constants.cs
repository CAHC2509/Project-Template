public static class Constants
{
    #region Player

    public class Player
    {
        // Player movement
        public const float MIN_FALL_VELOCITY = -0.5f;

        // Player animations
        public const string IDLE_ANIMATION = "Idle";
        public const string RUN_ANIMATION = "Run";
        public const string JUMP_ANIMATION = "Jump";
        public const string FALL_ANIMATION = "Fall";
        public const string WALL_SLIDE_ANIMATION = "Wall Slide";
        public const string DASH_ANIMATION = "Dash";
        public const string SPRINT_ANIMATION = "Sprint";
        public const string LONG_JUMP_ANIMATION = "Long Jump";
        public const string LEDGE_CLIMB_ANIMATION = "Ledge Climb";
    }

    #endregion

    #region Settings

    // Audio settings
    public const string AUDIO_SETTINGS_KEY = "AudioSettings";
    public const string GENERAL_VOLUME_KEY = "GeneralVolume";
    public const string MUSIC_VOLUME_KEY = "MusicVolume";
    public const string EFFECTS_VOLUME_KEY = "EffectsVolume";
    public const string UI_VOLUME_KEY = "UIVolume";
    public const float VOLUME_CONSTANT = 0.01f;

    // Graphics settings
    public const string GRAPHICS_SETTINGS_KEY = "GraphicsSettings";
    public const string RESOLUTIONS_KEY = "Resolution";
    public const string QUALITY_LEVELS_KEY = "QualityLevel";
    public const string FULL_SCREEN_KEY = "FullScreen";
    public const string SETTINGS_TABLE_REFERENCE = "Settings Menu";
    public const string FULLSCREEN_MODES_ENTRY_REFERENCE = "settings.fullScreen.";
    public const string QUALITIES_ENTRY_REFERENCE = "settings.qualities.";

    // Input settings
    public const string INPUT_ACTIONS_KEY = "InputActions";

    // Language settings
    public const string LANGUAGE_KEY = "LanguageSelected";

    #endregion
}
