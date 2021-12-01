namespace AppDev.ViewModel
{
	public class TraineeViewModel
	{
		public string Id { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		//Có thể tách riêng ViewModel tuỳ mục đích sử dụng

		public string FullName { get; set; }

		public int Age { get; set; }

		public string School { get; set; }
	}
}
