namespace EmployeeManagement.Models
{
	public class Department_Tbl
	{
		
		public int Id { get; set; }
		public int DepartmentCode { get; set; }
		public string Name { get; set; }
		public string location { get; set; }
		public int NumberOfPersonals { get; set; }
		public ICollection<Employee_Tbl>? Employees { get; set; }
	}
}
