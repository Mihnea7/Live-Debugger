using System.Collections;
namespace University
{
    public class Student
    {
        private string firstName;
        private string lastName;
         private ArrayList courses;
         private int creditsTaken;
         private int gpa = 0;

         public Student(string firstName, string lastName, int creditsTaken, ArrayList  courses) {
             this.firstName = firstName;
             this.lastName = lastName;
             this.creditsTaken = creditsTaken;
             this.courses = courses;
         }

         public double getGPA() {
             foreach (CourseResult  course in courses)
             {
                 gpa += course.getResult();
             }
             return (double)gpa/creditsTaken;
         }
    }
}