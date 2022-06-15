namespace MyEiu.Automapper.ViewModel.Staff
{
    public class DepartmentStaffEiuViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<StaffEiuViewModel>? Data { get; set; }
    }
}
