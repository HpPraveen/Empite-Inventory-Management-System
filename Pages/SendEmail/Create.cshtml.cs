using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSystem.Data.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Pages.SendEmail
{
    [Authorize(Roles = "Admin, Manager")]
    public class CreateModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        private readonly string fromMail = "hpptest00@gmail.com";
        private readonly string fromMailUsername = "hpptest00@gmail.com";
        private readonly string fromMailPassword = "hpptest123";
        private bool isMailSent;

        public CreateModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MerchantsName"] = new SelectList(_context.MerchantDetails, "MerchantId", "MerchantName");

            return Page();
        }

        [BindProperty]
        public MerchantDetails MerchantDetails { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var merchantId = Convert.ToInt32(Request.Form["merchant"][0]);
            var toEmailAddress = Request.Form["toEmail"][0].ToString();
            if (toEmailAddress != "")
            {
                var merchantName = _context.MerchantDetails.Where(i => i.MerchantId == merchantId).FirstOrDefault().MerchantName.ToString();
                var inventoryDetailList = _context.InventoryDetails.Where(i => i.MerchantId == merchantId).ToList();
                await _context.SaveChangesAsync();

                CreateMailBody(toEmailAddress, merchantName, inventoryDetailList);

                if (isMailSent == true)
                {
                    TempData["SuccessMessage"] = "Successfully sent the mail to " + merchantName + "!";
                }
                else if (isMailSent == false)
                {
                    TempData["ErrorMessage"] = "Mail send failed!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please fill the email address.";
            }
            return RedirectToPage("./Create");
        }

        public IActionResult OnGetFindUser(int merchantId)
        {
            var merchantEmail = _context.MerchantDetails.Where(i => i.MerchantId == merchantId).FirstOrDefault().MerchantEmail;
            ViewData["MerchantsEmail"] = merchantEmail;
            return new JsonResult(merchantEmail);
        }

        public void CreateMailBody(string toEmail, string merchantName, IEnumerable<InventoryDetails> inventoryDetails)
        {
            string htmlString = @"<html>
                       <head>
                            <style>
                                p {
                                    font-family: 'roboto';
                                    font-size: 11px;
                                    color: #C6C6C6;
                                    font-weight: bold;
                                }
                                h4 {
                                    font-family: 'roboto';
                                    font-size: 14px;
                                    color: #4B9505;
                                }
                                .table {
                                    border-collapse: collapse;
                                    width: 50%;
                                    font-family: 'roboto';
                                }
                                .table td,
                                th {
                                    border: 1px solid gray;
                                    padding: 5px 5px;
                                }
                                .table th {
                                    background-color: #dddddd;
                                    font-size: 13px;
                                    text-align: left;
                                }
                                .table td {
                                    color: #757575;
                                    font-size: 13px;
                                }
                                .blckclr {
                                    color: black !important;
                                    text-align: right;
                                }
                                .textrgt {
                                    text-align: right;
                                }
                                .spespan {
                                    color: #474747;
                                    font-weight: 500;
                                }
                            </style>
                        </head>
                        <body>
                            <div class='table-responsive'>
                                <h4>Inventory Summary Report - " + merchantName + @"</h4>
                                <br>
                                <table class='table'>
                                    <thead>
                                        <tr>
                                            <th>Item Name</th>
                                            <th>Qty</th>
                                        </tr>
                                    </thead>
                                    <tbody>";

            foreach (var item in inventoryDetails)
            {
                htmlString += @"<tr>
                                            <td>" + item.ItemName + @"</td>
                                            <td class='blckclr'>" + item.ItemQty + @"</td>
                                        </tr>";
            }

            htmlString += @"</tbody>
                                </table>
                                <p>This E-mail Automatically generated by Empite. Please do not reply to this mail.</p>
                            </div>
                        </body>
                        </html>";

            SendEmail(htmlString, toEmail);
        }

        public bool SendEmail(string htmlString, string toEmail)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("from", fromMail);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("to", toEmail);
                message.To.Add(to);

                message.Subject = "Inventory Summary Report";

                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = htmlString,
                    TextBody = "Hello World!"
                };

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(fromMailUsername, fromMailPassword);
                client.Send(message);
                isMailSent = true;
                return isMailSent;
            }
            catch (Exception)
            {
                isMailSent = false;
                return isMailSent;
            }
        }
    }
}