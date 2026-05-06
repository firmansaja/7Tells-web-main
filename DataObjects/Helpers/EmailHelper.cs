using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataObjects.Helpers
{
    public class EmailHelper
    {
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFile { get; set; }

        private readonly Db db = new Db();

        public void Send()
        {
            try
            {
                string host = "mail.tsmu.co.id";
                string cc = "log@tsmu.co.id";
                string user = "";
                string from = "";
                string pass = "";
                bool ssl = true;
                int port = 0;

                object[] Params = { "@Param", "SendMailCirculation" };
                DataTable table = db.CreateDataTableSync("usp_S_GeneralParamSelect", CommandType.StoredProcedure, SQLHelpers.strConn, Params);
                if (table.Rows.Count > 0)
                {
                    user = table.Rows[0]["Value1"].AsString();
                    from = table.Rows[0]["Value1"].AsString();
                    pass = table.Rows[0]["Value2"].AsString();
                    port = table.Rows[0]["Value3"].AsInt();

                    var message = new MimeMessage();
                    if (string.IsNullOrEmpty(Sender))
                    {
                        message.From.Add(new MailboxAddress(from, from));
                    }
                    else
                    {
                        message.From.Add(new MailboxAddress(Sender, Sender));
                    }
                    foreach (string item in Recipients)
                    {
                        message.To.Add(new MailboxAddress(item, item));
                    }
                    message.Cc.Add(new MailboxAddress(cc, cc));
                    message.Subject = Subject;

                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = EmailBody(Body),
                        TextBody = "-"
                    };

                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(host, port, ssl);
                        client.Authenticate(user, pass);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string EmailBody(string body)
        {
            return @"
                    <!DOCTYPE html>
                    <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">

                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width,initial-scale=1"">
                        <meta name=""x-apple-disable-message-reformatting"">
                        <title></title>
                        <style>
                            table,
                            td,
                            div,
                            h1,
                            p {
                                font-family: Arial, sans-serif;
                            }
                        </style>
                    </head>

                    <body style=""margin:0;padding:0;"">
                        <table role=""presentation""
                            style=""width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;"">
                            <tr>
                                <td align=""center"" style=""padding:20px;"">
                                    <table role=""presentation""
                                        style=""width:602px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;"">
                                        <tr>
                                            <td align=""center"" style=""padding:40px 0 30px 0;background:#70bbd9;"">
                                                <H1 style=""display:block;"">TAKAGI SARI MULTI UTAMA</H1>
                                                <h1>CIRCULATION</h1>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""padding:50px 30px 150px 30px;"">
                                                <table role=""presentation""
                                                    style=""width:100%;border-collapse:collapse;border:0;border-spacing:0;"">
                                                    <tr>
                                                        <td style=""padding:0 0 36px 0;color:#153643;"">
                                                            <p
                                                                style=""margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;"">
                                                                " + body + "" +
                                                            @"</p>
                                                            <p
                                                                style=""margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;"">
                                                                <a href=""https://7tells.tsmu.co.id/"" target=""_blank""
                                                                    style=""color:blue;text-decoration:none;"">7Tells</a>
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""padding:30px;background:#ee4c50;"">
                                                <table role=""presentation""
                                                    style=""width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;"">
                                                    <tr>
                                                        <td style=""padding:0;width:50%;"" align=""left"">
                                                            <p
                                                                style=""margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;"">
                                                                MIS Team @2022<br />
                                                            </p>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </body>

                    </html>
                ";
        }
    }
}