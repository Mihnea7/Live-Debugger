using System;
using Xunit;
using System.Collections;

namespace University.Tests
{
    public class ProfessorTests
    {
        [Fact]
        public void test_prof_GPA() 
        {
            var courses_1 = new ArrayList() {
                new CourseResult("CS1P", 20, 18),
                new CourseResult("CS1Q", 40, 20),
            };
            var courses_2 = new ArrayList() {
                new CourseResult("DB", 20, 17),
                new CourseResult("ML", 20, 15),
                new CourseResult("AI", 20, 22)
            };
            Student student_1 = new Student("Mihnea", "Maldaianu", 60, courses_1);
            Student student_2 = new Student("John", "Doe", 60, courses_2);
            Professor prof = new Professor("Bryan Jack", new ArrayList(){student_1, student_2});
            Assert.True(Math.Round(prof.getProfessorPerformance(),2) == 18.67, "Professor performance should be 18.67");
        }

        [Fact]
        public void test_prof_GPA_fail() 
        {
            var courses_1 = new ArrayList() {
                new CourseResult("CS1P", 20, 18),
                new CourseResult("CS1Q", 40, 20),
            };
            var courses_2 = new ArrayList() {
                new CourseResult("DB", 20, 17),
                new CourseResult("ML", 20, 15),
                new CourseResult("AI", 20, 22)
            };
            Student student_1 = new Student("Mihnea", "Maldaianu", 60, courses_1);
            Student student_2 = new Student("John", "Doe", 60, courses_2);
            Professor prof = new Professor("Bryan Jack", new ArrayList(){student_1, student_2});
            Assert.True(Math.Round(prof.getProfessorPerformance(),2) == 19, "Professor performance should be 18.66");
        }
        
    }
}