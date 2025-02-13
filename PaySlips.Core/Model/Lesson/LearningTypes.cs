namespace PaySlips.Core.Model.Lesson
{
    public sealed class LearningTypes
    {
        public string Name { get; }

        private LearningTypes(string name)
        {
            Name = name;
        }

        public static readonly LearningTypes FullTime = new LearningTypes("FullTime");
        public static readonly LearningTypes Сorrespondence = new LearningTypes("Сorrespondence");

        public static readonly List<LearningTypes> AllTypes = new List<LearningTypes>
        {
            FullTime, Сorrespondence
        };
    }
}
