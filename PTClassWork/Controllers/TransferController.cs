using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PTClassWork.Models;

namespace PTClassWork.Controllers
{
    public class TransferController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TransferModel transferModel)
        {
            TempData["ToAccountNo"] = transferModel.ToAccountNo;
            TempData["ToAccountHolderName"] = transferModel.ToAccountHolderName;
            TempData["AccountNo"] = transferModel.AccountNo;
            TempData["AccountHolderName"] = transferModel.AccountHolderName;
            TempData["Mode"] = transferModel.Mode;
            TempData["Amount"] = transferModel.Amount.ToString();
            return RedirectToAction("Verification");
        }
        public IActionResult Verification()
        {
            //return View(new TransferModel()
            //{
            //    AccountNo = (int)TempData["AccountNo"],
            //    AccountHolderName = (string)TempData["AccountHolderName"],
            //    ToAccountNo = (int)TempData["ToAccountNo"],
            //    ToAccountHolderName = (string)TempData["ToAccountHolderName"],
            //    Amount = Convert.ToDouble(TempData["Amount"].ToString()),
            //    Mode = (string)TempData["Mode"],
            //});
            return View(new TransferModel()
            {
                AccountNo = (int)TempData["AccountNo"],
                AccountHolderName = (string)TempData["AccountHolderName"],
                ToAccountNo = (int)TempData["ToAccountNo"],
                ToAccountHolderName = (string)TempData["ToAccountHolderName"],
                Amount = Convert.ToDouble(TempData["Amount"].ToString()),
                Mode = (string)TempData["Mode"],
            });
        }
        [HttpPost]
        [ActionName("Verification")]
        public IActionResult VerificationPost()
        {
            TransferModel transferModel = new TransferModel()
            {
                AccountNo = (int)TempData["AccountNo"],
                AccountHolderName = (string)TempData["AccountHolderName"],
                ToAccountNo = (int)TempData["ToAccountNo"],
                ToAccountHolderName = (string)TempData["ToAccountHolderName"],
                Amount = Convert.ToDouble(TempData["Amount"].ToString()),
                Mode = (string)TempData["Mode"],
            };
            
            return RedirectToAction("Index");
        }
        void Insert(TransferModel transferModel)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\01\source\repos\TutorialApplication\PTClassWork\App_Data\PTClassWorkDB.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("insert into tbl_transaction values(@AccountNo,@AccountHolderName, @ToAccountNo, @ToAccountHolderName, @Amount, @Mode", con);
            cmd.Parameters.AddWithValue("AccountNo", transferModel.AccountNo);
            cmd.Parameters.AddWithValue("AccountHolderName", transferModel.AccountHolderName);
            cmd.Parameters.AddWithValue("ToAccountNo", transferModel.ToAccountNo);
            cmd.Parameters.AddWithValue("ToAccountHolderName", transferModel.ToAccountHolderName);
            cmd.Parameters.AddWithValue("Amount", transferModel.Amount);
            cmd.Parameters.AddWithValue("Mode", transferModel.Mode);
            con.Open();
            if (cmd.ExecuteNonQuery() > 0)
            {
                TempData["msg"] = "Transfer completed succesfully";
            }
            else
            {
                TempData["msg"] = "Transfer failed";

            }
            con.Close();

        }
    }
}
