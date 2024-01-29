namespace EmployeeManagement.Models
{


	public class Employee_Tbl
	{
		
		public int Id { get; set; }
		public int EmployeeCode { get; set; }
		public string Name { get; set; }
		public string Rank { get; set; }
		public int DepartmentID { get; set; }
		public Department_Tbl Department { get; set; }
	}
}
