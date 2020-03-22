using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void VerificaQuantidadeRepositoriosRetornada()
		{
			var obj = new Api2Controller();
			var ApisFontes = obj.GetGit();

			var TotalApis = ApisFontes;

			//Para caracter de exemplo de teste certifica que são 3 repositórios no Git.

			Assert.AreEqual(TotalApis.Count, 3);
		}
	}
}
