using System;
using Castle.Core.Internal;
using TableElement.Exception;
using Xunit;

namespace TableElementTests
{
    public class TableElementTests
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
        public void CreateElement_ValidInput_NoHeader()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            Assert.Equal(3, tableElement.Rows.Count);
            Assert.Equal(9, tableElement.Cells.Count);
            Assert.Equal(0, tableElement.ColumnHeaders.Count);
        }

        [Fact]
        public void CreateElement_ValidInputUnEvenTable_NoHeader()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBodyMergedCells);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            Assert.Equal(3, tableElement.Rows.Count);
            Assert.Equal(8, tableElement.Cells.Count);
            Assert.Equal(0, tableElement.ColumnHeaders.Count);
        }

        [Fact]
        public void CreateElement_ValidInput_Headers()
        {
            var realTableWithHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableElement(realTableWithHeader);

            Assert.Equal(2, tableElement.Rows.Count);
            Assert.Equal(6, tableElement.Cells.Count);
            Assert.Equal(3, tableElement.ColumnHeaders.Count);
        }

        [Fact]
        public void CreateElement_InvalidInput_ThrowsException()
        {
            var notATable = new MockedTable().CreateNotATable();

            Assert.Throws<TableNotFoundException>(() => new TableElement.TableElement(notATable));
        }

        [Fact]
        public void GetCell_NoHeaderProperCell()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var actualCellText = tableElement.GetCell(0, 1).Text;
            var expectedCellText = expectedBody[1][0];
            Assert.Equal(expectedCellText, actualCellText);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(5, 1)]
        [InlineData(5, 5)]
        public void GetCell_OutOfBound_Returnsnull(int column, int row)
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var cell = tableElement.GetCell(column, row);

            Assert.Null(cell);
        }

        [Fact]
        public void GetHeaderNames_NoHearders_ReturnsEmptyList()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var headerNames = tableElement.GetHeaderNames();

            Assert.True(headerNames.IsNullOrEmpty());
        }

        [Fact]
        public void GetHeaderNames_FoundHearders_ReturnsProperNames()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var headerNames = tableElement.GetHeaderNames();

            Assert.Equal(headerNames, expectedHeader);
        }

        [Fact]
        public void GetRows_TableWithHeader_ReturnsProperRow()
        {
            var realTableNoHeader = new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var row = tableElement.GetRow(0);
            var elementText = row.GetCell(0).Element.Text;
            Assert.Equal(elementText, expectedBody[1][0]);
        }

        [Fact]
        public void GetRows_TableWithNoHeader_ReturnsProperRow()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var row = tableElement.GetRow(0);
            var elementText = row.GetCell(0).Element.Text;
            Assert.Equal(elementText, expectedBody[0][0]);
        }

        [Fact]
        public void GetCell_TableWithNoHeader_ReturnsProperCell()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var text = tableElement.GetCell(1, 2).Text;
            Assert.Equal(text, expectedBody[2][1]);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(5, 1)]
        [InlineData(5, 5)]
        public void GetCell_TableWithNoHeaderCellOutOfBound_ReturnsNull(int column, int row)
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);
            var webElement = tableElement.GetCell(column, row);
            Assert.Null(webElement);
        }

        [Fact]
        public void GetCell_TableWithHeader_ReturnsProperCell()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var text = tableElement.GetCell(1, 2).Text;
            Assert.Equal(text, expectedBody[2][1]);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(5, 1)]
        [InlineData(5, 5)]
        public void GetCell_TableWithHeaderCellOutOfBound_ReturnsNull(int column, int row)
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var webElement = tableElement.GetCell(column, row);
            Assert.Null(webElement);
        }

        [Fact]
        public void GetColumn_EvenTable_ReturnsProperCells()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var column = tableElement.GetColumn(2);
            Assert.Equal(column[0].Element.Text, expectedBody[0][2]);
            Assert.Equal(column[1].Element.Text, expectedBody[1][2]);
            Assert.Equal(column[2].Element.Text, expectedBody[2][2]);
        }

        [Fact]
        public void GetColumn_UnEvenTable_ReturnsProperOnUnEvenRow()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBodyMergedCells);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var column = tableElement.GetColumn(2);

            Assert.Equal(column[0].Element.Text, expectedBody[0][2]);
            Assert.Null(column[1]);
            Assert.Equal(column[2].Element.Text, expectedBody[2][2]);
        }
    }
}