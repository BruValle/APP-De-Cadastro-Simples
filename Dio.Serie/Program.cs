using System;

namespace DIO.Serie
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
			Console.Clear();
			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
				case "1":
					ListarSeries();
					break;
				case "2":
					InserirSerie();
					break;
				case "3":
					AtualizarSerie();
					break;
				case "4":
					ExcluirSerie();
					break;
				case "5":
					VisualizarSerie();
					break;
				case "C":
					Console.Clear();
					break;

				default:
					Console.WriteLine("Opção inválida digite Enter para continuar.");
					Console.ReadLine();
					Console.Clear();
					break;
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			//Console.ReadLine();
        }

        private static void ExcluirSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = Convert.ToInt32(Console.ReadLine());

			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = Convert.ToInt32(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{
			
			Console.Write("Digite o id da série: ");
			int indiceSerie = Convert.ToInt32(Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = Convert.ToInt32(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string? entradaTitulo = Console.ReadLine();
			if (entradaTitulo == null){
				entradaTitulo = String.Empty;
			}

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = Convert.ToInt32(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string? entradaDescricao = Console.ReadLine();
			if (entradaDescricao == null){
				entradaDescricao = String.Empty;
			}

			Serie atualizaSerie = new Serie(id: indiceSerie, 
				genero: (Genero)entradaGenero,
				titulo: entradaTitulo,
				ano: entradaAno,
				descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			
			int entradaGenero = 0;
			
			while(true){
			
				Console.Write("Digite o gênero entre as opções acima: ");

				try{

					entradaGenero = Convert.ToInt32(Console.ReadLine());
					if (entradaGenero <0 || entradaGenero > 13){
						Console.WriteLine("Opção inválida, digite uma das opções.");
					}

					else{
						break;
					}

				}
				catch (Exception e){
					Console.WriteLine("Digite a opção de 1 a 13." + e.Message);	
				}
			}
			

			Console.Write("Digite o Título da Série: ");
			string? entradaTitulo = Console.ReadLine();
			if (entradaTitulo == null){
				entradaTitulo = String.Empty;
			}

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = Convert.ToInt32(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string? entradaDescricao = Console.ReadLine();
			if (entradaDescricao == null){
				entradaDescricao = String.Empty;
			}

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
				genero: (Genero)entradaGenero,
				titulo: entradaTitulo,
				ano: entradaAno,
				descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string? opcaoUsuario = Console.ReadLine();
			if (opcaoUsuario == null){
				opcaoUsuario = String.Empty;
			}
			Console.WriteLine();
			return opcaoUsuario.ToUpper();
		}
    }
}