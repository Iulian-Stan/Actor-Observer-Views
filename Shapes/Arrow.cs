namespace Example.Shapes
{
    public readonly struct Arrow
    {
        public readonly float TailHeight;
        public readonly float TailRadius;
        public readonly float PeakHeight;
        public readonly float PeakRadius;
        public readonly int Sectors;

        public Arrow(float tailHeight, float tailRadius, float peakHeight, float peakRadius, int sectors)
        {
            TailHeight = tailHeight;
            TailRadius = tailRadius;
            PeakHeight = peakHeight;
            PeakRadius = peakRadius;
            Sectors = sectors;
        }
    }
}
