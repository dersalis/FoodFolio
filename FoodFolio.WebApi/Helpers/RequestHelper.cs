namespace FoodFolio.WebApi.Helpers;

    public class RequestHelper
{
	public static string GetHostName(HttpRequest request)
	{
		return $"{request.Scheme}://{request.Host}";
        }
}

