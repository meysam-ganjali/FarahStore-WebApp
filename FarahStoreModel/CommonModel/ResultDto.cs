namespace FarahStoreModel.CommonModel;

public class ResultDto
{
    public string Message { get; set; }
    public bool Status { get; set; }
}
public class ResultDto<T>
{
    public string Message { get; set; }
    public bool Status { get; set; }
    public T Data { get; set; }
}