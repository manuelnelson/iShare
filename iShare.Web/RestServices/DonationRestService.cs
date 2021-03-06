﻿using System;
using System.Collections.Generic;
using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using iShare.BusinessLogic.Contracts;
using iShare.Models;
using iShare.Web.Helper;

namespace iShare.Web.RestServices
{
    public class DonationRestService
    {
        [Route("/Donations", "POST")]
        [Route("/Donations", "PUT")]
        [Route("/Donations", "GET")]
        [Route("/Donations", "DELETE")]
        [Route("/Donations")]
        [Route("/Donations/{Id}")]
        public class DonationDto : IReturn<DonationDtoResponse>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public long CharityId { get; set; }
            public double Amount { get; set; }
            public long UserId { get; set; }
            public string Number { get; set; }
            public string Type { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public int Code { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class DonationDtoResponse
        {
            public string Confirmed { get; set; }
            public double Total { get; set; }
        }
        public class DonationsService : Service
        {
            public IDonationService DonationService { get; set; } //Injected by IOC
            public IUserService UserService { get; set; } //Injected by IOC

            public object Get(DonationDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    return DonationService.Get(request.Ids);
                if (request.Id > 0)
                    return DonationService.Get(request.Id);
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid argument(s) supplied.");
            }

            public object Put(DonationDto request)
            {
                var DonationEntity = request.TranslateTo<Donation>();
                DonationService.Update(DonationEntity);
                return DonationEntity;
            }

            public object Post(DonationDto request)
            {
                var DonationEntity = request.TranslateTo<Donation>();
                DonationService.Add(DonationEntity);
                var total = UserService.UpdateAmount(request.UserId, request.Amount);

                //Send to PayPal
                var accessToken = Cache.Get<AccessToken>("0");
                if (accessToken == null)
                {
                    accessToken = new AccessToken { Token = PaypalService.Initiate() };
                    Cache.Add("0", accessToken, new TimeSpan(0, 20, 0));
                }
                try
                {
                    PaypalService.Donate(request, accessToken.Token);
                }
                catch (Exception)
                {
                    accessToken = new AccessToken { Token = PaypalService.Initiate() };
                    Cache.Add("", accessToken, new TimeSpan(0, 20, 0));
                    PaypalService.Donate(request, accessToken.Token);                                        
                }
                
                return new DonationDtoResponse
                {
                    Confirmed = "true",
                    Total = total
                };
            }

            public void Delete(DonationDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    DonationService.DeleteAll(request.Ids);
                else
                    DonationService.Delete(request.Id);
            }

        }
        public class AccessToken
        {
            public string Token { get; set; }
        }

    }

}
