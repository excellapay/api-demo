using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace PayTrace
{
    public partial class _Default : System.Web.UI.Page
    {
        private string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["connstring"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                Submit_Click(sender, e);
        }
      

        protected void Submit_Click(object sender, EventArgs e)
        {
            string sPayTraceURL = "https://paytrace.com/api/default.pay";

            WebClient wClient = new WebClient();

            String sRequest = "PARMLIST=" + Server.UrlEncode("UN~" + ConfigurationManager.AppSettings["username"] +
                "|PSWD~" + ConfigurationManager.AppSettings["password"] + "|TERMS~Y" +
                "|TRANXTYPE~Sale|INVOICE~1234" + "|BNAME~" + Request.Form["name"] + "|BADDRESS~" + Request.Form["address1"] + 
                "|BADDRESS2~" + Request.Form["address2"] + "|BCITY~" + Request.Form["city"] + 
                "|BSTATE~" + Request.Form["state"] + "|BZIP~" + Request.Form["zip"] +
                "|AMOUNT~" + Request.Form["amount"] + "|PHONE~" + Request.Form["phone"] + 
                "|EMAIL~" + Request.Form["email"]);

            

            if (Request.Form["frequency"].Equals("once"))
            {
                sRequest += Server.UrlEncode("|METHOD~ProcessTranx|CC~" + Request.Form["credit"] + "|EXPMNTH~" + Request.Form["expMonth"] + "|EXPYR~" + Request.Form["expYear"] +
                "|CSC~" + Request.Form["cvv"]);
                sRequest += "|CUSTID~" + createCustomer(wClient, sPayTraceURL, sRequest);
                sRequest += "|METHOD~ProcessTranx";
                oneTime(wClient, sPayTraceURL, sRequest, false);
            }
            else if (Request.Form["frequency"].Equals("check"))
            {
                sRequest += Server.UrlEncode("|CHECKTYPE~Sale|DDA~" +
                    Request.Form["dda"] + "|TR~" + Request.Form["tr"]);
                sRequest += "|CUSTID~" + createCustomer(wClient, sPayTraceURL, sRequest);
                sRequest += "|METHOD~ProcessCheck";
                oneTime(wClient, sPayTraceURL, sRequest, true);
            }

            else if(Request.Form["frequency"].Equals("weekly"))
            {
                sRequest += Server.UrlEncode("|FREQUENCY~5|START~" + Request.Form["startMonth"] +
                    "/" + Request.Form["startDay"] + "/" + Request.Form["startYear"] + "|TOTALCOUNT~" + Request.Form["totalCount"] + "|CC~" + Request.Form["credit"] +
                    "|CSC~" + Request.Form["cvv"] + "|EXPMNTH~" + Request.Form["expMonth"] + "|EXPYR~" + Request.Form["expYear"]);

                sRequest += Server.UrlEncode("|CUSTID~" + createCustomer(wClient, sPayTraceURL, sRequest));
                sRequest += Server.UrlEncode("|METHOD~CreateRecur");
                recurring(wClient, sPayTraceURL, sRequest, "W");
            }
            else if (Request.Form["frequency"].Equals("monthly"))
            {
                sRequest += Server.UrlEncode("|FREQUENCY~3|START~" + Request.Form["startMonth"] +
                    "/" + Request.Form["startDay"] + "/" + Request.Form["startYear"] + "|TOTALCOUNT~" + Request.Form["totalCount"] + "|CC~" + Request.Form["credit"] +
                    "|CSC~" + Request.Form["cvv"] + "|EXPMNTH~" + Request.Form["expMonth"] + "|EXPYR~" + Request.Form["expYear"]);

                sRequest += Server.UrlEncode("|CUSTID~" + createCustomer(wClient, sPayTraceURL, sRequest));
                sRequest += Server.UrlEncode("|METHOD~CreateRecur");
                recurring(wClient, sPayTraceURL, sRequest, "M");
            }
            else if (Request.Form["frequency"].Equals("annually"))
            {
                sRequest += Server.UrlEncode("|FREQUENCY~1|START~" + Request.Form["startMonth"] +
                    "/" + Request.Form["startDay"] + "/" + Request.Form["startYear"] + "|TOTALCOUNT~" + Request.Form["totalCount"] + "|CC~" + Request.Form["credit"] +
                    "|CSC~" + Request.Form["cvv"] + "|EXPMNTH~" + Request.Form["expMonth"] + "|EXPYR~" + Request.Form["expYear"]);

                sRequest += Server.UrlEncode("|CUSTID~" + createCustomer(wClient, sPayTraceURL, sRequest));
                sRequest += Server.UrlEncode("|METHOD~CreateRecur");
                recurring(wClient, sPayTraceURL, sRequest, "A");
            }
            

        }

        protected void Prefill_Click(object sender, EventArgs e)
        {
            string sPayTraceURL = "https://paytrace.com/api/default.pay";
            
            WebClient wClient = new WebClient();

            String sRequest = "PARMLIST=" + Server.UrlEncode("UN~" + ConfigurationManager.AppSettings["username"] +
                "|PSWD~" + ConfigurationManager.AppSettings["password"] + "|TERMS~Y" +
                "|BNAME~John Doe|BADDRESS~First Address|BName~Test Customer|BADDRESS2~" +
                "Second Address|BCITY~City|BSTATE~State|BZIP~Zip|PHONE~8564758473" +
                "|EMAIL~my@email.comd");

            if (Request.Form["frequency"].Equals("once"))
            {
                sRequest += Server.UrlEncode("|CC~4012000098765439|EXPMNTH~12|EXPYR~20");

                createCustomer(wClient, sPayTraceURL, sRequest);
                sRequest += Server.UrlEncode("|AMOUNT~2.00|TEST~Y|METHOD~ProcessTranx|TRANXTYPE~SALE|CSC~999|INVOICE~1234");
                oneTime(wClient, sPayTraceURL, sRequest, false);
            }
            else if (Request.Form["frequency"].Equals("check"))
            {
                sRequest += Server.UrlEncode("|AMOUNT~2.00|TEST~Y|METHOD~ProcessCheck|CHECKTYPE~Sale|DDA~123456|TR~325070760|INVOICE~1234");
                oneTime(wClient, sPayTraceURL, sRequest, true);
            }

            if (Request.Form["frequency"].Equals("weekly"))
            {
                sRequest += Server.UrlEncode("|CUSTID~11087670|FREQUENCY~5|START~8/20/16|TOTALCOUNT~10");

                createCustomer(wClient, sPayTraceURL, sRequest);
                sRequest += Server.UrlEncode("|AMOUNT~2.00|TEST~Y|METHOD~CreateRecur|TRANXTYPE~SALE|CSC~999|INVOICE~1234");
                recurring(wClient, sPayTraceURL, sRequest, "W");
            }
            else if (Request.Form["frequency"].Equals("monthly"))
            {
                sRequest += Server.UrlEncode("|CUSTID~11087670|FREQUENCY~3|START~8/20/16|TOTALCOUNT~10");

                createCustomer(wClient, sPayTraceURL, sRequest);
                sRequest += Server.UrlEncode("|AMOUNT~2.00|TEST~Y|METHOD~CreateRecur|TRANXTYPE~SALE|CSC~999|INVOICE~1234");
                recurring(wClient, sPayTraceURL, sRequest, "M");
            }
            else if (Request.Form["frequency"].Equals("annually"))
            {
                sRequest += Server.UrlEncode("|CUSTID~11087670|FREQUENCY~1|START~8/20/16|TOTALCOUNT~10");

                createCustomer(wClient, sPayTraceURL, sRequest);
                sRequest += Server.UrlEncode("|AMOUNT~2.00|TEST~Y|METHOD~CreateRecur|TRANXTYPE~SALE|CSC~999|INVOICE~1234");
                recurring(wClient, sPayTraceURL, sRequest, "A");
            }

            
        }

        protected void oneTime(WebClient wClient, String sPayTraceURL, string sRequest, Boolean check)
        {
            Response.Write("Request : " + sRequest + "<br><br>");

            wClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            string sResponse = "";
            try
            {
                sResponse = wClient.UploadString(sPayTraceURL, sRequest);
                Response.Write("Raw Response : " + sResponse + "<br><br>");

                string strERROR = "";
                string strAPPCODE = "";
                string strTRANSACTIONID = "";
                string strCHECKIDENTIFIER = "";
                string strAPPMSG = "";
                string strAVSRESPONSE = "";
                string strCSCRESPONSE = "";
                string strACHCODE = "";
                string strACHMSG = "";

                string[] name_value_pairs = sResponse.Split('|');
                foreach (string row in name_value_pairs)
                {
                    string[] values = row.Split('~');
                    if (values.Length == 2)
                    {
                        Response.Write(values[0] + " : " + values[1] + "<br>");
                        logger(values[0] + " : " + values[1]);

                        if (!check)
                        {
                            if (values[0] == "ERROR")
                                strERROR = values[1];
                            else if (values[0] == "APPCODE")
                                strAPPCODE = values[1];
                            else if (values[0] == "TRANSACTIONID")
                                strTRANSACTIONID = values[1];
                            else if (values[0] == "APPMSG")
                                strAPPMSG = values[1];
                            else if (values[0] == "AVSRESPONSE")
                                strAVSRESPONSE = values[1];
                            else if (values[0] == "CSCRESPONSE")
                                strCSCRESPONSE = values[1];
                        }
                        else
                        {
                            if (values[0] == "ERROR")
                                strERROR += values[1];
                            else if (values[0] == "CHECKIDENTIFIER")
                                strCHECKIDENTIFIER = values[1];
                        }
                        //can continue to grab other response variables as needed
                    } //if (values.Length == 2) {

                    
                } //foreach (string row in name_value_pairs) {

                sRequest = Server.UrlDecode(sRequest.Substring(9));
                name_value_pairs = sRequest.Split('|');
                string value = "'";
                for (int i = 5; i < 14; i++)
                {
                    string[] values = name_value_pairs[i].Split('~');
                    value += values[1] + "', '";
                }

                value = value.Substring(0, value.Length - 3);

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    if (!check)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO [PayTrace].[dbo].[PaytraceResponse]" +
                        "([TransactionType],[TransactionID],[Appcode],[AVS],[CSC],[AppMsg], [Error])" +
                        "VALUES ('O', '" + strTRANSACTIONID + "', '" + strAPPCODE + "', '" + strAVSRESPONSE +
                        "', '" + strCSCRESPONSE + "', '" + strERROR + "', '" + strAPPMSG + "')", conn);
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO [PayTrace].[dbo].[PaytraceResponse]" +
                        "([TransactionType],[CheckIdentifier],[ACHCode],[ACHMessage], [Error]) VALUES (" +
                        "'C', '" + strCHECKIDENTIFIER + "', '" + strACHCODE + "', '" + strACHMSG + "', '" + strERROR + "')", conn);
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    if (!check)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO [PayTrace].[dbo].[PaytraceRequest]([TransactionType],[Name], [Address], [Address2], [City], [State], " +
                        "[Zip], [Amount], [PhoneNumber], [EmailAddress]) VALUES ('O', " + value + ")", conn);
                        {
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO [PayTrace].[dbo].[PaytraceRequest]([TransactionType],[Name], [Address], [Address2], [City], [State], " +
                        "[Zip], [Amount], [PhoneNumber], [EmailAddress], [FirstPaymentDate], [NumOfTransactions]) VALUES ('C', " + value + ")", conn);
                        {
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }

                if (!check)
                {
                    if (strERROR != "")
                        Response.Write("<br>The following error occurred: " + strERROR + ".<br>");
                    else if (strAPPCODE != "")
                    {
                        Response.Write("<br>Transaction ID, " + strTRANSACTIONID + ", was approved with approval code " + strAPPCODE + ".<br>");
                    }
                    else
                        Response.Write("<br>Transaction ID, " + strTRANSACTIONID + ", was NOT approved.<br>");
                }
                else
                {
                    if (strERROR != "")
                        Response.Write("<br>The following error occurred: " + strERROR + ".<br>");
                    else if (strCHECKIDENTIFIER != "")
                        Response.Write("<br>CHECKIDENTIFIER, " + strCHECKIDENTIFIER + ", was approved.<br>");
                    else
                        Response.Write("<br>CHECKIDENTIFIER, " + strCHECKIDENTIFIER + ", was NOT approved.<br>");
                }

            }

            catch (Exception ex)
            {
                Response.Write("Response : FAILED - " + ex + "<br>");


            }
        }

        protected void recurring(WebClient wClient, String sPayTraceURL, string sRequest, string frequency)                                                                                        
        {
            //process a swiped transaction
            //String sRequest = "PARMLIST=" + Server.UrlEncode("UN~demo123|PSWD~demo123|TERMS~Y|METHOD~ProcessTranx|TRANXTYPE~Sale|SWIPE~%B4012881888818888^Demo/Customer^1212101001020001000000701000000?;4012881888818888=12121010010270100001?|AMOUNT~1.00|BADDRESS~1234|BADDRESS~1234 Main Street|BZIP~97201|INVOICE~1234|");

            Response.Write("Request : " + sRequest + "<br><br>");

            wClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            string sResponse = "";
            try
            {
                sResponse = wClient.UploadString(sPayTraceURL, sRequest);
                Response.Write("Raw Response : " + sResponse + "<br><br>");

                string strERROR = "";
                string strRECURID = "";

                string[] name_value_pairs = sResponse.Split('|');

                sRequest = Server.UrlDecode(sRequest.Substring(9));

                foreach (string row in name_value_pairs)
                {
                    string[] values = row.Split('~');
                    if (values.Length == 2)
                    {
                        Response.Write(values[0] + " : " + values[1] + "<br>");
                        logger(values[0] + " : " + values[1]);
                        if (values[0] == "ERROR")
                            strERROR = strERROR + values[1];
                        else if (values[0] == "RECURID")
                            strRECURID = values[1];
                        //can continue to grab other response variables as needed
                    } //if (values.Length == 2) {
                } //foreach (string row in name_value_pairs) {

                name_value_pairs = sRequest.Split('|');
                string value = "'";

                for (int i = 5; i < name_value_pairs.Length; i++)
                {
                    string[] values = name_value_pairs[i].Split('~');

                    if (i != 14 && i != 17 && i != 18 && i != 19 && i != 20 && i != 22)
                        value += values[1] + "', '";
                }

                value = value.Substring(0, value.Length - 3);

                if (strERROR != "")
                    Response.Write("<br>The following error occurred: " + strERROR + ".<br>");
                else if (strRECURID != "")
                {
                    Response.Write("<br>Recur ID, " + strRECURID + ", was approved." + "<br>");
                    logger("TRANSACTIONID: " + strRECURID);
                }
                else
                    Response.Write("<br>Recur ID, " + strRECURID + ", was NOT approved.<br>");

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [PayTrace].[dbo].[PaytraceResponse]" +
                    "([TransactionType],[RecurID],[Error]) VALUES ('" + frequency + "', '" + strRECURID +
                    "', '" + strERROR + "')", conn);
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                
                }

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [PayTrace].[dbo].[PaytraceRequest]([TransactionType],[Name], [Address], [Address2], [City], [State], " +
                    "[Zip], [Amount], [PhoneNumber], [EmailAddress], [FirstPaymentDate], [NumOfTransactions], [CustID]) VALUES ('" + frequency + "', " + value + ")", conn);
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Write("Response : FAILED - " + ex.Message + "<br>");
            }
        }

        protected string createCustomer(WebClient wClient, string sPayTraceURL, string sRequest)
        {
            string custid = "wte_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string email = "";
            string sResponse;
            string[] values;
            string[] names;
            bool exists = false;

            names = Server.UrlDecode(sRequest).Split('|');
            for (int i = 0; i < names.Length; i++)
            {
                values = names[i].Split('~');

                if (values[0].Equals("EMAIL"))
                    email = values[1];
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [Timestamp],[CUSTID],[Email]FROM [PayTrace].[dbo].[CustIDs] " + 
                    "WHERE Email = '" + email + "'", conn);
                {
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        exists = true;
                        custid = dt.Rows[0]["CUSTID"].ToString();
                    }
                    conn.Close();
                }
            }

            if (!exists)
            {
                sRequest += Server.UrlEncode("|METHOD~CreateCustomer|CUSTID~" + custid);
                sResponse = wClient.UploadString(sPayTraceURL + "?" + sRequest, "");
                Response.Write("customer response: " + sResponse);

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [PayTrace].[dbo].[CustIDs]" +
                    "([CUSTID], [Email]) VALUES ('" + custid + "', '" + email + "')", conn);
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

            return custid;
        }

        protected void logger(string logEvent)
        {
            if (ConfigurationManager.AppSettings["logging"].Equals("ON"))
            {
                using (StreamWriter logFile = new StreamWriter("F:\\UseThisPayTrace\\Log.txt", true))
                {
                    logFile.WriteLine("[" + DateTime.Now + "] " + logEvent);
                }
            }
        }
    }
}
