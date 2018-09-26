using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace TygaSoft.WebHelper
{
    public class OpenXmlHelper
    {
        public static void Export(HttpContext context, DataTable dt)
        {
            using (var stream = new MemoryStream())
            {
                Export(stream, dt);
                context.Response.Buffer = true;
                context.Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
                context.Response.ContentType = "application/ms-excel";
                context.Response.BinaryWrite(stream.ToArray());
                context.Response.Flush();
            }
        }

        public static void Export(string filePath, DataTable dt)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                Export(filePath, dt);
            }
        }

        public static void Export(Stream stream, DataTable dt)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                Export(document, dt);
            }
        }

        private static void Export(SpreadsheetDocument document, DataTable dt)
        {
            var workbookpart = document.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());
            var sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
            var sheet = new Sheet()
            {
                Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Sheet1"
            };
            sheets.Append(sheet);
            DtToExcel(worksheetPart, dt);
            document.Close();
        }

        public static DataTable Import(string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
            {
                return ExcelToDt(document);
            }
        }

        public static DataTable Import(Stream stream)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false))
            {
                return ExcelToDt(document);
            }
        }

        private static void DtToExcel(WorksheetPart worksheetPart, DataTable dt)
        {
            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            var dcc = dt.Columns;
            var drc = dt.Rows;
            uint rowIndex = 1;
            var headRow = new Row() { RowIndex = rowIndex };
            var headCells = new List<Cell>();
            foreach (DataColumn item in dcc)
            {
                headCells.Add(new Cell() { CellValue = new CellValue(item.ColumnName), DataType = CellValues.String });
            }
            headRow.Append(headCells);
            sheetData.Append(headRow);
            foreach (DataRow dr in drc)
            {
                rowIndex++;
                var row = new Row() { RowIndex = rowIndex };
                var cells = new List<Cell>();
                for (var i = 0; i < headRow.Count(); i++)
                {
                    cells.Add(new Cell() { CellValue = new CellValue(dr[i].ToString()), DataType = CellValues.String });
                }
                row.Append(cells);
                sheetData.Append(row);
            }
            worksheetPart.Worksheet.Save();
        }

        private static DataTable ExcelToDt(SpreadsheetDocument document)
        {
            DataTable dt = new DataTable();
            var sheet = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().GetFirstChild<Sheet>();
            if (sheet == null) return null;

            var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id.Value);
            var rows = worksheetPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>();
            if (rows == null || rows.Count() == 0) return null;
            var headNames = new List<string>();
            var headValues = new List<string>();
            GetHeaderCell(document, rows.First().Elements<Cell>(),ref headNames,ref headValues);
            if (headValues.Count == 0) return null;
            foreach (var item in headValues)
            {
                dt.Columns.Add(new DataColumn(item.Trim('*'), typeof(System.String)));
            }
            rows.First().Remove();
            if (rows.Count() > 10000) throw new ArgumentException("数据量太大，请分批上传，一次上传不超过5000条为最佳！");
            var tasks = new List<Task>();
            foreach (var row in rows)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var items = GetCellValues(document, row.Elements<Cell>(), headNames);
                    if (items.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        for (var i = 0; i < headValues.Count; i++)
                        {
                            dr[i] = items[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }));
                if (tasks != null && tasks.Count() > 5)
                {
                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }
            }
            if(tasks.Count() > 0) Task.WaitAll(tasks.ToArray());

            return dt;
            
        }

        private static void GetHeaderCell(SpreadsheetDocument document, IEnumerable<Cell> cells, ref List<string> headNames,ref List<string> headValues)
        {
            foreach (var cell in cells)
            {
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    var shareStringPart = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    var items = shareStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                    headValues.Add(items[int.Parse(cell.CellValue.Text)].InnerText.Trim(new char[] { '\''}).Trim());
                    headNames.Add(GetCellChar(cell.CellReference.Value));
                }
                else
                {
                    if (cell.CellValue != null)
                    {
                        headValues.Add(cell.CellValue.Text.Trim(new char[] { '\'' }));
                        headNames.Add(GetCellChar(cell.CellReference.Value));
                    }
                }
            }
        }

        private static List<string> GetCellValues(SpreadsheetDocument document, IEnumerable<Cell> cells,List<string> headNames)
        {
            var list = new List<string>();
            foreach(var item in headNames)
            {
                var cell = cells.FirstOrDefault(c => c.CellReference.Value.StartsWith(item));
                if(cell == null) list.Add("");
                else
                {
                    if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                    {
                        var shareStringPart = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                        var items = shareStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                        list.Add(items[int.Parse(cell.CellValue.Text)].InnerText.Trim());
                    }
                    else
                    {
                        if (cell.CellValue != null) list.Add(cell.CellValue.Text.Trim());
                        else list.Add("");
                    }
                }
            }
            return list;
        }

        private int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        private Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        private Char GetColumnName(int index)
        {
            var az = new List<Char>(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray());
            return az[index];
        }

        private uint GetRowIndex(string cellName)
        {
            Match match = Regex.Match(cellName, @"\d+");
            return uint.Parse(match.Value);
        }

        private static string GetCellChar(string cellRef)
        {
            return Regex.Replace(cellRef, @"\d+", "");
        }
    }
}
