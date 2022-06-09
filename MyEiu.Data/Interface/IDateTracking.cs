namespace MyEiu.Data.Interface
{
    internal interface IDateTracking
    {
        DateTime? CreateDate { get; set; }
        DateTime? ModifyDate { get; set; }
    }
}
