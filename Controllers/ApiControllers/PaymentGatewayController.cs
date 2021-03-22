using Newtonsoft.Json;
using paytm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auth.Controllers.ApiControllers
{
    public class PaymentGatewayController : ApiController
    {
        public IHttpActionResult Post()
        {
            Dictionary<string, object> body = new Dictionary<string, object>();
            Dictionary<string, string> head = new Dictionary<string, string>();
            Dictionary<string, object> requestBody = new Dictionary<string, object>();

            Dictionary<string, string> txnAmount = new Dictionary<string, string>();
            string orderId = Guid.NewGuid().ToString("N");
            txnAmount.Add("value", "1.00");
            txnAmount.Add("currency", "INR");
            Dictionary<string, string> userInfo = new Dictionary<string, string>();
            userInfo.Add("custId", "cust");
            body.Add("requestType", "Payment");
            body.Add("mid", "cMKUvu24082043718755");
            body.Add("websiteName", "WEBSTAGING");
            body.Add("orderId",orderId );
            body.Add("txnAmount", txnAmount);
            body.Add("userInfo", userInfo);
            body.Add("callbackUrl", "https://localhost:44355/callback");

          
            string paytmChecksum = CheckSum.generateCheckSumByJson("IEcW2LzCzI#Zk42l",JsonConvert.SerializeObject(body));

            head.Add("signature", paytmChecksum);

            requestBody.Add("body", body);
            requestBody.Add("head", head);

            string post_data = JsonConvert.SerializeObject(requestBody);

            //For  Staging
            string url = $"https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=cMKUvu24082043718755&orderId={orderId}";

            //For  Production 
            //string  url  =  "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=YOUR_MID_HERE&orderId=ORDERID_98765";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = post_data.Length;

            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(post_data);
            }

            string responseData = string.Empty;

            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
               
            }

            return Ok(responseData);
           
        }
    }
}
