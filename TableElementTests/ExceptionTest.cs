using System;
using TableElement.Exception;
using Xunit;

namespace TableElementTests
{
    /// <summary>
    /// This is  class is an attempt to check if there is even a point in Testing assertion classes
    /// </summary>
    public class ExceptionTest
    {

        [Fact]
        public void TableNotFound_DefaultException_ProperError()
        {
            Assert.Throws<TableNotFoundException>(
                    ()=>ExceptionMock.ThrowTableNotFound()
            );
        }
        
        [Fact]
        public void TableNotFound_CustomMessage_ProperError()
        {
            var expectedMessege = "this was throwned";
            var exception = Assert.Throws<TableNotFoundException>(
                ()=>ExceptionMock.ThrowTableNotFound(expectedMessege)
            );
            
            Assert.Equal(expectedMessege,exception.Message);
        }
        
        [Fact]
        public void TableNotFound_CustomMessageAndExcepiton_ProperError()
        {
            var argumentOutOfRangeException = new ArgumentOutOfRangeException();
            var expectedMessege = "this was throwned";
            var exception = Assert.Throws<TableNotFoundException>(
                ()=>ExceptionMock.ThrowTableNotFound(expectedMessege,argumentOutOfRangeException)
            );
            
            Assert.Equal(expectedMessege,exception.Message);
        }
        
        [Fact]
        public void ThrowHeaderMissMatch_DefaultException_ProperError()
        {
            Assert.Throws<HeaderMissMatchException>(
                ()=>ExceptionMock.ThrowHeaderMissMatch()
            );
        }
        
        [Fact]
        public void ThrowHeaderMissMatch_CustomMessage_ProperError()
        {
            var expectedMessege = "this was throwned";
            var exception = Assert.Throws<HeaderMissMatchException>(
                ()=>ExceptionMock.ThrowHeaderMissMatch(expectedMessege)
            );
            
            Assert.Equal(expectedMessege,exception.Message);
        }
        
        [Fact]
        public void ThrowHeaderMissMatch_CustomMessageAndExcepiton_ProperError()
        {
            var argumentOutOfRangeException = new ArgumentOutOfRangeException();
            var expectedMessege = "this was throwned";
            var exception = Assert.Throws<HeaderMissMatchException>(
                ()=>ExceptionMock.ThrowHeaderMissMatch(expectedMessege,argumentOutOfRangeException)
            );
            
            Assert.Equal(expectedMessege,exception.Message);
        }
        
        [Fact]
        public void ThrowHeaderNotFound_DefaultException_ProperError()
        {
            Assert.Throws<HeaderNotFoundException>(
                ()=>ExceptionMock.ThrowHeaderNotFound()
            );
        }
        
        [Fact]
        public void ThrowHeaderNotFound_CustomMessage_ProperError()
        {
            var expectedMessege = "this was throwned";
            var exception = Assert.Throws<HeaderNotFoundException>(
                ()=>ExceptionMock.ThrowHeaderNotFound(expectedMessege)
            );
            
            Assert.Equal(expectedMessege,exception.Message);
        }
        
        [Fact]
        public void ThrowHeaderNotFound_CustomMessageAndExcepiton_ProperError()
        {
            var argumentOutOfRangeException = new ArgumentOutOfRangeException();
            var expectedMessege = "this was throwned";
            var exception = Assert.Throws<HeaderNotFoundException>(
                ()=>ExceptionMock.ThrowHeaderNotFound(expectedMessege,argumentOutOfRangeException)
            );
            
            Assert.Equal(expectedMessege,exception.Message);
        }
    }

    static class ExceptionMock
    {
        public static void ThrowTableNotFound()
        {
            throw new TableNotFoundException();
        }

        public static void ThrowTableNotFound(string expectedMessege)
        {
            throw new TableNotFoundException(expectedMessege);
        }

        public static void ThrowTableNotFound(string expectedMessege, Exception exception)
        {
            throw new TableNotFoundException(expectedMessege,exception);
        }
        
        public static void ThrowHeaderMissMatch()
        {
            throw new HeaderMissMatchException();
        }

        public static void ThrowHeaderMissMatch(string expectedMessege)
        {
            throw new HeaderMissMatchException(expectedMessege);
        }

        public static void ThrowHeaderMissMatch(string expectedMessege, Exception exception)
        {
            throw new HeaderMissMatchException(expectedMessege,exception);
        }
        
        public static void ThrowHeaderNotFound()
        {
            throw new HeaderNotFoundException();
        }

        public static void ThrowHeaderNotFound(string expectedMessege)
        {
            throw new HeaderNotFoundException(expectedMessege);
        }

        public static void ThrowHeaderNotFound(string expectedMessege, Exception exception)
        {
            throw new HeaderNotFoundException(expectedMessege,exception);
        }
    }
}