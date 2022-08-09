namespace GradeBook {

    public interface IBook { //I - convention, all interfaces are started from "I"
        void AddGrade( double grade );
        Statistics GetStatistic();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

}
