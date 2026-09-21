public class Program
{
	public static async Task Main()
	{
		Console.WriteLine("Ejecucion secuencial");
		DateTime inicio = DateTime.Now;
		Secuencial();
		double tiempo = (DateTime.Now - inicio).TotalMilliseconds;
		Console.WriteLine($"Tiempo:{tiempo} ms");
		
		Console.WriteLine("Ejecucion async/await");
		inicio = DateTime.Now;
		await AsyncAwait(CancellationToken.None);
		tiempo = (DateTime.Now - inicio).TotalMilliseconds;
		Console.WriteLine($"Tiempo:{tiempo} ms");
		
		Console.WriteLine("Ejecucion de mejor rendimiento");
		inicio = DateTime.Now;
		await MejorRendimiento(CancellationToken.None);
		tiempo = (DateTime.Now - inicio).TotalMilliseconds;
		Console.WriteLine($"Tiempo:{tiempo} ms");
		
		Console.WriteLine("Async/await + timeout");
		try
		{
			using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
			await AsyncAwait(cts.Token);
			Console.WriteLine("Completado");
		}
		catch (Exception ex)
		{
			Console.WriteLine("Cancelado");
		}
		
		Console.WriteLine("Mejor rendimiento + timeout");
		try
		{
			using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
			await MejorRendimiento(cts.Token);
			Console.WriteLine("Completado");
		}
		catch (Exception ex)
		{
			Console.WriteLine("Cancelado");
		}
	}

	private static void Secuencial()
	{
		HacerCafe();
		CalentarSarten();
		FreirHuevos();
		FreirBacon();
		TostarPan();
		UntarMantequilla();
		HacerZumo();
	}

	private static async Task AsyncAwait(CancellationToken token)
	{
		await HacerCafeAsync(token);
		await CalentarSartenAsync(token);
		await FreirHuevosAsync(token);
		await FreirBaconAsync(token);
		await TostarPanAsync(token);
		await UntarMantequillaAsync(token);
		await HacerZumoAsync(token);
	}

	private static async Task MejorRendimiento(CancellationToken token)
	{
		Task cafe =  HacerCafeAsync(token);

		async Task tostada()
		{
			await TostarPanAsync(token);
			await UntarMantequillaAsync(token);
		}

		async Task freir()
		{
			await CalentarSartenAsync(token);

			Task huevos = FreirHuevosAsync(token);
			Task bacon = FreirBaconAsync(token);
			
			await Task.WhenAll(huevos, bacon);
		}
		
		Task zumo = HacerZumoAsync(token);
		
		await Task.WhenAll(cafe, tostada(), freir(), zumo);
	}
	
	private static void HacerCafe()
	{
		Thread.Sleep(200);
	}
	
	private static void CalentarSarten()
	{
		Thread.Sleep(200);
	}
	
	private static void FreirHuevos()
	{
		Thread.Sleep(300);
	}
	
	private static void FreirBacon()
	{
		Thread.Sleep(300);
	}
	
	private static void TostarPan()
	{
		Thread.Sleep(200);
	}
	
	private static void UntarMantequilla()
	{
		Thread.Sleep(100);
	}
	
	private static void HacerZumo()
	{
		Thread.Sleep(200);
	}
	
	private static async Task HacerCafeAsync(CancellationToken token)
	{
		await Task.Delay(200, token);
	}
	
	private static async Task CalentarSartenAsync(CancellationToken token)
	{
		await Task.Delay(200, token);
	}
	
	private static async Task FreirHuevosAsync(CancellationToken token)
	{
		await Task.Delay(300, token);
	}
	
	private static async Task FreirBaconAsync(CancellationToken token)
	{
		await Task.Delay(300, token);
	}
    
	private static async Task TostarPanAsync(CancellationToken token)
	{
		await Task.Delay(200, token);
	}
	
	private static async Task UntarMantequillaAsync(CancellationToken token)
	{
		await Task.Delay(100, token);
	}
	
	private static async Task HacerZumoAsync(CancellationToken token)
	{
		await Task.Delay(200, token);
	}
}