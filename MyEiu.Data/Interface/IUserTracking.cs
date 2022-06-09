namespace MyEiu.Data.Interface
{
    internal interface IUserTracking
    {
        int? CreateBy { get; set; }
        int? ModifyBy { get; set; }

    }
}
