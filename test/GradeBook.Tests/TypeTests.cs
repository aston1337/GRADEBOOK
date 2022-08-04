using System;
using System.Net.NetworkInformation;
using GradeBook;
using Xunit;

namespace GradeBook.Tests {

    public class TypeTests {
        [Fact] 
        public void StringsAreImmutable() {
            string myName = "Tony"; //string is reference type but behave like value type
            string upper = myName.ToUpper(); // immutable. Cant change myName, ToUpper returns a copy instead of modifying myName 
            
            Assert.Equal("Tony", myName);
            Assert.Equal("TONY", upper);

        }
        [Fact]
        public void ValueTypeAlsoPassByValue() {
            int x = GetInt();
            SetIntOut( out x ); // аут параметр передается как чистая ссылка без значения переменной, поэтому в методе необходимо ей присвоить значение
            SetIntRef( ref x ); // реф передается как ссылка + значение, поэтому обязательное присваивание в методе ненужно
            Assert.Equal( 30, x );
        }

        private void SetIntOut( out int outNum ) {
            outNum = 10; // обязательное присваивание значения так как значение переменной, которая передается в этот метод нет
            outNum += 42;
        }

        private void SetIntRef( ref int refNum ) {
            refNum = 30; //оперируем напрямую с переменной x из метода ValueTypeAlsoPassByValue
        }

        private int GetInt() {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef() {
            Book book1 = GetBook( "Book 1" );
            GetBookSetName( ref book1, "TestName1" ); // here we pass parametr by reference

            Assert.Equal( "TestName1", book1.Name );
        }

        private void GetBookSetName( ref Book book, string name ) { //ref modifier - tells compiler that parametr passes by reference not by value
            book = new Book( name );
        }

        [Fact]
        public void CSharpTestByValue() {
            Book book1 = GetBook( "Book 1" );
            GetBookSetName( book1, "TestName1" ); //parametr book1 is copied to method by value

            Assert.Equal( "Book 1", book1.Name );
        }

        private void GetBookSetName( Book book, string name ) { // передается копия значения, которое является ссылкой, а не сама ссылка
            book = new Book( name ); // просто меняем локальную переменную.
            book.Name = name; // не имеет эффекта так как меняет локально созданный объект в строчке выше
        }

        [Fact]
        public void CanSetNameFromReference() {
            Book book1 = GetBook( "Book 1" );
            SetName( book1, "TestName1" ); //passing parametr by value

            Assert.Equal( "TestName1", book1.Name );
        }

        private void SetName( Book book, string name ) { // Book - copy of value book1 that actualy is reference
            book.Name = name;
        }

        [Fact]
        public void BookCalculatesAnAverageGrade() {
            Book book1 = GetBook( "Book 1" );
            Book book2 = GetBook( "Book 2" );

            Assert.Equal( "Book 1", book1.Name );
            Assert.Equal( "Book 2", book2.Name );
            Assert.NotSame( book1, book2 );
        }

        [Fact]
        public void TwoVarsCanReferenceToTheSameObj() {
            Book book1 = GetBook( "Book 1" );
            Book book2 = book1;

            Assert.Same( book1, book2 );
            Assert.True( Object.ReferenceEquals( book1, book2 ) );
        }

        Book GetBook( string name ) {
            return new Book( name );
        }
    }

}
