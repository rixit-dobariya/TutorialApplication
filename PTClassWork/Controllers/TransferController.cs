using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PTClassWork.Models;

namespace PTClassWork.Controllers
{
    public class TransferController : Controller
    {
        private readonly IHttpContextAccessor context;

        public TransferController(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TransferModel transferModel)
        {
            context.HttpContext.Session.SetInt32("ToAccountNo", transferModel.ToAccountNo);
            context.HttpContext.Session.SetString("ToAccountHolderName", transferModel.ToAccountHolderName);
            context.HttpContext.Session.SetInt32("AccountNo", transferModel.AccountNo);
            context.HttpContext.Session.SetString("AccountHolderName", transferModel.AccountHolderName);
            context.HttpContext.Session.SetString("Mode", transferModel.Mode);
            context.HttpContext.Session.SetString("Amount", transferModel.Amount.ToString());

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
            //return View(new TransferModel()
            //{
            //    AccountNo = (int)TempData["AccountNo"],
            //    AccountHolderName = (string)TempData["AccountHolderName"],
            //    ToAccountNo = (int)TempData["ToAccountNo"],
            //    ToAccountHolderName = (string)TempData["ToAccountHolderName"],
            //    Amount = Convert.ToDouble(TempData["Amount"].ToString()),
            //    Mode = (string)TempData["Mode"],
            //});
            return View();
        }
        [HttpPost]
        [ActionName("Verification")]
        public IActionResult VerificationPost()
        {
            TransferModel transferModel = new TransferModel()
            {
                AccountNo = (int)context.HttpContext.Session.GetInt32("AccountNo"),
                AccountHolderName = context.HttpContext.Session.GetString("AccountHolderName"),
                ToAccountNo = (int)context.HttpContext.Session.GetInt32("ToAccountNo"),
                ToAccountHolderName = context.HttpContext.Session.GetString("ToAccountHolderName"),
                Amount = Convert.ToDouble(context.HttpContext.Session.GetString("Amount")),
                Mode = context.HttpContext.Session.GetString("Mode"),
            };
            Insert(transferModel);


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
