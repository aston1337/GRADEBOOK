using System;
using System.Collections.Generic;

namespace GradeBook {

    public delegate void GradeAddedDelegate( object sender, EventArgs args );

    public class InMemoryBook : Book {
        public InMemoryBook( string name ) : base( name ) { //constructor, can't have return type, same name as class
            //category = "Science";               //BASE(name) этот конструктор в классе namedobject так же должен быть сконструирован, как и данный.
            grades = new List<double>(); //Поэтому необходимо обязательно передать ему параметр либо пустую строку либо такой же параметр как и в данном конструкторе 

            this.Name = name; //this указывает на поле name в объекте, для которого вызывается
        }

        public void AddGrade( char letter ) { //method overloading by signature.
            switch ( letter ) { //The signature is Method name + params (return type is not a signature)  
                case 'A': //ref out in do not affect on overloading  
                    AddGrade( 90 );
                    break;
                case 'B':
                    AddGrade( 80 );
                    break;
                case 'C':
                    AddGrade( 70 );
                    break;
                case 'D':
                    AddGrade( 60 );
                    break;
                default:
                    AddGrade( 0 );
                    break;
            }
        }

        public override void AddGrade( double grade ) {
            if ( grade <= 100 && grade >= 0 ) {
                grades.Add( grade );

                if ( grade != null ) {
                    GradeAdded( this, new EventArgs() );
                }
            } else {
                throw new ArgumentException( $"Invalid {nameof(grade)}" ); // при неправильном вводе данных выбросит эксепшн, который мы должны поймать в коде, где был вызван этот метод
            }
        }

        public event GradeAddedDelegate GradeAdded;

        public Statistics GetStatistic() {
            Statistics result = new Statistics();
            result.Average = 0.0;
            result.High = Double.MinValue;
            result.Low = Double.MaxValue;

            for ( int index = 0; index < grades.Count; index++ ) {
                if ( grades[index] == 42.1 ) {
                    continue;
                }

                result.High = Math.Max( grades[index], result.High );
                result.Low = Math.Min( grades[index], result.Low );
                result.Average += grades[index];
            }

            result.Average /= grades.Count;

            switch ( result.Average ) {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }

        // если добавить static для AddGrade & field grades то сколько бы мы не создавали объектов типа Book
        // список будет всегда один и все оценки из разных объектов запишутся в один этот список
        private List<double> grades; //field definition, private by default
        //list is dynamic   

        //old way to define getter setter
        /*public string Name { 
            get {
                return name.ToUpper();
            }
            set {
                if ( !String.IsNullOrEmpty( value ) ) {
                    name = value;
                }
            }
        }*/

        //private readonly string category = "Science"; // readonly может инициализироваться только в конструкторе или при объявлении поля; 
        public const string CATEGORY = "Science"; // cant be changed anywhere, accessable everywhere
        //like a static member CATEGORY, can be accessed via CLASS, no via object
    }

}
