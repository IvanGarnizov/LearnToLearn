namespace LearnToLearn.Entities
{
    using System;

    public class Enrollment
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        public int CourseId { get; set; }

        public double Grade { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual User User { get; set; }

        public virtual Course Course { get; set; }
    }
}
