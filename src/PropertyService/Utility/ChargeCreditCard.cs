using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using PropertyService.BO;
using PropertyService.ViewModel;
using System.Globalization;

namespace PropertyService
{
    public class ChargeCreditCard
    {
        public static TenantPaymentModel Run(String ApiLoginID, String ApiTransactionKey, Residential_Tenant_Application_Step1 objTenant)
        {
            // Console.WriteLine("Charge Credit Card Sample");
            TenantPaymentModel objPaymentModel = new TenantPaymentModel();

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

           
            
            //var dateTime = DateTime.ParseExact(objTenant.Exp, "MMyy", CultureInfo.CurrentCulture);
            //string sExpDate = dateTime.ToString();

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = objTenant.CreditCardNumber,
                expirationDate = objTenant.Exp,
                cardCode = objTenant.CVSNumber
            };

            var billingAddress = new customerAddressType
            {
                firstName = objTenant.NameOfAccount,
                lastName = "",
                address = objTenant.Address,
                city = objTenant.City,
                zip = objTenant.ZipCode
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            // Add line Items
            var lineItems = new lineItemType[2];
            //lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(15.00) };
            //lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(450.00) };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                amount = Convert.ToDecimal(objTenant.TotalApplicationFree),
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;


            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        //Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        //Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        //Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        //Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        //Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);

                        objPaymentModel.IsSuccess = true;
                        objPaymentModel.AuthCode = response.transactionResponse.authCode;
                        objPaymentModel.TransactionCode = response.transactionResponse.messages[0].code;
                        objPaymentModel.TransactionDetails = response.transactionResponse.messages[0].description;
                    }
                    else
                    {
                       // Console.WriteLine("Failed Transaction.");
                        objPaymentModel.IsSuccess = false;
                        objPaymentModel.AuthCode = "";
                        if (response.transactionResponse.errors != null)
                        {
                            //Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            //Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                            objPaymentModel.TransactionCode = response.transactionResponse.errors[0].errorCode;
                            objPaymentModel.TransactionDetails = response.transactionResponse.errors[0].errorText;
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("Failed Transaction.");
                    objPaymentModel.IsSuccess = false;
                    objPaymentModel.AuthCode = "";

                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        objPaymentModel.TransactionCode = response.transactionResponse.errors[0].errorCode;
                        objPaymentModel.TransactionDetails = response.transactionResponse.errors[0].errorText;

                        //Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        //Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        //Console.WriteLine("Error Code: " + response.messages.message[0].code);
                        //Console.WriteLine("Error message: " + response.messages.message[0].text

                        objPaymentModel.TransactionCode = response.messages.message[0].code;
                        objPaymentModel.TransactionDetails = response.messages.message[0].text;
                    }
                }
            }
            else
            {
                objPaymentModel.IsSuccess = false;
                objPaymentModel.AuthCode = "";
                objPaymentModel.TransactionCode = "";
                objPaymentModel.TransactionDetails = "";

               // Console.WriteLine("Null Response.");
            }

            return objPaymentModel;
        }
    }
}
