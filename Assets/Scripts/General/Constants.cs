public static class Constants
{
    
    #region Player

    public class PlayerAnimations
    {
        // Player animations
        public const string IDLE = "Idle";
        public const string RUN = "Run";
        public const string JUMP = "Jump";
        public const string FALL_ENTRY = "Fall Entry";
        public const string DASH = "Dash";
        public const string SPRINT = "Sprint";
        public const string AIR_DASH = "Air Dash";
        public const string STUN_FROM_SPRINT = "Stun From Sprint";
        public const string WALL_GRAB = "Wall Grab";
        public const string WALL_SLIDE = "Wall Slide";
        public const string WALL_RUN = "Wall Run";
        public const string WALL_JUMP = "Wall Jump";
        public const string LONG_JUMP = "Long Jump";
        public const string LEDGE_CLIMB = "Ledge Climb";
        public const string EXTRA_JUMP = "Extra Jump";

        // Player animation transitions
        public const string LAND = "Land";
        public const string ROLLING_FALL = "Rolling Fall";
        public const string HOP_LAND = "Hop Land";
        public const string LAND_TO_RUN = "Land To Run";
        public const string IDLE_TO_RUN = "Idle To Run";
        public const string RUN_TO_IDLE = "Run To Idle";
        public const string DASH_TO_IDLE = "Dash To Idle";
        public const string DASH_TO_RUN = "Dash To Run";
        public const string MANTLE_TO_IDLE = "Mantle To Idle";
        public const string MANTLE_TO_RUN = "Mantle To Run";
        public const string RUNNING_TURN = "Running Turn";
        public const string SPRINTING_TURN = "Sprinting Turn";
        public const string UMBRELLA_INFLATE = "Umbrella Inflate";
        public const string UMBRELLA_TO_IDLE = "Umbrella To Idle";
        public const string UMBRELLA_TO_FALL = "Umbrella To Fall";
        public const string UMBRELLA_TURN = "Umbrella Turn";
    }

    #endregion

    #region Physics

    public class Physics
    {
        public const float MIN_FALL_VELOCITY = -0.2f;
    }

    #endregion

    #region Settings

    public class Settings
    {
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
    }

    #endregion
}
