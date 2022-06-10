namespace MyEiu.Automapper.ViewModel
{
    public class DepartmentEiuViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<StaffEiuViewModel>? Data { get; set; }
    }
}
