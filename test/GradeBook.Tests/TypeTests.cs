using System;
using System.Net.NetworkInformation;
using GradeBook;
using Xunit;

namespace GradeBook.Tests {

    //Делегат как переменная, только для методов
    public delegate string WriteLogDelegate( string logMessage ); //важно возвращаемое значение и тип параметра(ов)

    public class TypeTests {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod() {
            //singleCast delegat
            WriteLogDelegate log = ReturnMessage; //создали переменную типа делегат в которую занесли ссылку на метод ReturnMessage
            log += ReturnCounter; // multicast delegate
            string result = log( "Hello!" ); //переменная LOG имеет тип делегата, а это ссылка на метод, который  мы можем вызвать
            Assert.Equal( 2, count ); //при вызове метода LOG(переменная типа мультикаст делегат) вызовутся сразу два метода, ссылки на которые в эту переменную занесены
        }

        string ReturnCounter( string message ) {
            count++;
            return message;
        }

        string ReturnMessage( string message ) {
            count++;
            return message;
        }

        [Fact]
        public void StringsAreImmutable() {
            string myName = "Tony"; //string is reference type but behave like value type
            string upper = myName.ToUpper(); // immutable. Cant change myName, ToUpper returns a copy instead of modifying myName 

            Assert.Equal( "Tony", myName );
            Assert.Equal( "TONY", upper );
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
            InMemoryBook book1 = GetBook( "Book 1" );
            GetBookSetName( ref book1, "TestName1" ); // here we pass parametr by reference

            Assert.Equal( "TestName1", book1.Name );
        }

        private void GetBookSetName( ref InMemoryBook inMemoryBook, string name ) { //ref modifier - tells compiler that parametr passes by reference not by value
            inMemoryBook = new InMemoryBook( name );
        }

        [Fact]
        public void CSharpTestByValue() {
            InMemoryBook book1 = GetBook( "Book 1" );
            GetBookSetName( book1, "TestName1" ); //parametr book1 is copied to method by value

            Assert.Equal( "Book 1", book1.Name );
        }

        private void GetBookSetName( InMemoryBook inMemoryBook, string name ) { // передается копия значения, которое является ссылкой, а не сама ссылка
            inMemoryBook = new InMemoryBook( name ); // просто меняем локальную переменную.
            inMemoryBook.Name = name; // не имеет эффекта так как меняет локально созданный объект в строчке выше
        }

        [Fact]
        public void CanSetNameFromReference() {
            InMemoryBook book1 = GetBook( "Book 1" );
            SetName( book1, "TestName1" ); //passing parametr by value

            Assert.Equal( "TestName1", book1.Name );
        }

        private void SetName( InMemoryBook inMemoryBook, string name ) { // Book - copy of value book1 that actualy is reference
            inMemoryBook.Name = name;
        }

        [Fact]
        public void BookCalculatesAnAverageGrade() {
            InMemoryBook book1 = GetBook( "Book 1" );
            InMemoryBook book2 = GetBook( "Book 2" );

            Assert.Equal( "Book 1", book1.Name );
            Assert.Equal( "Book 2", book2.Name );
            Assert.NotSame( book1, book2 );
        }

        [Fact]
        public void TwoVarsCanReferenceToTheSameObj() {
            InMemoryBook book1 = GetBook( "Book 1" );
            InMemoryBook book2 = book1;

            Assert.Same( book1, book2 );
            Assert.True( Object.ReferenceEquals( book1, book2 ) );
        }

        InMemoryBook GetBook( string name ) {
            return new InMemoryBook( name );
        }
    }

}
