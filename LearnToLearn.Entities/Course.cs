namespace LearnToLearn.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        private ICollection<Enrollment> enrollments;

        public Course()
        {
            enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string TeacherId { get; set; }

        public bool IsVisible { get; set; }

        public int Capacity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual User Teacher { get; set; }

        public virtual ICollection<Enrollment> Enrollments
        {
            get
            {
                return enrollments;
            }

            set
            {
                enrollments = value;
            }
        }
    }
}
