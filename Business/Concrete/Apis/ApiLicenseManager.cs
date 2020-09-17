using Business.Abstract;
using Business.Abstract.Apis;
using Entities.Concrete.License;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Business.Concrete.Apis
{
    public class ApiLicenseManager : IApiLicenseService
    {
        public ProjectLicenseCode Get(string licenseCode)
        {
            ProjectLicenseCode projectLicenseCode = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ozanercan.com.tr/license/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                response = client.GetAsync(licenseCode).Result;
                if (response.IsSuccessStatusCode)
                {
                    projectLicenseCode = response.Content.ReadAsAsync<ProjectLicenseCode>().Result;

                    
                }
            }
            return projectLicenseCode;
        }
    }
}
