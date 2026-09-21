namespace Code.Services.Tutorial
{
    public enum TutorialPointerSide
    {
        Left,
        Right
    }

    public static class TutorialPointerSideExtensions
    {
        public static float ToDirection(this TutorialPointerSide side) =>
            side == TutorialPointerSide.Left ? -1f : 1f;
    }
}