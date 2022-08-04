using System;
using System.Collections.Generic;

namespace GradeBook {

    public class Book {
        public Book( string name ) { //constructor, cant have return type, same name as class
            grades = new List<double>();
            this.Name = name; //this указывает на поле name в объекте, для которого вызывается
        }

        public void AddGradeLetter( char letter ) {
            switch ( letter ) {
                case 'A':
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
                    AddGrade(0);
                    break;
            }
        }

        public void AddGrade( double grade ) {
            if ( grade <= 100 && grade >= 0 ) {
                grades.Add( grade );
            } else {
                Console.WriteLine( "Invalid Value" );
            }
        }

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
                case var d when d >= 90.0 :
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0 :
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0 :
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0 :
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
        public string Name;
    }

}
