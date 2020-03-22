using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using static Api2.Util;

namespace Api.Controllers
{
	[ApiController]
	public class Api2Controller : ControllerBase
	{
		[Route("calculajuros")]
		[HttpGet]
		public ActionResult<decimal> Get(double valorinicial, int meses)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json").Build();

			int PortaApiTaxaJuros = Convert.ToInt32(config["PortaApiTaxaJuros"]);

			if (valorinicial == 0)
			{
				return BadRequest("Necessário informar o parâmetro valorinicial");
			}
			else if (valorinicial == 0)
			{
				return BadRequest("Necessário informar o parâmetro meses");
			}

			var ip = HttpContext.Connection.RemoteIpAddress.ToString();
			ip = ip.Replace(":", "").Replace("f", "");
			string UrlApi = $"{Request.Scheme}://{ip}:{PortaApiTaxaJuros}";


			var client = new RestClient(UrlApi);

			var request = new RestRequest("taxaJuros", Method.GET);
			var response = client.Execute<Double>(request);

			bool isError = (((int)response.StatusCode).ToString()[0]) != '2';

			if (isError)
			{
				return BadRequest("Houve um erro na tentativa de retornar a taxa de juros.");
			}

			double TaxaMensalJuros = response.Data;

			double Calculo = valorinicial * Math.Pow((1 + TaxaMensalJuros), Convert.ToDouble(meses));

			decimal Truncar = Util.TruncateDecimal(Convert.ToDecimal(Calculo), 2);


			return Ok(Truncar.ToString("F2"));
		}


		[Route("showmethecode")]
		[HttpGet]
		public List<Git> GetGit()
		{
			var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json").Build();

			string Api1Url = config["Api1Url"].ToString();
			string Api2Url = config["Api2Url"].ToString();
			string WebUrl = config["WebUrl"].ToString();

			var lstGitRepositoris = new List<Git>();
			lstGitRepositoris.Add(new Git() { Repositorio = "Api1", Url = Api1Url });
			lstGitRepositoris.Add(new Git() { Repositorio = "Api2", Url = Api2Url });
			lstGitRepositoris.Add(new Git() { Repositorio = "Web", Url = WebUrl });

			return lstGitRepositoris;
		}
	}
}
