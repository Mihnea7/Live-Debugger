using System.Collections;

namespace University
{
    public class Professor
    {
        public string Name {get; set; }
        ArrayList students;

        public Professor(string name, ArrayList students) {
            Name = name;
            this.students = students;
        }

        public double getProfessorPerformance() {
            double gpa = 0;
            foreach (Student student in students)
            {
                gpa += student.getGPA();
            }
            return gpa/students.Count;
        }
    }
}