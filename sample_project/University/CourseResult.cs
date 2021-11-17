namespace University
{
    public class CourseResult
    {
       private string name;
       private int credits;
        private int grade;
       public CourseResult(string name, int credits, int grade) {
           this.name = name;
           this.credits = credits;
           this.grade = grade;
       }

       public int getResult() {
           return credits*grade;
       }
}
}
