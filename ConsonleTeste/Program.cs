using System;

namespace ConsonleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
			var pessoa1 = new Pessoa_Model()
			{ 
				Nome = "Eduardo",
			    Cpf = "123.123.123-12"
			};
			var pessoa2 = new Pessoa_Model()
			{
				Nome = "Jose",
				Cpf = "123.123.123-00"
			};


			Console.WriteLine(pessoa1.Nome);
			Console.WriteLine(pessoa1.Cpf);

			Console.WriteLine(pessoa2.Nome);
			Console.WriteLine(pessoa2.Cpf);
		}
    }



	public class Pessoa_Model
	{
		private long _cpf;
		public string Cpf
		{
			get => FormataComPontos(_cpf); 
			set => _cpf = TiraPonto(value);    
		}
		public string Nome { get; set; }

		public string FormataComPontos(long cpf)
		{
			string retorno = null;
			string strCpf = cpf.ToString();
			retorno = $"{strCpf.Substring(0, 3)}." +
				$"{strCpf.Substring(3, 3)}." +
				$"{strCpf.Substring(6, 3)}-" +
				$"{strCpf.Substring(9, 2)}";
			return retorno;
		}
		private long TiraPonto(string cpf)
		{
			var cpfSemPonto = cpf.Replace(".", "");
			var cpfSemTraco = cpfSemPonto.Replace("-", "");
			var cpfInt = Convert.ToInt64(cpfSemTraco);
			this.prepara();
			this.finaliza();

			return cpfInt;
		}

		private void prepara()
		{
			Nome += "1";
		}
		private void finaliza()
		{
			if (Nome.Contains("1"))
				Nome = Nome.Replace("2", "1");
			else
				Nome = Nome.Replace("3", "1");
			
		}
	}
}
