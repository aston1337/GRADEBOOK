using System;
using System.Net.NetworkInformation;
using GradeBook;
using Xunit;

namespace GradeBook.Tests {

    public class BookTests {
        //TODO
        /*[Fact]
        public void CorrectGradeInput() {
            Book book = new Book( "" );
            book.AddGrade( 105 );
            var el = book.grades   
            Assert.Equal( , book.grades[0] );
        }*/

        [Fact]
        public void BookCalculatesAnAverageGrade() {
            //arrange
            Book book = new Book( "" );

            book.AddGrade( 89.1 );
            book.AddGrade( 90.1 );
            book.AddGrade( 73.3 );

            //act
            Statistics result = book.GetStatistic();

            //assert
            Assert.Equal( 84.2, result.Average, 1 );
            Assert.Equal( 90.1, result.High, 1 );
            Assert.Equal( 73.3, result.Low, 1 );
            Assert.Equal( 'B', result.Letter );
        }
    }

}
