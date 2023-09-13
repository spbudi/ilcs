using ILCSTest.Models;
using ILCSTest.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ILCSTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string apibaseUrl;
        

        public HomeController(ILogger<HomeController> logger, IConfiguration configure)
        
        {
            _logger = logger;
            _configuration = configure;
            apibaseUrl = _configuration.GetValue<string>("WebAPIBaseURL");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(BiayaImpor kb)
        {
            BiayaImporRepository bir = new BiayaImporRepository();
            
            BiayaImpor biayaImpor = new BiayaImpor();
            Root root = new Root();
            Datum dtm = new Datum();
            int status = 0;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apibaseUrl + "barang?hs_code="+kb.kode_barang;
                using(var response = await client.GetAsync(endpoint))
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        status += 1;
                        var json =  client.GetAsync(endpoint).Result;
                        var a = json.Content.ReadAsStringAsync().Result;
                         root = JsonConvert.DeserializeObject<Root>(a);
                        
                    }
                    else
                    {
                        status = 0;
                    }
                }

            }
            using (HttpClient client = new HttpClient())
            {
                string getTarif = apibaseUrl + "tarif?hs_code=" + kb.kode_barang;
                using (var response = await client.GetAsync(getTarif))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        status += 1;
                        var json = client.GetAsync(getTarif).Result;
                        var a = json.Content.ReadAsStringAsync().Result;
                        dtm = JsonConvert.DeserializeObject<Datum>(a);
                    }
                    else
                    {
                        status = 0;
                    }
                }
            }
            if (status > 1)
            {
                biayaImpor.id_simulasi = Guid.NewGuid();
                biayaImpor.kode_barang = root.data[0].hs_code_format;
                biayaImpor.Waktu_Insert = DateTime.Now;
                biayaImpor.Uraian_barang = root.data[0].uraian_id;
                biayaImpor.Nilai_bm = (kb.Nilai_komoditas * dtm.data[0].cukai / 100);
                biayaImpor.Nilai_komoditas = kb.Nilai_komoditas;
                biayaImpor.Bm = dtm.data[0].bm;

                var a = bir.Insert(biayaImpor);

                return View();
            }
            else
            {
                return View();
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}