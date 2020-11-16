using ClosedXML.Excel;
using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class ExportExcelController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/ExportExcel
        public ActionResult ExportReservations()
        {

                var wb = new XLWorkbook();
                var WS = wb.Worksheets.Add("Sample sheet");

                WS.Row(1).Height = 5;
                WS.Column("A").Width = 0.5;
                WS.Column("C").Width = 30;
                WS.Column("D").Width = 20;
                WS.Column("E").Width = 20;
                WS.Column("F").Width = 20;
                WS.Column("G").Width = 20;
                WS.Column("H").Width = 20;
                WS.Column("I").Width = 20;
                WS.Column("J").Width = 20;


                WS.Cell("B3").Value = "ID";
                WS.Cell("C3").Value = "Car Model";
                WS.Cell("D3").Value = "Cutomer";
                WS.Cell("E3").Value = "Admin";
                WS.Cell("F3").Value = "Start Date";
                WS.Cell("G3").Value = "End Date";
                WS.Cell("H3").Value = "Rental Fee";
                WS.Cell("I3").Value = "Date Added";
                WS.Cell("J3").Value = "Status";





            WS.Range("B2:J2").Merge();
                WS.Row(2).Height = 30;
                WS.Range("B2:J2").Value = "Reservation List";
                WS.Range("B2:J2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Range("B2:J2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Range("B2:J2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Range("B2:J2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Range("B2:J2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Range("B2:J2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                WS.Range("B2:J2").Style.Fill.BackgroundColor = XLColor.Blue;
                WS.Range("B2:J2").Style.Font.FontColor = XLColor.White;
                WS.Range("B2:J2").Style.Font.Bold = true;



                int i = 4;
                foreach (Reservation xdb in db.Reservations.Include("User").Include("Model").Include("Model.Admin").Include("Model.Brand").ToList())
                {
                    WS.Cell("B" + i + "").Value = xdb.Id;
                    WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                    WS.Cell("C" + i + "").Value = xdb.Model.Brand.Name + " " + xdb.Model.Name;
                    WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    WS.Cell("D" + i + "").Value = xdb.User.FullName;
                    WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                    WS.Cell("E" + i + "").Value = xdb.Model.Admin.FullName;
                    WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                    WS.Cell("F" + i + "").Value = xdb.StartDate + " " + xdb.Time;
                    WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                    WS.Cell("G" + i + "").Value = xdb.EndDate + " " + xdb.Time;
                    WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;




                    WS.Cell("H" + i + "").Value = xdb.Price;
                    WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                    WS.Cell("I" + i + "").Value = xdb.PostDate;
                    WS.Cell("I" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("I" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("I" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("I" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("I" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("I" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                if (xdb.isActive)
                {
                    WS.Cell("J" + i + "").Value = "Active";

                }
                else if (xdb.isCancelled)
                {
                    WS.Cell("J" + i + "").Value = "Cancelled";

                }
                else if (xdb.isPending)
                {
                    WS.Cell("J" + i + "").Value = "Pending";

                }
                else
                {
                    WS.Cell("J" + i + "").Value = "Finished";

                }


               
                    WS.Cell("J" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    WS.Cell("J" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    WS.Cell("J" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    WS.Cell("J" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    WS.Cell("J" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    WS.Cell("J" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                i++;
                }

                HttpResponseBase httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Rentals.xlsx\"");

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();





            return View();
        }
        public ActionResult ExportSales()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;


            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Product Name";
            WS.Cell("D3").Value = "Quantity";
            WS.Cell("E3").Value = "Admin";
            WS.Cell("F3").Value = "Post Date";
            WS.Cell("G3").Value = "Total Price";
            WS.Cell("H3").Value = "Status";






            WS.Range("B2:H2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:H2").Value = "Orders List";
            WS.Range("B2:H2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:H2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:H2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:H2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:H2").Style.Font.Bold = true;



            int i = 4;
            foreach (Sale xdb in db.Sales.Include("User").Include("Product").Include("Product.Admin").ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Product.Name;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.Amount;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Product.Admin.FullName;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.PostDate;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("G" + i + "").Value = xdb.Price;
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                if (xdb.isActive)
                {
                    WS.Cell("H" + i + "").Value = "Active";

                }
                else if (xdb.isCancelled)
                {
                    WS.Cell("H" + i + "").Value = "Cancelled";

                }
                else if (xdb.isPending)
                {
                    WS.Cell("H" + i + "").Value = "Pending";

                }
                else
                {
                    WS.Cell("H" + i + "").Value = "Finished";

                }

                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Sales.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
            return View();
        }
        public ActionResult ExportAppointments()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;


            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Service Title";
            WS.Cell("D3").Value = "Appointment Date";
            WS.Cell("E3").Value = "Appointment Time";
            WS.Cell("F3").Value = "Customer Name";
            WS.Cell("G3").Value = "Date Posted";
            WS.Cell("H3").Value = "Status";





            WS.Range("B2:H2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:H2").Value = "Appointment List";
            WS.Range("B2:H2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:H2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:H2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:H2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:H2").Style.Font.Bold = true;



            int i = 4;
            foreach (ReservationService xdb in db.ReservationServices.Include("Service").Include("Service.Admin").ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Service.Title ;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.AppDate;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Time.ToString("HH:mm");
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.User.FullName;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("G" + i + "").Value = xdb.PostDate;
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                if (xdb.isActive)
                {
                    WS.Cell("H" + i + "").Value = "Active";

                }else if (xdb.isCancelled)
                {
                    WS.Cell("H" + i + "").Value = "Cancelled";

                }
                else if (xdb.isPending)
                {
                    WS.Cell("H" + i + "").Value = "Pending";

                }
                else
                {
                    WS.Cell("H" + i + "").Value = "Finished";

                }



                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Appointments.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();





            return View();
        }
        public ActionResult ExportExpert()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;


            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Name";
            WS.Cell("D3").Value = "Twitter";
            WS.Cell("E3").Value = "Instagram";
            WS.Cell("F3").Value = "Linkedin";
            WS.Cell("G3").Value = "Facebook";
            WS.Cell("H3").Value = "Post Date";


            WS.Range("B2:H2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:H2").Value = "Expert List";
            WS.Range("B2:H2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:H2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:H2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:H2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:H2").Style.Font.Bold = true;



            int i = 4;
            foreach (Expert xdb in db.Expert.Include("Admin").ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.FullName;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.TwitterProfile;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.InstagramProfile;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.LinkedInProfile;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("G" + i + "").Value = xdb.FacebookProfile;
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                
                WS.Cell("H" + i + "").Value = xdb.PostDate;
                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Experts.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();





            return View();
        }
        public ActionResult ExportMessages()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;
         


            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "User Name";
            WS.Cell("D3").Value = "Email";
            WS.Cell("E3").Value = "Phone Number";
            WS.Cell("F3").Value = "Subject";
            WS.Cell("G3").Value = "Content";
            WS.Cell("H3").Value = "Date Posted";





            WS.Range("B2:H2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:H2").Value = "Message List";
            WS.Range("B2:H2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:H2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:H2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:H2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:H2").Style.Font.Bold = true;



            int i = 4;
            foreach (Message xdb in db.Message.ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Name;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.Email;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Phone;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.Subject;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("G" + i + "").Value = xdb.Content;
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("H" + i + "").Value = xdb.PostDate;
                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

               

                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Messages.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();





            return View();
        }
        public ActionResult ExportNewsletter()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
          

            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Email";
            WS.Cell("D3").Value = "Date Posted";


            WS.Range("B2:D2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:D2").Value = "Newsletter List";
            WS.Range("B2:D2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:D2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:D2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:D2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:D2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:D2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:D2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:D2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:D2").Style.Font.Bold = true;



            int i = 4;
            foreach (NewsLetter xdb in db.NewsLetters.ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Email;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.PostDate;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



             

                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Newsletter.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();





            return View();
        }
        public ActionResult ExportProducts()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;
            WS.Column("I").Width = 20;


            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Product Name";
            WS.Cell("D3").Value = "Quantity";
            WS.Cell("E3").Value = "Admin";
            WS.Cell("F3").Value = "Post Date";
            WS.Cell("G3").Value = "Price";
            WS.Cell("H3").Value = "Condition";
            WS.Cell("I3").Value = "Description";






            WS.Range("B2:I2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:I2").Value = "Product List";
            WS.Range("B2:I2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:I2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:I2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:I2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:I2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:I2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:I2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:I2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:I2").Style.Font.Bold = true;



            int i = 4;
            foreach (Product xdb in db.Products.Include("Admin").ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Name;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.Amount;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Admin.FullName;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.PostDate;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("G" + i + "").Value = xdb.Price;
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("H" + i + "").Value = xdb.Condition;
                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("I" + i + "").Value = xdb.Condition;
                WS.Cell("I" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("I" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("I" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Products.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
            return View();
        }
        public ActionResult ExportServices()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
         

            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Title";
            WS.Cell("D3").Value = "Description";
            WS.Cell("E3").Value = "Admin";
            WS.Cell("F3").Value = "Post Date";



            WS.Range("B2:F2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:F2").Value = "Service List";
            WS.Range("B2:F2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:F2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:F2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:F2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:F2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:F2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:F2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:F2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:F2").Style.Font.Bold = true;



            int i = 4;
            foreach (Service xdb in db.Service.Include("Admin").ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Title;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.Description;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Admin.FullName;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.PostDate;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Services.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
            return View();
        }
        public ActionResult ExportVacancies()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;
         

            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Title";
            WS.Cell("D3").Value = "Description";
            WS.Cell("E3").Value = "Deadline";
            WS.Cell("F3").Value = "Salary";
            WS.Cell("G3").Value = "Admin";
            WS.Cell("H3").Value = "Post Date";



            WS.Range("B2:H2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:H2").Value = "Vacancy List";
            WS.Range("B2:H2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:H2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:H2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:H2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:H2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:H2").Style.Font.Bold = true;



            int i = 4;
            foreach (Vacancy xdb in db.Vacancies.Include("Admin").ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Title;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.Description;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Deadline;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("F" + i + "").Value = xdb.Salary;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("G" + i + "").Value = xdb.Admin.FullName;
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("H" + i + "").Value = xdb.PostDate;
                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Services.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
            return View();
        }
        public ActionResult ExportUsers()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;
            WS.Column("I").Width = 20;
            WS.Column("J").Width = 20;


            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Name";
            WS.Cell("D3").Value = "Email";
            WS.Cell("E3").Value = "Phone";
            WS.Cell("F3").Value = "Address";
            WS.Cell("G3").Value = "Type";
            WS.Cell("H3").Value = "Post Date";
            WS.Cell("I3").Value = "Last Entrance Time";
            WS.Cell("J3").Value = "Last IP";


            WS.Range("B2:J2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:J2").Value = "User List";
            WS.Range("B2:J2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:J2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:J2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:J2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:J2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:J2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:J2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:J2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:J2").Style.Font.Bold = true;



            int i = 4;
            foreach (User xdb in db.User.ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.FullName;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.Email;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Phone;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.Address;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                if (xdb.IsRegistered)
                {
                    WS.Cell("G" + i + "").Value = "Registered";
                }
                else
                {
                    WS.Cell("G" + i + "").Value = "Not Registered";
                }
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("H" + i + "").Value = xdb.PostDate;
                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("I" + i + "").Value = xdb.LastEntranceTime;
                WS.Cell("I" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("I" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("I" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("J" + i + "").Value = xdb.LastIPAddress;
                WS.Cell("J" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("J" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("J" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("J" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("J" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("J" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Users.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();





            return View();
        }
        public ActionResult ExportModels()
        {

            var wb = new XLWorkbook();
            var WS = wb.Worksheets.Add("Sample sheet");

            WS.Row(1).Height = 5;
            WS.Column("A").Width = 0.5;
            WS.Column("C").Width = 30;
            WS.Column("D").Width = 20;
            WS.Column("E").Width = 20;
            WS.Column("F").Width = 20;
            WS.Column("G").Width = 20;
            WS.Column("H").Width = 20;
            WS.Column("I").Width = 20;
            WS.Column("J").Width = 20;
            WS.Column("K").Width = 20;
            WS.Column("L").Width = 20;
            WS.Column("M").Width = 20;
            WS.Column("N").Width = 20;
            WS.Column("O").Width = 20;
            WS.Column("P").Width = 20;
            WS.Column("R").Width = 20;
            WS.Column("Q").Width = 20;
            WS.Column("S").Width = 20;
            WS.Column("T").Width = 20;
            WS.Column("U").Width = 20;
            WS.Column("V").Width = 20;
            WS.Column("W").Width = 20;
            WS.Column("X").Width = 20;
            WS.Column("Y").Width = 20;
            WS.Column("Z").Width = 20;


            WS.Cell("B3").Value = "ID";
            WS.Cell("C3").Value = "Name";
            WS.Cell("D3").Value = "Brand";
            WS.Cell("E3").Value = "Condition";
            WS.Cell("F3").Value = "Doors";
            WS.Cell("G3").Value = "Engine";
            WS.Cell("H3").Value = "Engine Layout";
            WS.Cell("I3").Value = "Drivetrain";
            WS.Cell("J3").Value = "Fuel Type";
            WS.Cell("K3").Value = "Horse Power";
            WS.Cell("L3").Value = "Mass";
            WS.Cell("M3").Value = "Mileage";
            WS.Cell("N3").Value = "Rental Price (Daily)";
            WS.Cell("O3").Value = "Seats";
            WS.Cell("P3").Value = "Transmission";
            WS.Cell("R3").Value = "ABS";
            WS.Cell("Q3").Value = "Allow Wheels";
            WS.Cell("S3").Value = "ESP";
            WS.Cell("T3").Value = "Sensors";
            WS.Cell("U3").Value = "Conditioner";
            WS.Cell("V3").Value = "CC";
            WS.Cell("W3").Value = "Leather Interior";
            WS.Cell("X3").Value = "Xenon";
            WS.Cell("Y3").Value = "Admin Name";
            WS.Cell("Z3").Value = "Post Date";


            WS.Range("B2:Z2").Merge();
            WS.Row(2).Height = 30;
            WS.Range("B2:Z2").Value = "Model List";
            WS.Range("B2:Z2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            WS.Range("B2:Z2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            WS.Range("B2:Z2").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:Z2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:Z2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:Z2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            WS.Range("B2:Z2").Style.Fill.BackgroundColor = XLColor.Blue;
            WS.Range("B2:Z2").Style.Font.FontColor = XLColor.White;
            WS.Range("B2:Z2").Style.Font.Bold = true;



            int i = 4;
            foreach (Model xdb in db.Model.Include("Admin").Include("Brand").ToList())
            {
                WS.Cell("B" + i + "").Value = xdb.Id;
                WS.Cell("B" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("B" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("B" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("B" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("C" + i + "").Value = xdb.Name;
                WS.Cell("C" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("C" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("C" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("C" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("D" + i + "").Value = xdb.Brand.Name;
                WS.Cell("D" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("D" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("D" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("D" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("E" + i + "").Value = xdb.Condition;
                WS.Cell("E" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("E" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("E" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("E" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;



                WS.Cell("F" + i + "").Value = xdb.Doors;
                WS.Cell("F" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("F" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("F" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("F" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("G" + i + "").Value = xdb.Engine;
                WS.Cell("G" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("G" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("G" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("G" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("H" + i + "").Value = xdb.EngineLayout;
                WS.Cell("H" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("H" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("H" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("H" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("I" + i + "").Value = xdb.DriveTrain;
                WS.Cell("I" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("I" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("I" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("I" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("J" + i + "").Value = xdb.FuelType;
                WS.Cell("J" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("J" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("J" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("J" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("J" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("J" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("K" + i + "").Value = xdb.HorsePower;
                WS.Cell("K" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("K" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("K" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("K" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("K" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("K" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("L" + i + "").Value = xdb.Mass;
                WS.Cell("L" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("L" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("L" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("L" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("L" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("L" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("M" + i + "").Value = xdb.Mileage;
                WS.Cell("M" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("M" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("M" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("M" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("M" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("M" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("N" + i + "").Value = xdb.PriceDaily;
                WS.Cell("N" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("N" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("N" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("N" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("N" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                WS.Cell("O" + i + "").Value = xdb.Seats;
                WS.Cell("O" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("O" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("O" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("O" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("O" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                WS.Cell("P" + i + "").Value = xdb.Transmission;
                WS.Cell("P" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("P" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("P" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("P" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("P" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("P" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("R" + i + "").Value = xdb.hasABS;
                WS.Cell("R" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("R" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("R" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("R" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("R" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("R" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("Q" + i + "").Value = xdb.hasAlloyWheels;
                WS.Cell("Q" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("Q" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("Q" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("Q" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("Q" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                WS.Cell("S" + i + "").Value = xdb.hasESP;
                WS.Cell("S" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("S" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("S" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("S" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("S" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("S" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

             
                WS.Cell("T" + i + "").Value = xdb.hasPSensors;
                WS.Cell("T" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("T" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("T" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("T" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("T" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("T" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

           

                WS.Cell("U" + i + "").Value = xdb.hasConditioner;
                WS.Cell("U" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("U" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("U" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("U" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("U" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("U" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

          

                WS.Cell("V" + i + "").Value = xdb.hasCC;
                WS.Cell("V" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("V" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("V" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("V" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("V" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("V" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                WS.Cell("W" + i + "").Value = xdb.hasLeatherInterior;
                WS.Cell("W" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("W" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("W" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("W" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("W" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("W" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

               
                WS.Cell("X" + i + "").Value = xdb.hasXenon;
                WS.Cell("X" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("X" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("X" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("X" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("X" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("X" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("Y" + i + "").Value = xdb.Admin.FullName;
                WS.Cell("Y" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("Y" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("Y" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("Y" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("Y" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("Y" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                WS.Cell("Z" + i + "").Value = xdb.PostDate;
                WS.Cell("Z" + i + "").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                WS.Cell("Z" + i + "").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                WS.Cell("Z" + i + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                WS.Cell("Z" + i + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                WS.Cell("Z" + i + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                WS.Cell("Z" + i + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;


                i++;
            }

            HttpResponseBase httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"CD-Models.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();





            return View();
        }
    }
}