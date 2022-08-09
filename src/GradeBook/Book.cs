namespace GradeBook {

    public abstract class Book : NamedObject, IBook { // ":" - is //абстрактный класс, если мы не знаем чего хотим на данный момент от него
        protected Book( string name ) : base( name ) { // необходимо протягивать конструктор во всех классах которые наследуются, этот конструктор промежуточный
        }

        public abstract void AddGrade( double grade ); // пока мы не знаем что будет делать этот метод, поэтому тело отсутствует. В одном классе он может записывать в память все оценки, а в другом классе на диск в файл

        public virtual Statistics GetStatistic() {
            throw new System.NotImplementedException();
        }

        public virtual event GradeAddedDelegate GradeAdded;
    }

}
