namespace LearnToLearn.Rest.Models
{
    using System;

    public class EnrollmentViewModel
    {
        public int Id { get; set; }

        public double Grade { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string StudentName { get; set; }

        public string CourseName { get; set; }
    }
}