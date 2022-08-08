using System;
using System.Collections.Generic;

/* Modifiers
 * public - code outside of class have access to this method/field/member
   private - Field is accessable only inside of a class  
   static - method is not connected to object (see example in Book class) and associated with type
 */
namespace GradeBook {

    class MyBook {
        public static void Main( string[] args ) { //Method Main entry point, string[] - type of parametr(string array), args - name of parametr
            /*
            MyBook staticMain = new MyBook(); 
            staticMain.Main( args ); из-за статик мы не можем получить доступ к методам функции мэин, только через класс
            */

            Book book = new Book( "Aston's book" );
            //book.AddGrade(89.1); //вызов метода для объекта типа Book из класса Book
            book.GradeAdded += OnGradeAdded;

            do {
                Console.Write( "Enter grade: " );
                string input = Console.ReadLine();

                if ( input == "q" ) {
                    break;
                }

                try {
                    double gradeInput = double.Parse( input );
                    book.AddGrade( gradeInput );
                } catch ( ArgumentException e ) {
                    Console.WriteLine( e.Message );
                } catch ( FormatException e ) {
                    Console.WriteLine( e.Message );
                } finally {
                    Console.WriteLine( "****Exeption is handled****" );
                }
            } while ( true );

            //Book.AddGrade( 12.4 ); // Обращение через класс, а не через объект
            //book.grades.Add(84.3  ); // обращение напрямую к полю grades в классе Book
            Statistics stats = book.GetStatistic(); //incapsulation - hide complexity
            Console.WriteLine( book.Name.ToUpper() );
            Console.WriteLine( Book.CATEGORY ); // we have access to read, but not to overwrite
            Console.WriteLine( $"The average grade is {stats.Average:N1}\nThe highest grade is {stats.High}\nThe lowes grade is {stats.Low}\nThe mark grade is {stats.Letter}" ); //N1 - shorts the double to 1 digit after comma 20.1
        }

        static void OnGradeAdded( object sender, EventArgs args ) {
            Console.WriteLine( "A grade was added" );
        }

    }

}
