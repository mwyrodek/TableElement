using TableElement.Exception;
using Xunit;

namespace TableElementTests
{
    public class TableWithHeaderTests
    {
        private string[][] expectedBody =
        {
            new[] {"c0r0", "c1r0", "c2r0"},
            new[] {"c0r1", "c1r1", "c2r1"},
            new[] {"c0r2", "c1r2", "c2r2"}
        };
        
        private string[][] expectedBodyMergedCells =
        {
            new[] {"c0r0", "c1r0", "c2r0"},
            new[] {"c0r1", "c1r1"},
            new[] {"c0r2", "c1r2", "c2r2"}
        };
        
        private string[] expectedHeader = {"h1", "h2", "h3"};
        
        
        [Fact]
        public void CreateElement_NoHeader_ThrowsError()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            Assert.Throws<HeaderMissMatchException>(
                () =>  new TableElement.TableWithHeader(realTableNoHeader)
            );
        }
        
        [Fact]
        public void CreateElement_UnEvenTable_ThrowsError()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBodyMergedCells,expectedHeader);
            
            Assert.Throws<HeaderMissMatchException>(
                () =>  new TableElement.TableWithHeader(realTableNoHeader)
            );
        }

        [Fact]
        public void CreateElement_ValidInput()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableWithHeader(realTableNoHeader);

            Assert.Equal(2, tableElement.Rows.Count);
            Assert.Equal(6, tableElement.Cells.Count);
            Assert.Equal(3, tableElement.ColumnHeaders.Count);
        }
        
        
        //get collumn
        //get collumn HeaderNotFoundException
        [Fact]
        public void GetColumn_ActualHeader_ReturnsProperCells()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableWithHeader(realTableNoHeader);

            var column = tableElement.GetColumn("h3");
            Assert.Equal(column[0].Element.Text, expectedBody[1][2]);
            Assert.Equal(column[1].Element.Text, expectedBody[2][2]);
        }
        
        [Fact]
        public void GetColumn_WrongHeader_ThrowError()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableWithHeader(realTableNoHeader);

            Assert.Throws<HeaderNotFoundException>(
                ()=> tableElement.GetColumn("h6"));
        }       

        
        [Fact]
        public void GetCell_CorrectRowAndHeader_ReturnsProperCell()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableWithHeader(realTableNoHeader);
            
            var text = tableElement.GetCell("h3", 1).Text;
            Assert.Equal(expectedBody[2][2],text);
        }
        
        [Fact]
        public void GetCell_FakeHeader_ReturnsProperCell()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableWithHeader(realTableNoHeader);
            
            Assert.Throws<HeaderNotFoundException>(
                ()=> tableElement.GetCell("h6",1));
        }
    }
}