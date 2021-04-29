using System.Data.Entity;

using WhatProject.Data.Entities;

namespace WhatProject.Data
{
    public class ApplicationContext : DbContext
    {        
        public ApplicationContext(string connection) : base(connection) 
        { 
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttachmentOfHomework> AttachmentOfHomeworks { get; set; }
        public DbSet<AttachmentOfHomeworkStudent> AttachmentOfHomeworkStudents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EventOccurrence> EventOccurrences { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkStudent> HomeworkStudents { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<MentorOfCourse> MentorOfCourses { get; set; }
        public DbSet<MentorOfStudentGroup> MentorOfStudentGroups { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvents { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentOfStudentGroup> StudentOfStudentGroups { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
