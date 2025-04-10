using System;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace KADR
{
    public partial class MainForm : Window
    {
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            List<JornalKard> jkList = CRUDHelper.ReadAll<JornalKard>("JornalKard");
            if (jkList != null)
            {
                jkList = jkList.Where(p => p.NumDoc == txtBoxNumDoc.Text).ToList();
                if (jkList.Count > 0)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog
                    {
                        Filter = "Excel файлы (*.xlsx)|*.xlsx",
                        FileName = "JornalKard_Export.xlsx"
                    };
                    if (saveDialog.ShowDialog() == true)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("JornalKard");

                            worksheet.Cell("A1").Value = "Имя";
                            worksheet.Cell("B1").Value = "ФИО";
                            worksheet.Cell("C1").Value = "Должность";
                            worksheet.Cell("D1").Value = "Тип документа";
                            worksheet.Cell("E1").Value = "Номер";
                            worksheet.Cell("F1").Value = "Дата";
                            worksheet.Cell("G1").Value = "Статус";

                            worksheet.Cell("A2").Value = jkList[0].Name;
                            worksheet.Cell("B2").Value = jkList[0].FullName;
                            worksheet.Cell("C2").Value = jkList[0].PostId;
                            worksheet.Cell("D2").Value = jkList[0].TypeDoscId;
                            worksheet.Cell("E2").Value = jkList[0].NumDoc;
                            worksheet.Cell("F2").Value = jkList[0].DateDoc;
                            worksheet.Cell("G2").Value = jkList[0].Status;

                            worksheet.Columns().AdjustToContents();
                            workbook.SaveAs(saveDialog.FileName);
                        }

                        MessageBox.Show("Экспорт завершён успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show($"Нет отчета с номером {txtBoxNumDoc.Text}");
                }
                    return;
            }
        }
        //private void ExportSelectedJornalToExcel(JornalKardViewModel selected)
        //{
            

        //    SaveFileDialog saveDialog = new SaveFileDialog
        //    {
        //        Filter = "Excel файлы (*.xlsx)|*.xlsx",
        //        FileName = "JornalKard_Export.xlsx"
        //    };

        //    if (saveDialog.ShowDialog() == true)
        //    {
        //        using (var workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("JornalKard");

        //            worksheet.Cell("A1").Value = "Имя";
        //            worksheet.Cell("B1").Value = "ФИО";
        //            worksheet.Cell("C1").Value = "Должность";
        //            worksheet.Cell("D1").Value = "Тип документа";
        //            worksheet.Cell("E1").Value = "Номер";
        //            worksheet.Cell("F1").Value = "Дата";
        //            worksheet.Cell("G1").Value = "Статус";

        //            worksheet.Cell("A2").Value = selected.Name;
        //            worksheet.Cell("B2").Value = selected.FullName;
        //            worksheet.Cell("C2").Value = selected.PostId.Title ?? "";
        //            worksheet.Cell("D2").Value = selected.TypeDoc?.Name ?? "";
        //            worksheet.Cell("E2").Value = selected.NumDoc;
        //            worksheet.Cell("F2").Value = selected.DateDoc?.ToString("dd.MM.yyyy");
        //            worksheet.Cell("G2").Value = selected.Status;

        //            worksheet.Columns().AdjustToContents();
        //            workbook.SaveAs(saveDialog.FileName);
        //        }

        //        MessageBox.Show("Экспорт завершён успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //}
    }
}
