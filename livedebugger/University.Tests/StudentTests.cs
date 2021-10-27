using System;
using Xunit;
using System.Collections;

namespace University.Tests
{
    public class StudentTests
    {
        [Fact]
        public void test_GPA()
        {
            var courses = new ArrayList() {
                new CourseResult("CS1P", 20, 18),
                new CourseResult("CS1Q", 40, 20),
            };
            Student student = new Student("Mihnea", "Maldaianu", 60, courses);
            Assert.True(Math.Round(student.getGPA(),2) == 19.33, "GPA should be 19");
        }
    }
}
